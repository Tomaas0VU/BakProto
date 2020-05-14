﻿using Models;
using MyMqtt;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TempDevice.WeatherApi;

namespace TempDevice
{
    public class Device
    {
        // Params
        public static int _interval = 300000;
        const string _devicePublishTopic = "data/temperature";

        // Vars
        private MyMqttClient _mqttClient;
        private WeatherApiClient _weatherClient;
        private string _serialNo;

        public Device(MyMqttClient mqttClient, string serialNo)
        {
            _serialNo = serialNo;
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