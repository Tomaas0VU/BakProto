using MqttSubscriber.NET.Models;
using MqttSubscriber.NET.Database.Interfaces;
using MongoDB.Driver;

namespace MqttSubscriber.NET.Database
{
    public class MongoDBClient : IDatabase
    {
        private const string connectionString = "mongodb://127.0.0.1:27017";

        public void InsertTemperatureReadingToDatabase(Message message)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Bakalaurinis");
            var collection = db.GetCollection<Message>("Temperature");
            collection.InsertOne(message);
        }

        public void InsertElectricityReadingToDatabase(Message message)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Bakalaurinis");
            var collection = db.GetCollection<Message>("Electricity");
            collection.InsertOne(message);
        }
    }
}
