using Models;
using MqttSubscriber.Database.Interfaces;
using System;
using RiakClient;

namespace MqttSubscriber.Database
{
    public class RiakTSClient : IDatabase
    {
        private string connectionString = "";

        public void InsertTemperatureReadingToDatabase(Message message)
        {
            const string contributors = "contributors";
            IRiakEndPoint cluster = RiakCluster.FromConfig("riakConfig");
            IRiakClient client = cluster.CreateClient();
        }

        public void InsertElectricityReadingToDatabase(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
