using DT.Core.Web.Common.Api.WebApi.Filters;
using DT.Core.Web.Common.Api.WebApi.Formatter;
using FluentValidation.WebApi;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace DocumentManagement.Mvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
#if DEBUG
            // Pretty json for developers.
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
#else
                 config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
#endif
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter());

            config.Filters.Add(new DtExceptionFilterAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
