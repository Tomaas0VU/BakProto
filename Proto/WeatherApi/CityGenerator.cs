using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi
{
    public class CityGenerator
    {
        public List<City> Cities { get; set; }

        public async Task LoadFile()
        {
            using (StreamReader file = File.OpenText(@"city.list.json"))
            {
                var a = await file.ReadToEndAsync();
                Cities = JsonConvert.DeserializeObject<List<City>>(a);
            }
        }

        public List<City> GetAllLithuanianCities()
        {
            return Cities.Where(a => a.country.Equals("LT")).ToList();
        }
    }
}
