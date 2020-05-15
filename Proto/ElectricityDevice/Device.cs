using Models;
using MyMqtt;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ElectricityDevice
{
    public class Device
    {
        // Params
        public static int _interval = 600000;
        const string _devicePublishTopic = "data/electricity";

        // Vars
        private MyMqttClient _mqttClient;
        private string _serialNo;
        private string _deviceName;

        private double _counter;

        public Device(MyMqttClient mqttClient, string serialNo, string deviceName)
        {
            _serialNo = serialNo;
            _deviceName = deviceName;
            _mqttClient = mqttClient;

            GetCounterData();
        }

        public async Task StartWorkAsync()
        {
            while (true)
            {
                double electricityIncrease = GenerateElectricityIncrease();

                IncreaseCounter(electricityIncrease);
                SetCounterData();

                var mes = new Message
                {
                    SerialNo = _serialNo,
                    DeviceName = _deviceName,
                    Timestamp = DateTime.Now,
                    Value = _counter
                };

                var mesToSend = JsonConvert.SerializeObject(mes);

                await _mqttClient.PublishAsync(_devicePublishTopic, mesToSend);

                System.Threading.Thread.Sleep(_interval);
            }
        }

        private double GenerateElectricityIncrease()
        {
            var morningTimespan = new TimeSpan(8, 0, 0);
            var eveningTimespan = new TimeSpan(22, 0, 0);

            var time = DateTime.Now.TimeOfDay;
            if ((time >= morningTimespan) && (time < eveningTimespan))
            {
                return Helpers.GetRandomNumber(10, 30);
            }
            else
            {
                return Helpers.GetRandomNumber(3, 15);
            }
        }

        private void GetCounterData()
        {
            throw new NotImplementedException();
        }

        private void SetCounterData()
        {
            throw new NotImplementedException();
        }

        private void IncreaseCounter(double increase)
        {
            _counter += increase;
        }
    }
}
