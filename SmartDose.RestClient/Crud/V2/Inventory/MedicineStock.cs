using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using ModelsV2 = SmartDose.RestDomain.Models.V2;


namespace SmartDose.RestClient.Crud.V2.Inventory
{
    public class MedicineStock : CoreV2<ModelsV2.Inventory.MedicineStock>
    {
        public MedicineStock() : base(InventoryName, "Medicines")
        {
        }

        public static MedicineStock Instance => Instance<MedicineStock>();

        public async Task<SdrcFlurHttpResponse<List<ModelsV2.Inventory.StockBottle>>> GetStockBottles(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
                  => await Url.AppendPathSegment(medicineCode).AppendPathSegment("StockBottles").SdrcGetJsonAsync<List<ModelsV2.Inventory.StockBottle>>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
