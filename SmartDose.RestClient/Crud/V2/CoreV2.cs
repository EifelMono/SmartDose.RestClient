using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.RestClient.Crud.V2
{
    public class CoreV2<T> : Core<T> where T : class
    {
        public CoreV2(params string[] pathSegments) : base(UrlConfig.UrlV2, pathSegments)
        {

        }
    }

    public class CoreCrudV2<T> : CoreCrud<T> where T : class
    {
        public CoreCrudV2(params string[] pathSegments) : base(UrlConfig.UrlV2, pathSegments)
        {

        }
    }
}
