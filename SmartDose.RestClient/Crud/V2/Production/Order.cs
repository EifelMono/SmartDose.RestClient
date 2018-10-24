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
    public class Order : CoreV2<Models.Production.Order>
    {
        public Order() : base(ProductionName, nameof(Order) + "s")
        {
        }

        public static Order Instance => Instance<Order>();

        public async Task<SdrcFlurHttpResponse<Models.Production.OrderResult>> GetOrderResult(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment("Result").AppendPathSegment(orderId).SdrcGetJsonAsync<Models.Production.OrderResult>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.Production.OrderResultByTime>> GetOrderResultByTime(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment("ResultByTime").AppendPathSegment(orderId).SdrcGetJsonAsync<Models.Production.OrderResultByTime>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<Models.Production.OrderStatus>>> GetOrderStatus(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment("Status").SdrcGetJsonAsync<List<Models.Production.OrderStatus>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.Production.OrderStatus>> GetOrderStatus(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment("Status").AppendPathSegment(orderId).SdrcGetJsonAsync<Models.Production.OrderStatus>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
