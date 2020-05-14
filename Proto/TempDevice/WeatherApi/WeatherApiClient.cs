using System.IO;
using System.Net;
using Newtonsoft.Json;
using TempDevice.WeatherApi.Models;

namespace TempDevice.WeatherApi
{
    public class WeatherApiClient
    {
        private string ApiUrl { get; set; }

        public WeatherApiClient(string url)
        {
            ApiUrl = url;
        }

        public double GetTemperatureFromApi(string location)
        {
            string url = ApiUrl.Replace("[CityId]", location);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
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
