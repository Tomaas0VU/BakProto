using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TempDevice.WeatherApi.Models;

namespace TempDevice.WeatherApi
{
    public class WeatherApiClient
    {
        const string appid = "1b4825408776f438641499767a4a148d";
        const string url = "http://api.openweathermap.org/data/2.5/weather?id=[CityId]&appid=[ApiKey]";

        public static string ApiUrl { get { return url.Replace("[ApiKey]", appid); } }

        public WeatherApiClient()
        {

        }

        public async Task<double> GetTemperatureFromApiAsync(string cityId)
        {
            string url = ApiUrl.Replace("[CityId]", cityId);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(json);
                return weatherResponse.main.temp - 273.15;
            }
        }
    }
}
