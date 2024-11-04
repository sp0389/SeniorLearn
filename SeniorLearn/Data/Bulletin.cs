using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SeniorLearn.Data
{
    public enum BulletinStatus
    {
        Active,
        Archived
    }

    public class Bulletin
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; } = default!;
        public string MemberId { get; set; } = default!;
        public string MemberName { get; set;} = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ContentImageUrl { get; set; } = default!;
        public string ContentMessage { get; set; } = default!;
        public List<string> Tags { get; set; } = new();
        public int Likes { get; set; }
        public string Status { get; set; }
        public LinkedList<BulletinComment> Comments { get; set; }

        public Bulletin()
        {
            Id = ObjectId.GenerateNewId().ToString();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Likes = 0;
            Status = BulletinStatus.Active.ToString();
            Comments = new LinkedList<BulletinComment>();
        }
    }

    public class BulletinComment
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MemberId { get; set; } = default!;
        public string MemberName { get; set; } = default!;
        public DateTime CommentedAt { get; set; }
        public string Message { get; set; } = default!;

        public BulletinComment()
        {
            Id = ObjectId.GenerateNewId().ToString();
            CommentedAt = DateTime.UtcNow;
        }
    }

    public class BulletinDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string BulletinCollectionName { get; set; } = null!;
    }
}