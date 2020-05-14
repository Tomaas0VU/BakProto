using MyMqtt;
using System.Threading.Tasks;
using WeatherApi;
using WeatherApi.Models;

namespace TempDevice
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMqttClient mqttClient = new MyMqttClient();

            var generator = new CityGenerator();
            generator.LoadFile().Wait();
            foreach (City city in generator.GetAllLithuanianCities())
            {
                var device = new Device(mqttClient, city.id.ToString(), "TempDevice" + city.name.ToString());
                Task.Run(device.StartWorkAsync);
            }
        }
    }
}
