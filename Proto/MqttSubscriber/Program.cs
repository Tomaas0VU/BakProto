using Models;
using MqttSubscriber.Database;
using MqttSubscriber.Database.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Mqtt;
using System.Text;

namespace MqttSubscriber
{
    class Program
    {
        static string _hostname = "localhost";
        static string[] _topics = { "data/water", "data/temperature" };

        static void Main(string[] args)
        {
            IDatabase mongo = new MongoDBClient();

            var mqttClient = MqttClient.CreateAsync(_hostname).Result;
            var sess = mqttClient.ConnectAsync().Result;
            foreach (string topic in _topics)
            {
                mqttClient.SubscribeAsync(topic, MqttQualityOfService.ExactlyOnce);
            }
            mqttClient.MessageStream.Subscribe(msg =>
            {
                string msgString = Encoding.UTF8.GetString(msg.Payload);
                Console.WriteLine(String.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), msgString));

                Message messageObject = JsonConvert.DeserializeObject<Message>(msgString);

                // TODO: Now should upload to Databases
                mongo.InsertTemperatureReadingToDatabase(messageObject);
            });
        }
    }
}
