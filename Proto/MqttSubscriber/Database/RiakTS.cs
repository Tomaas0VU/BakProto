using Models;
using MqttSubscriber.Database.Interfaces;
using System;

namespace MqttSubscriber.Database
{
    public class RiakTSClient : IDatabase
    {
        private string connectionString = "";

        public void InsertTemperatureReadingToDatabase(Message message)
        {
            throw new NotImplementedException();
        }

        public void InsertElectricityReadingToDatabase(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
