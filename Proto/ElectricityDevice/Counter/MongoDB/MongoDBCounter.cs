using ElectricityDevice.Counter.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ElectricityDevice.Counter.MongoDB
{
    public class MongoDBCounter : ICanStoreCounter
    {
        // Šitą saugom lokaliai
        private const string connectionString = "mongodb://localhost:27017";
        private const string collectionName = "ElectricityConfig";

        public async Task StoreCounterForDevice(string serialNo, double counter)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Bakalaurinis");
            var collection = db.GetCollection<ElectricityCounterEntity>(collectionName);
            await collection.FindOneAndUpdateAsync(
                                Builders<ElectricityCounterEntity>.Filter.Eq("_id", serialNo),
                                Builders<ElectricityCounterEntity>.Update.Set("Value", counter)
                                );
        }
        public async Task<double> GetCounterForDevice(string serialNo)
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
                    Value = 10000
                };
                await collection.InsertOneAsync(result);
            }
            return result.Value;
        }
    }
}
