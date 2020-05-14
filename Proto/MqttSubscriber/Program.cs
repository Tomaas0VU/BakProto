using MqttSubscriber.Subscribers;
using MqttSubscriber.Subscribers.Interfaces;
using System;
using System.Threading.Tasks;

namespace MqttSubscriber
{
    class Program
    {
        static string _hostname = "localhost";

        static void Main(string[] args)
        {
            ITopicSubscriber temperatureSubscriber = new TemperatureTopicSubscriber(_hostname);
            Task.Run(temperatureSubscriber.SubscribeAsync);

            Console.ReadKey();
        }
    }
}
