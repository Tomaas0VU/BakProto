using Models;

namespace MqttSubscriber.Database.Interfaces
{
    public interface IDatabase
    {
        void InsertTemperatureReadingToDatabase(Message message);
        void InsertElectricityReadingToDatabase(Message message);
    }
}
