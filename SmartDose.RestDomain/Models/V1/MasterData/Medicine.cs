﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

#if RestDomainDev
using SmartDose.RestDomainDev.PropertyEditorThings;
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
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<Synonym> SynonymIds { get; set; } = new List<Synonym>();
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<PrintDetail> PrintDetails { get; set; } = new List<PrintDetail>();
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<MedicinePicture> MedicinePictures { get; set; } = new List<MedicinePicture>();
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
