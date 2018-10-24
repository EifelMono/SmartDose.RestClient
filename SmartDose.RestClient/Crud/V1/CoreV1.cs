using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.RestClient.Crud.V1
{
    public class CoreV1<T> : Core<T> where T : class
    {
        public CoreV1(params string[] pathSegments) : base(UrlConfig.UrlV1, pathSegments)
        {

        }
    }

    public class CoreCrudV1<T> : CoreCrud<T> where T : class
    {
        public CoreCrudV1(params string[] pathSegments) : base(UrlConfig.UrlV1, pathSegments)
        {

        }
    }
}
