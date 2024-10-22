using MongoDB.Driver;
using SeniorLearn.Data;

namespace SeniorLearn.Services
{
    public class BulletinService
    {
        // TODO: Update later. Localhost for now.

        private const string connectionString = "mongodb://localhost:27017";
        private readonly IMongoCollection<Bulletin> _bulletinCollection;

        public BulletinService()
        {
            var mongoClient = new MongoClient(connectionString);
            _bulletinCollection = mongoClient.GetDatabase("bulletin_db")
                .GetCollection<Bulletin>("bulletin");
        }

        //TODO: Service Methods for bulletins GET/POST/UPDATE etc.
    }
}