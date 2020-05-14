using MyMqtt;
using System.Threading.Tasks;

namespace TempDevice
{
    class Program
    {

        const string _serialNoPnv = "596128";
        const string _serialNoVln = "593116";

        static void Main(string[] args)
        {
            MyMqttClient mqttClient = new MyMqttClient();

            var pnvDevice = new Device(mqttClient, _serialNoPnv);
            Task.Run(pnvDevice.StartWorkAsync);

            var vlnDevice = new Device(mqttClient, _serialNoVln);
            Task.Run(vlnDevice.StartWorkAsync);
        }
    }
}
