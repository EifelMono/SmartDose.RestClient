using System;
using System.Net;

namespace SmartDose.RestClient.Extensions
{
    public class SdrcFlurHttpResponse
    {
        public bool Ok => StatusCode.IsHttpStatusCode(HttpStatusCode.OK);
        public HttpStatusCode StatusCode { get; set; } = (HttpStatusCode)(SdrcHttpStatusCode.Undefined);
        public Exception Exception { get; set; } = null;
        public string Message { get; set; } = "";
        public object Data { get; set; }
    }

    public class SdrcFlurHttpResponse<T> : SdrcFlurHttpResponse
    {
        public new T Data { get => (T)base.Data; set => base.Data = value; }
    }
}
