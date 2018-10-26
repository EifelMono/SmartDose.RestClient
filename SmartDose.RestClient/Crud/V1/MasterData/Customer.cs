using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V1;


namespace SmartDose.RestClient.Crud.V1.MasterData
{
    public class Customer : CoreV1Crud<Models.MasterData.Customer>
    {
        public Customer() : base(nameof(Customer) + "s")
        {
        }

        public static Customer Instance => Instance<Customer>();
    }
}
