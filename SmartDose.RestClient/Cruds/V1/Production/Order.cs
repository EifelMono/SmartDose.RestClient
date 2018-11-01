using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestClient.Cruds.V1.Production
{
    public class Order : CoreV1<Models.Production.ExternalOrder>
    {
        public Order() : base(nameof(Order) + "s")
        {
        }

        public static Order Instance => Instance<Order>();

        public async Task<SdrcFlurHttpResponse<Models.Production.SingleOrderResultSet>> GetOrderResultByIdentifierAsync(string identifier, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments("Result", identifier)
                .SdrcGetJsonAsync<Models.Production.SingleOrderResultSet>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.Production.BasicOrderInfo>> GetBasicOrderInfoByIdentifierAsync(string identifier, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(identifier)
                .SdrcGetJsonAsync<Models.Production.BasicOrderInfo>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.Production.BasicOrderInfo>> GetResultUsageByCustomerIdAsync(string customerId, string start, string end, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments("Result", "Usage", customerId)
                .SetQueryParam("start", start).SetQueryParam("end", end)
                .SdrcGetJsonAsync<Models.Production.BasicOrderInfo>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<Models.Production.ExternalOrder>>> GetLifeOrdersAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("LifeOrders")
                .SdrcGetJsonAsync<List<Models.Production.ExternalOrder>>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<Models.Production.ExternalOrder>>> GetArchivedOrdersAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("ArchievedOrders")
                .SdrcGetJsonAsync<List<Models.Production.ExternalOrder>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<List<Models.Production.BasicOrderInfo>>> GetOrdersAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone
                .SdrcGetJsonAsync<List<Models.Production.BasicOrderInfo>>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse> CreateAsync(Models.Production.RestExternalOrder value, bool checkMedicine = false, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
           => checkMedicine
            ? await UrlClone.WithHeader("CheckMedicines", "True")
                .SdrcPostJsonAsync(value, cancellationToken, completionOption).ConfigureAwait(false)
            : await UrlClone
                .SdrcPostJsonAsync(value, cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse> DeleteAsync(string identifier, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(identifier)
                .SdrcDeleteAsync(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
