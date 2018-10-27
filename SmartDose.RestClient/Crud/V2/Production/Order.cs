using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestClient.Crud.V2.Production
{
    public class Order : CoreV2<Models.Production.Order>
    {
        public Order() : base(ProductionName, nameof(Order) + "s")
        {
        }

        public static Order Instance => Instance<Order>();

        public async Task<SdrcFlurHttpResponse> CreateAsync(Models.Production.Order value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone
                .SdrcPostJsonAsync(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse> DeleteAsync(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(orderId)
                .SdrcDeleteAsync(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.Production.OrderResult>> GetOrderResult(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments("Result", orderId)
                .SdrcGetJsonAsync<Models.Production.OrderResult>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.Production.OrderResultByTime>> GetOrderResultByTime(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments("ResultByTime", orderId)
                .SdrcGetJsonAsync<Models.Production.OrderResultByTime>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<Models.Production.OrderStatus>>> GetOrderStatus(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("Status")
                .SdrcGetJsonAsync<List<Models.Production.OrderStatus>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.Production.OrderStatus>> GetOrderStatus(string orderId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments("Status", orderId)
                .SdrcGetJsonAsync<Models.Production.OrderStatus>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
