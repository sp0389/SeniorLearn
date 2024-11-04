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

        public BulletinService(OrganisationUserService organisationUserService, 
            IOptions<BulletinDatabaseSettings> bulletinDatabaseSettings)
        {
            var mongoClient = new MongoClient(bulletinDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bulletinDatabaseSettings.Value.DatabaseName);
            _bulletinCollection = mongoDatabase.GetCollection<Bulletin>(bulletinDatabaseSettings
                .Value.BulletinCollectionName);
            _organisationUserService = organisationUserService;
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
            //This code is a lot better for the functionality of searching for tags later, but I've
            //included a custom sort ignoring accents/case & a binary search for the assessment requirements below.
            
            //var filter = Builders<Bulletin>.Filter.AnyEq(b => b.Tags, searchTerm);
            //var bulletins = await _bulletinCollection
            //    .Find(filter)
            //    .ToListAsync();
            //return bulletins;

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

        public async Task<Bulletin> SaveNewBulletinAsync(Bulletin bulletin, string memberId)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(memberId);
            bulletin.MemberId = member.Id;
            bulletin.MemberName = $"{member.FirstName} {member.LastName}";

            await _bulletinCollection.InsertOneAsync(bulletin);
            return bulletin;
        }

        public async Task<Bulletin> UpdateExistingBulletinAsync(string id, Bulletin bulletin)
        {
            var existingBulletin = await GetBulletinByIdAsync(id);

            var filter = Builders<Bulletin>.Filter.Eq(b => b.Id, existingBulletin.Id);

            var update = Builders<Bulletin>.Update
                .Set(b => b.Title, bulletin.Title)
                .Set(b => b.ContentMessage, bulletin.ContentMessage)
                .Set(b => b.ContentImageUrl, bulletin.ContentImageUrl)
                .Set(b => b.UpdatedAt, DateTime.UtcNow)
                .Set(b => b.Status, bulletin.Status)
                .Set(b => b.Tags, bulletin.Tags)
                .Set(b => b.MemberId, existingBulletin.MemberId)
                .Set(b => b.MemberName, existingBulletin.MemberName);

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
            await _bulletinCollection.DeleteOneAsync(filter);
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
    }
}