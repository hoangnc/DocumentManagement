using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Localization.Sources;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DocumentManagement.Application;
using DocumentManagement.Persistence;
using DT.Core.Localization;
using DT.Core.Web.Common;
using MediatR;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DocumentManagement.Mvc
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
     
            // Register out persistence dependencies
            builder.RegisterModule(new DocumentManagementPersistenceModule(ConfigurationManager.ConnectionStrings["DocumentConnectionString"].ConnectionString));

            builder.RegisterModule(new DocumentManagementApplicationModule());

            // Register out DocumentManagment MVC dependencies
            builder.RegisterModule(new DocumentManagementMvcModule());
 
            // Get your HttpConfiguration.
            HttpConfiguration config = GlobalConfiguration.Configuration;

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(AutofacConfig).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register your Web API controllers.
            builder.RegisterApiControllers(typeof(AutofacConfig).Assembly);

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            builder.Register<ServiceFactory>(ctx =>
            {
                IComponentContext c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            IContainer container = builder.Build();
            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Set Web Api DI resolver to use our Autofac container
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            #if DEBUG
                 // Pretty json for developers.
                 GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
            #else
                 GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
            #endif
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");

            CultureInfo.CurrentCulture = new CultureInfo("vi-VN");
            CultureInfo.CurrentUICulture = new CultureInfo("vi-VN");
            return container;
        }
    }
}