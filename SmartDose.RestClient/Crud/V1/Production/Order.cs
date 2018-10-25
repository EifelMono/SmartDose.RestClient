using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestClient.Crud.V1.Production
{
    public class Order : CoreV1<Models.Production.ExternalOrder>
    {
        public Order() : base(nameof(Order) + "s")
        {
        }

        public static Order Instance => Instance<Order>();

        public async Task<SdrcFlurHttpResponse<Models.Production.SingleOrderResultSet>> GetOrderResult(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("Result").AppendPathSegment(orderId).SdrcGetJsonAsync<Models.Production.SingleOrderResultSet>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.Production.BasicOrderInfo>> ReadAsync(string readId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(readId).SdrcGetJsonAsync<Models.Production.BasicOrderInfo>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<Models.Production.BasicOrderInfo>>> ReadListAsync(string customerId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("Result").AppendPathSegment("Usage").AppendPathSegment(customerId).SdrcGetJsonAsync<List<Models.Production.BasicOrderInfo>>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<Models.Production.ExternalOrder>>> LifeOrdersAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("LifeOrders").SdrcGetJsonAsync<List<Models.Production.ExternalOrder>>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<Models.Production.ExternalOrder>>> ArchivedOrdersAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("ArchievedOrders").SdrcGetJsonAsync<List<Models.Production.ExternalOrder>>(cancellationToken, completionOption).ConfigureAwait(false);

    }
}
