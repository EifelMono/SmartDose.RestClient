using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDose.RestClient.ConsoleWithWcfReference
{
    public class QueryBuilder
    {
        public MasterData1000.QueryRequest Request { get; set; } = new MasterData1000.QueryRequest();

        public ServiceClientCore Client { get; set; }

        public int Page { get; set; } = -1;
        public int PageSize { get; set; } = -1;
    }

    public class QueryBuilder<TModel> : QueryBuilder where TModel : class
    {
        public QueryBuilder<TModel> Where(Expression<Func<TModel, bool>> whereExpression)
        {
            return this;
        }

        public QueryBuilder<TModel> OrderBy(Expression<Func<TModel, string>> OrderByExpression)
        {
            return this;
        }
        public QueryBuilder<TModel> OrderBy(Expression<Func<TModel, int>> OrderByExpression)
        {
            return this;
        }

        public QueryBuilder<TModel> Paging(int page = -1, int pageSize = -1)
        {
            Page = page;
            PageSize = pageSize;

            return this;
        }

        protected async Task<MasterData1000.ServiceResult<TResult>> ExecuteAsync<TResult>() where TResult : class
        {
            var executeServiceResult = await Client.ExecuteQueryBuilderAsync(this).ConfigureAwait(false);
            return executeServiceResult.CastByClone<MasterData1000.ServiceResult<TResult>>();
        }

        public async Task<MasterData1000.ServiceResult<List<TModel>>> ToListAsync()
          => await ExecuteAsync<List<TModel>>().ConfigureAwait(false);

        public async Task<MasterData1000.ServiceResult<TModel>> FirstOrDefaultAsync()
            => await ExecuteAsync<TModel>().ConfigureAwait(false);
    }
}
