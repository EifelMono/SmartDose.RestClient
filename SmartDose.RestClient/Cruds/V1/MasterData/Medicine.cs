using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestClient.Cruds.V1.MasterData
{
    public class Medicine : CoreV1Crud<Models.MasterData.Medicine>
    {
        public Medicine() : base(nameof(Medicine) + "s")
        {
        }

        public static Medicine Instance => Instance<Medicine>();

        public async Task<SdrcFlurHttpResponse<int>> GetCanisterCountAsync(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments(medicineCode, "CanisterCount")
                .SdrcGetJsonAsync<int>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
