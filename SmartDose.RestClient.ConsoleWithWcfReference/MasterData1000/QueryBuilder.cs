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

        protected ServiceClientBase Client { get; set; }

        protected string WhereAsJson { get; set; } = string.Empty;
        protected string OrderByAsJson { get; set; } = string.Empty;

        protected bool OrderByAsc { get; set; } = true;

        protected QueryRequestOrderByAs OrderByAs { get; set; } = QueryRequestOrderByAs.None;

        protected QueryRequestResultAs ResultAs { get; set; } = QueryRequestResultAs.None;

        protected Type ModelType { get; set; }

        protected int Page { get; set; } = -1;
        protected int PageSize { get; set; } = -1;

        // Use deconstructor while properties are protected
        public (string WhereAsJson,
            string OrderByAsJson,
            bool OrderByAsc,
            QueryRequestOrderByAs OrderByAs,
            QueryRequestResultAs ResultAs,
            Type ModelType,
            int Page,
            int PageSize) GetValues()
            => (WhereAsJson, OrderByAsJson, OrderByAsc, OrderByAs, ResultAs, ModelType, Page, PageSize);
    }

    public class QueryBuilder<TModel> : QueryBuilder where TModel : class
    {
        public QueryBuilder(ServiceClientBase client)
        {
            Client = client;
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
            OrderByAsc = true;
            OrderByAs = QueryRequestOrderByAs.Int;
            return this;
        }

        public QueryBuilder<TModel> OrderByDescending(Expression<Func<TModel, int>> orderByExpression)
        {
            OrderByAsJson = orderByExpression.ToJson();
            OrderByAsc = false;
            OrderByAs = QueryRequestOrderByAs.Int;
            return this;
        }

        public QueryBuilder<TModel> OrderBy(Expression<Func<TModel, long>> orderByExpression)
        {
            OrderByAsJson = orderByExpression.ToJson();
            OrderByAsc = true;
            OrderByAs = QueryRequestOrderByAs.Long;
            return this;
        }

        public QueryBuilder<TModel> OrderByDescending(Expression<Func<TModel, long>> orderByExpression)
        {
            OrderByAsJson = orderByExpression.ToJson();
            OrderByAsc = false;
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
