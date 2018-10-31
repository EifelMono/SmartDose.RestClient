using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartDose.RestClientApp.Globals;

namespace SmartDose.RestClientApp.Generators.V0
{
    public class Names
    {
        public List<string> Lastnames { get; set; }

        public List<string> Firstnames { get; set; }
    }

    public static class NamesGenerator
    {
        private static Names s_names = null;
        private static Random s_random = new Random();
        public static Names Names
        {
            get => s_names ?? (s_names = JsonConvert.DeserializeObject<Names>(AppGlobals.ReadFromResource($"{typeof(NamesGenerator).Namespace}.Names.json")));
        }

        public static (string FirstName, string LastName) RandomNames()
            => (Names.Firstnames[s_random.Next(Names.Firstnames.Count)], Names.Lastnames[s_random.Next(Names.Lastnames.Count)]);
    }
}
