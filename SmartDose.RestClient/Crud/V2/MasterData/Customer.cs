using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V2;


namespace SmartDose.RestClient.Crud.V2.MasterData
{
    public class Customer : CoreV2<Models.MasterData.Customer>
    {
        public Customer() : base(MasterDataName, nameof(Customer) + "s")
        {
        }

        public static Customer Instance => Instance<Customer>();
    }
}
