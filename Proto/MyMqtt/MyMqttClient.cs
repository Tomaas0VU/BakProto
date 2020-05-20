using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace MyMqtt
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

        public async Task PublishAsync(string topic, string message)
        {
            var data = Encoding.UTF8.GetBytes(message);

            await _mqttClient.PublishAsync(new MqttApplicationMessage(topic, data), MqttQualityOfService.ExactlyOnce);
        }
    }
}
