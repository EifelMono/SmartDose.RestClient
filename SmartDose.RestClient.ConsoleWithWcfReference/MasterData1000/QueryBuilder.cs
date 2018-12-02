using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serialize.Linq.Extensions;
using SmartDose.Core.Extensions;

namespace MasterData1000
{

    public class QueryBuilder
    {
        public enum QueryRequestOrderByAs
        {
            None = 0,
            Int = 1,
            String = 2,
            Long = 3
        }

        public enum QueryRequestResultAs
        {
            None = 0,
            Item = 1,
            List = 2
        }

        public ServiceClientBase Client { get; set; }

        public string WhereAsJson { get; set; } = string.Empty;
        public string OrderByAsJson { get; set; } = string.Empty;

        public bool OrderByAsc { get; set; } = true;

        public QueryRequestOrderByAs OrderByAs { get; set; } = QueryRequestOrderByAs.None;

        public QueryRequestResultAs ResultAs { get; set; } = QueryRequestResultAs.None;

        public Type ModelType { get; protected set; }

        public int Page { get; protected set; } = -1;
        public int PageSize { get; protected set; } = -1;
    }

    public class QueryBuilder<TModel> : QueryBuilder where TModel : class
    {
        public QueryBuilder()
        {
            ModelType = typeof(TModel);
        }

        public QueryBuilder<TModel> Where(Expression<Func<TModel, bool>> whereExpression)
        {
            WhereAsJson = whereExpression.ToJson();
            return this;
        }

        public QueryBuilder<TModel> OrderBy(Expression<Func<TModel, string>> orderByExpression)
        {
            OrderByAsJson = orderByExpression.ToJson();
            OrderByAsc = true;
            OrderByAs = QueryRequestOrderByAs.String;
            return this;
        }

        public QueryBuilder<TModel> OrderByDescending(Expression<Func<TModel, string>> orderByExpression)
        {
            OrderByAsJson = orderByExpression.ToJson();
            OrderByAsc = false;
            OrderByAs = QueryRequestOrderByAs.String;
            return this;
        }
        public QueryBuilder<TModel> OrderBy(Expression<Func<TModel, int>> orderByExpression)
        {
            OrderByAsJson = orderByExpression.ToJson();
            OrderByAs = QueryRequestOrderByAs.Int;
            return this;
        }

        public QueryBuilder<TModel> OrderBy(Expression<Func<TModel, long>> orderByExpression)
        {
            OrderByAsJson = orderByExpression.ToJson();
            OrderByAs = QueryRequestOrderByAs.Long;
            return this;
        }

        public QueryBuilder<TModel> Paging(int page = -1, int pageSize = -1)
        {
            Page = page;
            PageSize = pageSize;
            return this;
        }

        protected async Task<ServiceResult<TResult>> ExecuteAsync<TResult>() where TResult : class
        {
            var executeServiceResult = await Client.ExecuteQueryBuilderAsync(this).ConfigureAwait(false);
            var returnResult = executeServiceResult.CastByClone<ServiceResult<TResult>>();
            if (executeServiceResult.IsOk)
                returnResult.Data = (executeServiceResult.Data as string).UnZipString().ToObjectFromJson<TResult>();
            return returnResult;
        }

        public async Task<ServiceResult<List<TModel>>> ToListAsync()
        {
            ResultAs = QueryRequestResultAs.List;
            return await ExecuteAsync<List<TModel>>().ConfigureAwait(false);
        }

        public async Task<ServiceResult<TModel>> FirstOrDefaultAsync()
        {
            ResultAs = QueryRequestResultAs.Item;
            return await ExecuteAsync<TModel>().ConfigureAwait(false);
        }
    }
}
