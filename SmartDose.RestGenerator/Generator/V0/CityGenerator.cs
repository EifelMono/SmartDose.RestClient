using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartDose.RestGenerator.Generator.V0
{
    public class City
    {
        public string Name { get; set; }
        public string Cip { get; set; }
        public string AreCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; } = "Germany";
    }
    public class Cities
    {
        public List<City> Items { get; set; }
    }

    public static class CityGenerator
    {
        private static Cities s_cities = null;
        private static Random s_random = new Random();
        public static Cities Cities
        {
            get => s_cities ?? (s_cities = JsonConvert.DeserializeObject<Cities>(RestGeneratorGlobals.ReadFromResource($"{typeof(CityGenerator).Namespace}.Cities.json")));
        }

        public static City Random()
            => Cities.Items[s_random.Next(Cities.Items.Count)];
    }
}
