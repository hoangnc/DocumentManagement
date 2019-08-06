using DT.Core.Web.Common.Api.WebApi.Filters;
using DT.Core.Web.Common.Api.WebApi.Formatter;
using DT.Core.Web.Common.Api.WebApi.ModelBinder;
using FluentValidation.WebApi;
using Newtonsoft.Json.Serialization;
using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Validation;

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

            config.Services.Clear(typeof(ModelValidatorProvider));

            config.BindParameter(typeof(DateTime), new DateTimeModelBinder());
            config.BindParameter(typeof(DateTime?), new DateTimeModelBinder());

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
