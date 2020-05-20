using MqttSubscriber.NET.Subscribers;
using MqttSubscriber.NET.Subscribers.Interfaces;
using System;
using System.Threading.Tasks;

namespace MqttSubscriber.NET
{
    class Program
    {
        static string _hostname = "127.0.0.1";

        static void Main(string[] args)
        {
            ITopicSubscriber temperatureSubscriber = new TemperatureTopicSubscriber(_hostname);
            Task.Run(temperatureSubscriber.SubscribeAsync);

            ITopicSubscriber electricitySubscriber = new ElectricityTopicSubscriber(_hostname);
            Task.Run(electricitySubscriber.SubscribeAsync);

            Console.ReadKey();
        }
    }
}
