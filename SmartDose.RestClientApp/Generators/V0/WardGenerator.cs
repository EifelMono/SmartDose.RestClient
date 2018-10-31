﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartDose.RestClientApp.Globals;

namespace SmartDose.RestClientApp.Generators.V0
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
            get => s_wards ?? (s_wards = JsonConvert.DeserializeObject<Wards>(AppGlobals.ReadFromResource($"{typeof(WardGenerator).Namespace}.Wards.json")));
        }

        public static string Random()
            => Wards.Names[s_random.Next(Wards.Names.Count)];
    }
}
