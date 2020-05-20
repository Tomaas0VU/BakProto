using MyMqtt;
using System;
using System.Threading.Tasks;

namespace ElectricityDevice
{
    class Program
    {
        const string _preDeviceName = "ElectDevice";

        static void Main(string[] args)
        {
            MyMqttClient mqttClient = new MyMqttClient();

            int startingPointOfSerialNumber = 12000;
            int howManyDevices = 10000;
            int goingToYear = 2011;
            int goingToMonth = 1;
            int goingToDay = 1;

            for (int i = startingPointOfSerialNumber; i < startingPointOfSerialNumber + howManyDevices; i++)
            {
                string id = i.ToString();
                var device = new Device(mqttClient, id, _preDeviceName + id);
                Task.Run(() => device.StartWorkAsync(new DateTime(goingToYear, goingToMonth, goingToDay)));
            }
        }
    }
}
