using System;
using System.Net;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace TestApplication.Shared
{
    [ApiController]
    public class RevControllerBase<TService> : RevControllerBase
    {
        protected TService Service { get; }

        protected RevControllerBase(TService service)
        {
            Service = service;
        }
    }

    
    [Produces("application/json")]
    public class RevControllerBase : ControllerBase
    {
        
        public string Host
        {
            get
            {
                var uri = HttpContext.Request.GetUri();
                var apiIndex = uri.AbsoluteUri.IndexOf("api", StringComparison.Ordinal);
                return uri.AbsoluteUri.Substring(0, apiIndex);
            }
        }
    }
}