using Abp.Localization;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DocumentManagement.Application;
using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Persistence;
using DT.Core.Application.Validation;
using DT.Core.Web.Common.Validation;
using FluentValidation;
using FluentValidation.Mvc;
using MediatR;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Globalization;
using System.Reflection;
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

            builder.RegisterAssemblyTypes(typeof(CreateDocumentCommandValidator).Assembly)
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<FluentValidationModelValidatorProvider>()
                .As<ModelValidatorProvider>()
                .SingleInstance();

            builder.RegisterType<FluentValidation.WebApi.FluentValidationModelValidatorProvider>()
                .As<System.Web.Http.Validation.ModelValidatorProvider>()
                .SingleInstance();

            builder.RegisterType<AutofacValidatorFactory>()
                .As<IValidatorFactory>()
                .SingleInstance();
 
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

            return container;
        }
    }
}