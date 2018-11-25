using System;
using System.ServiceModel;
using System.Threading.Tasks;
using MasterData1000;

namespace SmartDose.RestClient.ConsoleWithWcfReference
{
    public abstract class ServiceClientCore : IDisposable
    {
        public string EndpointAddress { get; protected set; }
        public SecurityMode SecurityMode { get; protected set; }
        // Timeout?!

        public ServiceClientCore(string endpointAddress, SecurityMode securityMode = SecurityMode.None)
        {
            EndpointAddress = endpointAddress;
            SecurityMode = securityMode;
        }

        public ICommunicationObject Client { get; set; }

        public void Dispose()
        {
            // TODO
        }

        protected async Task<TResult> SafeExecuteAsync<TResult>(Func<Task<TResult>> func) where TResult : ServiceResult, new()
        {
            try
            {
                return await (func?.Invoke()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new TResult { Exception = ex, Status = ServiceResultStatus.ErrorConnection };
            }
        }

        #region Query

        public abstract Task<TResult> ExecuteQueryBuilderAsync<TResult>(QueryBuilder queryBuilder) where TResult : ServiceResult;


        public QueryBuilder<T> NewQuery<T>() where T : class
        {
            return new QueryBuilder<T> { Client = this };
        }

        #endregion
    }
}
