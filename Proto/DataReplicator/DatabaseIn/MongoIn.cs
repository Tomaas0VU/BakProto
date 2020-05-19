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
        public async Task<List<Message>> GetData(string collectionName, DateTime from, DateTime to)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("Bakalaurinis");
            var collection = db.GetCollection<Message>(collectionName);
            var result = await collection.Find(Builders<Message>.Filter.And(
                Builders<Message>.Filter.Gte("Timestamp", from),
                Builders<Message>.Filter.Lt("Timestamp", to)
                )).ToListAsync();

            return result;
        }
    }
}
