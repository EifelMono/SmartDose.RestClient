using System;
using Models = SmartDose.RestDomain.Models.V1;
using SmartDose.RestClientApp.Generators.V0;

namespace SmartDose.RestClientApp.Generators.V1
{
    public class DestinationFacilityGenerator
    {
        private static readonly Random s_random = new Random();
        public Models.Production.DestinationFacility New()
        {
            var customer = new Models.Production.Customer
            {
                CustomerId = new Random().Next(1, 999999).ToString(),
                Name = "BD Rowa Germany GmbH DestinationFacility",
                ContactPerson = new Models.Production.ContactPerson
                {
                    Email = "Johanna.Blisterfrau@bd.com",
                    Name = "Johanna Blisterfrau",
                    TelephoneNumber = "0269292060"
                },
                ContactAddress = new Models.Production.ContactAddress
                {
                    Addressline1 = "Rowastraße 1-3",
                    NameLine1 = "BD Rowa Germany GmbH",
                    City = "Kelberg",
                    Country = "Germany",
                    Postalcode = "53539",
                    State = "Rheinland-Pfalz"
                }
            };
            var departmentItem = DepartmentGenerator.Random();
            var destinationFacility = new Models.Production.DestinationFacility
            {
                DepartmentCode = departmentItem.Code,
                DepartmentName = departmentItem.Name,
                Name = customer.Name,
                ContactAddress = customer.ContactAddress,
                ContactPerson = customer.ContactPerson,
                CustomerId = customer.CustomerId
            };
            return destinationFacility;
        }
    }
}
