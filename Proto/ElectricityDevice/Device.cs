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
        public static int _interval = 900000;
        const string _devicePublishTopic = "data/electricity";

        // Vars
        private MyMqttClient _mqttClient;
        private ICanStoreCounter _counterStore;
        private string _serialNo;
        private string _deviceName;

        private double _counter;
        private DateTime _time;

        public Device(MyMqttClient mqttClient, string serialNo, string deviceName)
        {
            _serialNo = serialNo;
            _deviceName = deviceName;
            _mqttClient = mqttClient;
            _counterStore = new MongoDBCounter();

            var config = _counterStore.GetConfigForDevice(_serialNo).Result;
            _counter = config.Value;
            _time = config.Time;
        }

        public async Task StartWorkAsync(DateTime endDate)
        {
            while (_time < endDate)
            {
                double electricityIncrease = GenerateElectricityIncrease();
                int timeIncrease = GenerateTimeIncrease();

                IncreaseCounter(electricityIncrease);
                IncreaseTime(timeIncrease);
                await _counterStore.StoreConfigForDevice(_serialNo, _time, _counter);

                var mes = new Message
                {
                    SerialNo = _serialNo,
                    DeviceName = _deviceName,
                    Timestamp = _time,
                    Value = _counter
                };

                var mesToSend = JsonConvert.SerializeObject(mes);

                await _mqttClient.PublishAsync(_devicePublishTopic, mesToSend);
            }
            Console.WriteLine(_serialNo + " done.");
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

        private int GenerateTimeIncrease()
        {
            return Helpers.GetRandomInteger(_interval - 120000, _interval + 120000);
        }

        private void IncreaseCounter(double increase)
        {
            _counter += increase;
        }

        private void IncreaseTime(int increase)
        {
            _time = _time.AddMilliseconds(increase);
        }
    }
}
