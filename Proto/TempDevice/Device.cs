using Models;
using MyMqtt;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WeatherApi;

namespace TempDevice
{
    public class Device
    {
        // Params
        public static int _interval = 600000;
        const string _devicePublishTopic = "data/temperature";

        // Vars
        private MyMqttClient _mqttClient;
        private WeatherApiClient _weatherClient;
        private string _serialNo;
        private string _deviceName;

        public Device(MyMqttClient mqttClient, string serialNo, string deviceName)
        {
            _serialNo = serialNo;
            _deviceName = deviceName;
            _mqttClient = mqttClient;
            _weatherClient = new WeatherApiClient();
        }

        public async Task StartWorkAsync()
        {
            while (true)
            {
                double temp = await _weatherClient.GetTemperatureFromApiAsync(_serialNo);

                var mes = new Message
                {
                    SerialNo = _serialNo,
                    DeviceName = _deviceName,
                    Timestamp = DateTime.Now,
                    Value = temp
                };

                var mesToSend = JsonConvert.SerializeObject(mes);

                await _mqttClient.PublishAsync(_devicePublishTopic, mesToSend);

                System.Threading.Thread.Sleep(_interval);
            }
        }
    }
}
