using ElectricityDevice.Counter.Interfaces;
using ElectricityDevice.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace ElectricityDevice.Counter.MongoDB
{
    public class MongoDBCounter : ICanStoreCounter
    {
        // Šitą saugom lokaliai
        private const string connectionString = "mongodb://localhost:27017";
        private const string collectionName = "ElectricityConfig";

        public async Task StoreConfigForDevice(string serialNo, DateTime time, double counter)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Bakalaurinis");
            var collection = db.GetCollection<ElectricityCounterEntity>(collectionName);
            await collection.FindOneAndUpdateAsync(
                                Builders<ElectricityCounterEntity>.Filter.Eq("_id", serialNo),
                                Builders<ElectricityCounterEntity>.Update.Set("Value", counter).Set("Time", time)
                                );
        }
        public async Task<ElectricityDeviceConfig> GetConfigForDevice(string serialNo)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Bakalaurinis");
            var collection = db.GetCollection<ElectricityCounterEntity>(collectionName);
            var result = await collection.Find(Builders<ElectricityCounterEntity>.Filter.Eq("_id", serialNo)).FirstOrDefaultAsync();
            if (result == null)
            {
                result = new ElectricityCounterEntity
                {
                    SerialNo = serialNo,
                    Time = new DateTime(2010,1,1),
                    Value = 10000
                };
                await collection.InsertOneAsync(result);
            }
            return result;
        }
    }
}
