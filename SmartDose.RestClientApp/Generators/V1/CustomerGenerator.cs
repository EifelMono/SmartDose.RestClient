using System;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestClientApp.Generators.V1
{
    public class CustomerGenerator
    {
        public Models.MasterData.Customer New()
        {
            var customer = new Models.MasterData.Customer
            {
                CustomerId = new Random().Next(1, 999999).ToString(),
                Name = "BD Rowa Germany GmbH",
                ContactPerson = new Models.MasterData.ContactPerson
                {
                    Email = "Max.Blistermann@bd.com",
                    Name = "Max Blistermann",
                    TelephoneNumber = "0269292062273"
                },
                ContactAddress = new Models.MasterData.ContactAddress
                {
                    Addressline1 = "In der Struht 3",
                    NameLine1 = "BD Rowa Germany GmbH R&D",
                    City = "Kelberg",
                    Country = "Germany",
                    Postalcode = "53539",
                    State = "Rheinland-Pfalz"
                }
            };
            return customer;
        }
    }
}
