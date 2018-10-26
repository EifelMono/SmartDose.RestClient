using System;
using System.Collections.Generic;
using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.MasterData
#else
namespace SmartDose.RestDomain.Models.V1.MasterData
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class Medicine
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public Synonym[] SynonymIds { get; set; } = new Synonym[] { };
        public PrintDetail[] PrintDetails { get; set; } = new PrintDetail[] { };
        public MedicinePicture[] MedicinePictures { get; set; } = new MedicinePicture[] { };
        public SpecialHandling SpecialHandling { get; set; }
        public bool TrayFillOnly { get; set; }

        public override string ToString()
            => $"{Name} ({Identifier})";

        public static Medicine Dummy(string identifier, string name = null)
        {
            identifier = identifier ?? Guid.NewGuid().ToString();
            return new Medicine
            {

                Identifier = identifier,
                Name = string.IsNullOrEmpty(name) ? $"Medicine {identifier}" : name,
                Description = $"Med Desc {identifier}",
                Comment = $"Comment {identifier}",
                Active = true,
                SynonymIds = new Synonym[] { },
                PrintDetails = new PrintDetail[] { },
                MedicinePictures = new MedicinePicture[] { },
                SpecialHandling = new SpecialHandling
                {
                    Narcotic = true,
                    SeparatePouch = false,
                    MaxAmountPerPouch = 4,
                    RoboticHandling = false,
                    NeedsCooling = false,
                    Splittable = true
                },
                TrayFillOnly = false,
            };
        }
    }
}
