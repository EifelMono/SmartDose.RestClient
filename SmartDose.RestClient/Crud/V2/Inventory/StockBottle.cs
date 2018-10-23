using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V2;


namespace SmartDose.RestClient.Crud.V2.Inventory
{
    public class StockBottle : CoreV2<Models.Inventory.StockBottle>
    {
        public StockBottle() : base(InventoryName, nameof(StockBottle)+ "s")
        {
        }

        public static StockBottle Instance => Instance<StockBottle>();
    }
}
