using MongoDB.Driver;
using SeniorLearn.Data;
using MongoDB.Bson;
using System.Globalization;
using Microsoft.Extensions.Options;

namespace SeniorLearn.Services
{
    public class BulletinService
    {
        private readonly IMongoCollection<Bulletin> _bulletinCollection;
        private readonly OrganisationUserService _organisationUserService;
        private readonly IWebHostEnvironment _webHost;

        public BulletinService(OrganisationUserService organisationUserService, 
            IOptions<BulletinDatabaseSettings> bulletinDatabaseSettings, IWebHostEnvironment webHost)
        {
            var mongoClient = new MongoClient(bulletinDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bulletinDatabaseSettings.Value.DatabaseName);
            _bulletinCollection = mongoDatabase.GetCollection<Bulletin>(bulletinDatabaseSettings
                .Value.BulletinCollectionName);
            _organisationUserService = organisationUserService;
            _webHost = webHost;
        }

        public async Task<List<Bulletin>> GetBulletinsAsync()
        {
            var filter = Builders<Bulletin>.Filter.Eq(b => b.Status, "Active");
            var bulletins = await _bulletinCollection
                .Find(filter)
                .ToListAsync();
            return bulletins.OrderByDescending(b => b.CreatedAt).ToList();
        }

        public async Task<Bulletin> GetBulletinByIdAsync(string id)
        {
            var filter = Builders<Bulletin>.Filter.Eq("_id", new ObjectId(id));
            var bulletin = await _bulletinCollection
                .Find(filter)
                .FirstOrDefaultAsync();
            return bulletin;
        }

        public async Task<Bulletin> GetBulletinsBySearchTermAsync(string searchTerm)
        {
            var bulletins = await GetBulletinsAsync();

            //https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object
            //https://learn.microsoft.com/en-us/dotnet/api/system.string.compare?view=net-8.0&redirectedfrom=MSDN#System_String_Compare_System_String_System_String_System_Boolean

            bulletins.Sort((a, b) => String.Compare(a.Title, b.Title, CultureInfo.InvariantCulture,
                CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase));

            int low = 0;
            int high = bulletins.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (String.Compare(bulletins[mid].Title, searchTerm, true) == 0)
                {
                    return bulletins[mid];
                    
                }
                else if (String.Compare(bulletins[mid].Title, searchTerm, true) > 0)
                {
                    high = mid - 1;
                }
                else if (String.Compare(bulletins[mid].Title, searchTerm, true) < 0)
                {
                    low = mid + 1;
                }
            }
            return null!;
        }

        public async Task<Bulletin> SaveNewBulletinAsync(string title, string contentMessage, List<string>? tagList, IFormFile? image, string memberId)
        {
            var bulletin = new Bulletin();
            var member = await _organisationUserService.GetUserByUserNameAsync(memberId);

            if (image != null)
            {
                bulletin.ContentImageUrl = await AddImageUrlForBulletinAsync(image);
            }
            else
            {
                bulletin.ContentImageUrl = $"http://10.0.2.2:5085/images/placeholder.jpg";
            }

            bulletin.Title = title;
            bulletin.ContentMessage = contentMessage;
            bulletin.MemberId = member.Id;
            bulletin.MemberName = $"{member.FirstName} {member.LastName}";
            bulletin.MemberEmail = member.Email;
            bulletin.Tags = tagList!;
            
            await _bulletinCollection.InsertOneAsync(bulletin);
            return bulletin;
        }

        public async Task<Bulletin> UpdateExistingBulletinAsync(string id, string title, string contentMessage, string contentImageUrl, string status, List<string> tagList, IFormFile? image)
        {
            var existingBulletin = await GetBulletinByIdAsync(id);

            if (image != null)
            {
                contentImageUrl = await AddImageUrlForBulletinAsync(image);
            }

            var bulletin = new Bulletin()
            {
                Title = title,
                ContentMessage = contentMessage,
                ContentImageUrl = contentImageUrl,
                Status = status,
                Tags = tagList,
                MemberId = existingBulletin.MemberId,
                MemberName = existingBulletin.MemberName,
                MemberEmail = existingBulletin.MemberEmail,
            };

            var filter = Builders<Bulletin>.Filter.Eq(b => b.Id, existingBulletin.Id);

            var update = Builders<Bulletin>.Update
                .Set(b => b.Title, bulletin.Title)
                .Set(b => b.ContentMessage, bulletin.ContentMessage)
                .Set(b => b.ContentImageUrl, bulletin.ContentImageUrl)
                .Set(b => b.UpdatedAt, DateTime.UtcNow)
                .Set(b => b.Status, bulletin.Status)
                .Set(b => b.Tags, bulletin.Tags)
                .Set(b => b.MemberId, bulletin.MemberId)
                .Set(b => b.MemberName, bulletin.MemberName)
                .Set(b => b.MemberEmail, bulletin.MemberEmail);

            await _bulletinCollection.UpdateOneAsync(filter, update);
            return bulletin;
        }

        public async Task<Bulletin> AddCommentToBulletinAsync(string id, BulletinComment bulletinComment, string memberId)
        {
            var existingBulletin = await GetBulletinByIdAsync(id) 
                ?? throw new Exception("Bulletin with that ID does not exist!");

            var member = await _organisationUserService.GetUserByUserNameAsync(memberId);
    
            bulletinComment.MemberId = member.Id;
            bulletinComment.MemberName = $"{member.FirstName} {member.LastName}";

            existingBulletin.Comments.AddLast(bulletinComment);

            var filter = Builders<Bulletin>.Filter.Eq(b => b.Id, existingBulletin.Id);
            var update = Builders<Bulletin>.Update.Push(b => b.Comments, bulletinComment);

            await _bulletinCollection.UpdateOneAsync(filter, update);
            return existingBulletin;
        }

        public async Task DeleteBulletinAsync(string id)
        {
            var existingBulletin = await GetBulletinByIdAsync(id)
                ?? throw new Exception("Bulletin with that ID does not exist!");

            var filter = Builders<Bulletin>.Filter.Eq(b => b.Id, existingBulletin.Id);
            
            existingBulletin.Status = "Inactive";

            var update = Builders<Bulletin>.Update
                .Set(b => b.Status, existingBulletin.Status);

            await _bulletinCollection.UpdateOneAsync(filter, update);
        }

        public async Task<Bulletin> UpdateBulletinLikesAsync(string id)
        {
            var existingBulletin = await GetBulletinByIdAsync(id);
            existingBulletin.Likes++;

            var filter = Builders<Bulletin>.Filter.Eq(b => b.Id, existingBulletin.Id);
            var update = Builders<Bulletin>.Update.Set(b => b.Likes, existingBulletin.Likes);

            await _bulletinCollection.UpdateOneAsync(filter, update);

            return existingBulletin;
        }

        public async Task<Bulletin> DecreaseBulletinLikesAsync(string id)
        {
            var existingBulletin = await GetBulletinByIdAsync(id);
            if (existingBulletin.Likes > 0) 
            {
                existingBulletin.Likes--;

                var filter = Builders<Bulletin>.Filter.Eq(b => b.Id, existingBulletin.Id);
                var update = Builders<Bulletin>.Update.Set(b => b.Likes, existingBulletin.Likes);

                await _bulletinCollection.UpdateOneAsync(filter, update);
            }

            return existingBulletin;
        }

        private async Task<string> AddImageUrlForBulletinAsync(IFormFile image)
        {
            var imageFolder = Path.Combine(_webHost.WebRootPath, "images");
            var imageName = Path.GetFileName(image.FileName);
            var imageSavePath = Path.Combine(imageFolder, imageName);
            
            var relativePath = $"http://10.0.2.2:5085/images/{imageName}";

            using (var stream = new FileStream(imageSavePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return relativePath;
        }
    }
}