﻿using System;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace SmartDose.RestClient.Extensions
{
    public class SdrcFlurHttpResponse
    {
        public DateTime ReceivedOn { get; set; } = DateTime.Now;
        public bool Ok => StatusCode.IsHttpStatusCode(HttpStatusCode.OK);
        public HttpStatusCode StatusCode { get; set; } = (HttpStatusCode)(SdrcHttpStatusCode.Undefined);

        public string StatusCodeAsString
        {
            get
            {
                switch ((int)StatusCode)
                {
                    case (int)SdrcHttpStatusCode.Undefined:
                        return SdrcHttpStatusCode.Undefined.ToString();
                    case (int)SdrcHttpStatusCode.FlurlTimeOut:
                        return SdrcHttpStatusCode.FlurlTimeOut.ToString();
                    case (int)SdrcHttpStatusCode.FlurlTaskCanceled:
                        return SdrcHttpStatusCode.FlurlTaskCanceled.ToString();
                    case (int)SdrcHttpStatusCode.FlurlException:
                        return SdrcHttpStatusCode.FlurlException.ToString();
                    case (int)SdrcHttpStatusCode.Exception:
                        return SdrcHttpStatusCode.Exception.ToString();
                    default: return StatusCode.ToString();
                }
            }
        }
        public Exception Exception { get; set; } = null;
        public string Message { get; set; } = "";
        public string Request { get; set; } = "";
        public object Data { get; set; }

        public SdrcFlurHttpResponse MarkReceived()
        {
            ReceivedOn = DateTime.Now;
            return this;
        }
    }

    public class SdrcFlurHttpResponse<T> : SdrcFlurHttpResponse
    {
        public new T Data { get => (T)base.Data; set => base.Data = value; }

        public new SdrcFlurHttpResponse<T> MarkReceived()
        {
            ReceivedOn = DateTime.Now;
            return this;
        }
    }
}
