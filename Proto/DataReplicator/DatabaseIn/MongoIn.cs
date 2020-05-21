using DataReplicator.DatabaseIn.Interfaces;
using DataReplicator.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataReplicator.DatabaseIn
{
    public class MongoIn : ICanRead
    {
        private string _connectionString;
        public MongoIn(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<MessageFromMongo>> GetData(string collectionName, DateTime from, DateTime to)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("BakalaurasProd");
            var collection = db.GetCollection<MessageFromMongo>(collectionName);
            var result = await collection.Find(Builders<MessageFromMongo>.Filter.And(
                Builders<MessageFromMongo>.Filter.Gte("Timestamp", from),
                Builders<MessageFromMongo>.Filter.Lt("Timestamp", to)
                )).ToListAsync();

            return result;
        }
    }
}
