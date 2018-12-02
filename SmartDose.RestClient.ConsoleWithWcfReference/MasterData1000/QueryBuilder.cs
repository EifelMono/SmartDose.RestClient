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
        public ServiceClientBase Client { get; set; }

        public string WhereAsJson { get; protected set; } = string.Empty;
        public string OrderByStringAsJson { get; protected set; } = string.Empty;
        public string OrderByIntAsJson { get; protected set; } = string.Empty;

        public bool ResultAsItem { get; protected set; } = false;
        public bool ResultAsList { get; protected set; } = false;

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
            OrderByStringAsJson = orderByExpression.ToJson();
            return this;
        }
        public QueryBuilder<TModel> OrderBy(Expression<Func<TModel, int>> orderByExpression)
        {
            OrderByIntAsJson = orderByExpression.ToJson();
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
            var returnResult = new ServiceResult<TResult>
            {
                Exception = executeServiceResult.Exception,
                Message = executeServiceResult.Message,
                Status = executeServiceResult.Status,
                StatusAsInt = executeServiceResult.StatusAsInt,
            };
            if (executeServiceResult.IsOk)
            {
                returnResult.Data = (executeServiceResult.Data as string).UnZipString().ToObjectFromJson<TResult>();
            }
            return returnResult;
        }

        public async Task<ServiceResult<List<TModel>>> ToListAsync()
        {
            ResultAsList = true;
            return await ExecuteAsync<List<TModel>>().ConfigureAwait(false);
        }

        public async Task<ServiceResult<TModel>> FirstOrDefaultAsync()
        {
            ResultAsItem = true;
            return await ExecuteAsync<TModel>().ConfigureAwait(false);
        }
    }
}
