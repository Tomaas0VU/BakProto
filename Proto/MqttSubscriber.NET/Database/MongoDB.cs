using MqttSubscriber.NET.Models;
using MqttSubscriber.NET.Database.Interfaces;
using MongoDB.Driver;

namespace MqttSubscriber.NET.Database
{
    public class MongoDBClient : IDatabase
    {
        private const string connectionString = "mongodb://192.168.1.105:27017";

        public void InsertTemperatureReadingToDatabase(Message message)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Bakalaurinis");
            var collection = db.GetCollection<Message>("TemperatureProduction");
            collection.InsertOne(message);
        }

        public void InsertElectricityReadingToDatabase(Message message)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Bakalaurinis");
            var collection = db.GetCollection<Message>("ElectricityProduction");
            collection.InsertOne(message);
        }
    }
}
