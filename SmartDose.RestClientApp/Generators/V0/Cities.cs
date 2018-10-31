using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartDose.RestClientApp.Globals;

namespace SmartDose.RestClientApp.Generators.V0
{
    public class City
    {
        public string Name { get; set; }
        public string Cip { get; set; }
        public string AreCode { get; set; }
        public string State { get; set; }
    }
    public class Cities
    {
        public List<City> Items { get; set; }
    }

    public static class CityGenerator
    {
        private static Cities s_Cities = null;
        private static Random s_random = new Random();
        public static Cities Cities
        {
            get => s_Cities ?? (s_Cities = JsonConvert.DeserializeObject<Cities>(AppGlobals.ReadFromResource($"{typeof(CityGenerator).Namespace}.Cities.json")));
        }

        public static City RandomCity()
            => Cities.Items[s_random.Next(Cities.Items.Count)];
    }
}
