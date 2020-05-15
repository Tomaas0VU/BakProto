using ElectricityDevice.Counter.Interfaces;
using ElectricityDevice.Counter.MongoDB;
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
        const string _deviceCounterFileFormat = ".txt";

        // Vars
        private MyMqttClient _mqttClient;
        private ICanStoreCounter _counterStore;
        private string _serialNo;
        private string _deviceName;

        private double _counter;

        public Device(MyMqttClient mqttClient, string serialNo, string deviceName)
        {
            _serialNo = serialNo;
            _deviceName = deviceName;
            _mqttClient = mqttClient;
            _counterStore = new MongoDBCounter();

            _counter = _counterStore.GetCounterForDevice(_serialNo).Result;
        }

        public async Task StartWorkAsync()
        {
            while (true)
            {
                double electricityIncrease = GenerateElectricityIncrease();

                IncreaseCounter(electricityIncrease);
                await _counterStore.StoreCounterForDevice(_serialNo, _counter);

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

        private void IncreaseCounter(double increase)
        {
            _counter += increase;
        }
    }
}
