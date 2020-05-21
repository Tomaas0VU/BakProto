using DataReplicator.DatabaseOut.Interfaces;
using DataReplicator.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace DataReplicator.DatabaseOut
{
    public class MongoOut : ICanStore
    {
        private string _connectionString;
        public MongoOut(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task InsertData(string collectionName, List<Message> data)
        {
            var list = new List<Message>();
            foreach(Message point in data)
            {
                list.Add(new Message
                {
                    SerialNo = point.SerialNo,
                    DeviceName = point.DeviceName,
                    Timestamp = point.Timestamp,
                    Value = point.Value,
                });
            }
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("BakalaurasProd");
            var collection = db.GetCollection<Message>(collectionName);
            await collection.InsertManyAsync(list);
        }
    }
}
