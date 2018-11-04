using System;
using SmartDose.RestDummy.Generators.V0;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestDummy.Generators.V1
{
    public class PatientGenerator
    {
        private static Random s_random = new Random();
        public static Models.Production.Patient New()
        {
#pragma warning disable IDE0042 // Deconstruct variable declaration
            var name = NameGenerator.Random();
            var city = CityGenerator.Random();
#pragma warning restore IDE0042 // Deconstruct variable declaration
            var patient = new Models.Production.Patient
            {
                ContactPerson = new Models.Production.ContactPerson
                {
                    Name = $"{name.FirstName} {name.LastName}",
                    Email = $"{name.FirstName}.{name.LastName}@generatedpatient.com",
                    TelephoneNumber = $"{city.AreCode}/{s_random.Next(10, 9999999)}"
                },
                DateOfBirth = BirthdayGenerator.RandomBirthday().ToString("s"),
                BedNumber = $"Bed {s_random.Next(1, 3)}",
                RoomNumber = $"Room {s_random.Next(1000, 999)}",
                WardName = WardGenerator.Random(),
                Gender = Models.Production.Gender.Undefined,
                ExternalPatientNumber = $"{name.FirstName.Substring(0, 1)}{name.LastName.Substring(0, 1)}_{s_random.Next(10000, 99999)}",
                ContactAddress = new Models.Production.ContactAddress
                {
                    NameLine1 = $"{name.FirstName} {name.LastName}",
                    Addressline1 = "Generated Patient Street 1",
                    State = city.State,
                    City = city.Name,
                    Postalcode = city.Cip,
                    Country = city.Country
                }
            };
            return patient;
        }
    }
}
