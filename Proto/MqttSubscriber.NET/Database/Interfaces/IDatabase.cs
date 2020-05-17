using MqttSubscriber.NET.Models;

namespace MqttSubscriber.NET.Database.Interfaces
{
    public interface IDatabase
    {
        void InsertTemperatureReadingToDatabase(Message message);
        void InsertElectricityReadingToDatabase(Message message);
    }
}
