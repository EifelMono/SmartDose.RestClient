using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestClient.Crud.V2.Inventory
{
    public class StockBottle : CoreV2<Models.Inventory.StockBottle>
    {
        public StockBottle() : base(InventoryName, nameof(StockBottle) + "s")
        {
        }

        public static StockBottle Instance => Instance<StockBottle>();

        public async Task<SdrcFlurHttpResponse> CreateAsync(Models.Inventory.StockBottle value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone
                .SdrcPostJsonAsync(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.Inventory.StockBottle>> ReadAsync(string readId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(readId)
                .SdrcGetJsonAsync<Models.Inventory.StockBottle>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse> UpdateAsync(Models.Inventory.StockBottle value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.SdrcPutJsonAsync(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse> DeleteAsync(string deleteId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(deleteId)
                .SdrcDeleteAsync(cancellationToken, completionOption).ConfigureAwait(false);

    }
}
