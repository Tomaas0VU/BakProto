using Models;
using MqttSubscriber.Database.Interfaces;
using MongoDB.Driver;

namespace MqttSubscriber.Database
{
    public class MongoDBClient : IDatabase
    {
        private string connectionString = "mongodb://localhost:27017";

        public void InsertTemperatureReadingToDatabase(Message message)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Bakalaurinis");
            var collection = db.GetCollection<Message>("Temperature");
            collection.InsertOne(message);
        }
    }
}
