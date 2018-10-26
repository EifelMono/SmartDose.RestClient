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
    public class DestinationFacility : CoreV2Crud<Models.MasterData.DestinationFacility>
    {
        public DestinationFacility() : base(MasterDataName, "DestinationFacilities" )
        {
        }

        public static DestinationFacility Instance => Instance<DestinationFacility>();
    }
}
