using DT.Core.Web.Common.Api.WebApi.Filters;
using DT.Core.Web.Common.Api.WebApi.ModelBinder;
using FluentValidation.WebApi;
using MultipartDataMediaFormatter;
using MultipartDataMediaFormatter.Infrastructure;
using Newtonsoft.Json.Serialization;
using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Validation;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

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

            // config.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter());
            config.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter(new MultipartFormatterSettings()));
            config.Filters.Add(new DtExceptionFilterAttribute());

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{action}"
           );
        }
    }
}
