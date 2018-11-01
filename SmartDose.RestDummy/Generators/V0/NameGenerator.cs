using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartDose.RestDummy.Generators.V0
{
    public class Names
    {
        public List<string> Lastnames { get; set; }

        public List<string> Firstnames { get; set; }
    }

    public static class NameGenerator
    {
        private static Names s_names = null;
        private static Random s_random = new Random();
        public static Names Names
        {
            get => s_names ?? (s_names = JsonConvert.DeserializeObject<Names>(RestDummyGlobals.ReadFromResource($"{typeof(NameGenerator).Namespace}.Names.json")));
        }

        public static (string FirstName, string LastName) Random()
            => (Names.Firstnames[s_random.Next(Names.Firstnames.Count)], Names.Lastnames[s_random.Next(Names.Lastnames.Count)]);
    }
}
