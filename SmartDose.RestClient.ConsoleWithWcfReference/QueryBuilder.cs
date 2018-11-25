using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasterData1000;

namespace SmartDose.RestClient.ConsoleWithWcfReference
{
    public class QueryBuilder
    {
        public QueryRequest Request { get; set; } = new QueryRequest();

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

        protected async Task<ServiceResult<TResult>> ExecuteAsync<TResult>()
        {
            return await Client.ExecuteQueryBuilderAsync<ServiceResult<TResult>>(this).ConfigureAwait(false);
        }

        public async Task<ServiceResult<List<TModel>>> ToListAsync()
          => await ExecuteAsync<List<TModel>>().ConfigureAwait(false);

        public async Task<ServiceResult<TModel>> FirstOrDefaultAsync()
            => await ExecuteAsync<TModel>().ConfigureAwait(false);


    }
}
