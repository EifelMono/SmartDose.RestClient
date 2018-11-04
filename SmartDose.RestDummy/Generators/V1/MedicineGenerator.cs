using System;
using System.Collections.Generic;
using System.Text;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestDummy.Generators.V1
{
    public class MedicineGenerator
    {
        public static Models.MasterData.Medicine New(string identifier, string name = null)
        {
            identifier = identifier ?? Guid.NewGuid().ToString();
            return new Models.MasterData.Medicine
            {

                Identifier = identifier,
                Name = string.IsNullOrEmpty(name) ? $"Medicine {identifier}" : name,
                Description = $"Med Desc {identifier}",
                Comment = $"Comment {identifier}",
                Active = true,
                SpecialHandling = new Models.MasterData.SpecialHandling
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
