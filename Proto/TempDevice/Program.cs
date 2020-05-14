using Models;
using Newtonsoft.Json;
using System;
using System.Net.Mqtt;
using System.Text;
using TempDevice.WeatherApi;

namespace TempDevice
{
    class Program
    {
        // Params
        //public static int _interval = 300000;
        public static int _interval = 5000;
        const string _mqttHostname = "localhost";
        const string _devicePublishTopic = "data/temperature";

        const string _serialNo = "596128"; // Panevėžys
        //const string _serialNo = "593116"; // Vilnius

        // Weather api params
        const string appid = "1b4825408776f438641499767a4a148d";
        const string url = "http://api.openweathermap.org/data/2.5/weather?id=[CityId]&appid=[ApiKey]";

        // Global vars
        private static IMqttClient _mqttClient;

        public static string ApiUrl { get { return url.Replace("[ApiKey]", appid); } }

        static void Main(string[] args)
        {
            _mqttClient = MqttClient.CreateAsync(_mqttHostname).Result;
            var sess = _mqttClient.ConnectAsync().Result;

            var weatherClient = new WeatherApiClient(ApiUrl);

            while (true)
            {
                double temp = weatherClient.GetTemperatureFromApi(_serialNo);

                var mes = new Message
                {
                    SerialNo = _serialNo,
                    Timestamp = DateTime.Now,
                    Value = temp
                };

                var mesToSend = JsonConvert.SerializeObject(mes);

                Publish(_devicePublishTopic, mesToSend);

                System.Threading.Thread.Sleep(_interval);
            }
        }

        private static void Publish(string topic, string message)
        {
            var data = Encoding.UTF8.GetBytes(message);

            _mqttClient.PublishAsync(new MqttApplicationMessage(topic, data), MqttQualityOfService.ExactlyOnce).Wait();
        }
    }
}
