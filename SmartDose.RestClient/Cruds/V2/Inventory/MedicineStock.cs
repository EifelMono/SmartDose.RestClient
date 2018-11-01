using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestClient.Cruds.V2.Inventory
{
    public class MedicineStock : CoreV2<Models.Inventory.MedicineStock>
    {
        public MedicineStock() : base(InventoryName, "Medicines")
        {
        }

        public static MedicineStock Instance => Instance<MedicineStock>();

        public async Task<SdrcFlurHttpResponse<List<Models.Inventory.StockBottle>>> GetMedicineStocks(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone
                .SdrcGetJsonAsync<List<Models.Inventory.StockBottle>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.Inventory.StockBottle>> GetMedicineStock(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(medicineCode)
                .SdrcGetJsonAsync<Models.Inventory.StockBottle>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<List<Models.Inventory.StockBottle>>> GetStockBottles(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments(medicineCode, "StockBottles")
                .SdrcGetJsonAsync<List<Models.Inventory.StockBottle>>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
