using MongoDB.Bson;

namespace SeniorLearn.Data
{
    public enum BulletinStatus
    {
        Active,
        Archived
    }

    public class Bulletin
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; } = default!;
        public string MemberId { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ContentImageUrl { get; set; } = default!;
        public string ContentMessage { get; set; } = default!;
        public List<string> Tags { get; set; } = new();
        public int Likes { get; set; }
        public BulletinStatus Status { get; set; } = BulletinStatus.Active;
        public List<BulletinComment> Comments { get; set; } = new();
    }

    public class BulletinComment
    {
        public ObjectId Id { get; set; }
        public ObjectId BulletinId { get; set; } = default!;
        public string MemberId { get; set; } = default!;
        public string ProfileImageUrl { get; set; } = default!;
        public DateTime CommentedAt { get; set; }
        public string Message { get; set; } = default!;
    }
}