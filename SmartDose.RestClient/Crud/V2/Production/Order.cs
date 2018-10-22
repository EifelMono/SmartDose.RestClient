using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using ModelsV2 = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestClient.Crud.V2.MasterData
{
    public class Order : Core<ModelsV2.Production.Order>
    {
        public Order() : base(ProductionName, nameof(Order) + "s")
        {
        }

        public static Order Instance => Instance<Order>();

        public async Task<SdrcFlurHttpResponse<ModelsV2.Production.OrderResult>> GetOrderResult(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment("Result").AppendPathSegment(orderId).SdrcGetJsonAsync<ModelsV2.Production.OrderResult>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<ModelsV2.Production.OrderResultByTime>> GetOrderResultByTime(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment("ResultByTime").AppendPathSegment(orderId).SdrcGetJsonAsync<ModelsV2.Production.OrderResultByTime>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<ModelsV2.Production.OrderStatus>>> GetOrderStatus(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment("Status").SdrcGetJsonAsync<List<ModelsV2.Production.OrderStatus>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<ModelsV2.Production.OrderStatus>> GetOrderStatus(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment("Status").AppendPathSegment(orderId).SdrcGetJsonAsync<ModelsV2.Production.OrderStatus>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
