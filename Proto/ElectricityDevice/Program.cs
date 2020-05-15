using MyMqtt;
using System.Threading.Tasks;

namespace ElectricityDevice
{
    class Program
    {
        const string _preDeviceName = "ElectDevice";

        static void Main(string[] args)
        {
            MyMqttClient mqttClient = new MyMqttClient();

            for (int i = 11110; i < 11610; i += 5)
            {
                string id = i.ToString();
                var device = new Device(mqttClient, id, _preDeviceName + id);
                Task.Run(device.StartWorkAsync);
            }
        }
    }
}
