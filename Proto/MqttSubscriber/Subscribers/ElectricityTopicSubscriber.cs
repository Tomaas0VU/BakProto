﻿using Models;
using MqttSubscriber.Database;
using MqttSubscriber.Database.Interfaces;
using MqttSubscriber.Subscribers.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace MqttSubscriber.Subscribers
{
    public class ElectricityTopicSubscriber : ITopicSubscriber
    {
        private string _hostname;
        private string _topic = "data/electricity";

        public ElectricityTopicSubscriber(string hostname)
        {
            _hostname = hostname;
        }

        public async Task SubscribeAsync()
        {
            IDatabase mongo = new MongoDBClient();

            var mqttClient = MqttClient.CreateAsync(_hostname).Result;
            var sess = mqttClient.ConnectAsync().Result;
            await mqttClient.SubscribeAsync(_topic, MqttQualityOfService.ExactlyOnce);
            mqttClient.MessageStream.Subscribe(msg =>
            {
                string msgString = Encoding.UTF8.GetString(msg.Payload);
                Console.WriteLine(String.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), msgString));

                Message messageObject = JsonConvert.DeserializeObject<Message>(msgString);

                mongo.InsertElectricityReadingToDatabase(messageObject);
            });
        }
    }
}
