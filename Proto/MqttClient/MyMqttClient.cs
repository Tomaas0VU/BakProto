using System.Net.Mqtt;
using System.Text;

namespace MyMqttClient
{
    public class MyMqttClient
    {
        const string _mqttHostname = "localhost";

        private IMqttClient _mqttClient;

        public MyMqttClient()
        {
            _mqttClient = MqttClient.CreateAsync(_mqttHostname).Result;
            var sess = _mqttClient.ConnectAsync().Result;
        }

        public void Publish(string topic, string message)
        {
            var data = Encoding.UTF8.GetBytes(message);

            _mqttClient.PublishAsync(new MqttApplicationMessage(topic, data), MqttQualityOfService.ExactlyOnce).Wait();
        }
    }
}
