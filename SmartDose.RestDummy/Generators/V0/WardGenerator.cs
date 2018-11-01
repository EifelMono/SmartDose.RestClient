using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartDose.RestDummy.Generators.V0
{
    public class Wards
    {
        public List<string> Names { get; set; }

    }

    public static class WardGenerator
    {
        private static Wards s_wards = null;
        private static Random s_random = new Random();
        public static Wards Wards
        {
            get => s_wards ?? (s_wards = JsonConvert.DeserializeObject<Wards>(RestDummyGlobals.ReadFromResource($"{typeof(WardGenerator).Namespace}.Wards.json")));
        }

        public static string Random()
            => Wards.Names[s_random.Next(Wards.Names.Count)];
    }
}
