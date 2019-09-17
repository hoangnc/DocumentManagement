using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Localization.Sources;
using Abp.Web.Configuration;
using Abp.Web.Localization;
using Autofac;
using Autofac.Integration.Mvc;
using DocumentManagement.Mvc.Mapper;
using DocumentManagement.Mvc.Services;
using DT.Core.Application.Validation;
using DT.Core.Authorization;
using DT.Core.Localization;
using DT.Core.Web.Common;
using DT.Core.Web.Common.Mvc.Controllers;
using DT.Core.Web.Localization;
using DT.Core.Web.Ui.Navigation;
using LazyCache;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using System.Web;

namespace DocumentManagement.Mvc
{
    public class DocumentManagementMvcModule : Autofac.Module
    {
        public DocumentManagementMvcModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            AutoMapperConfiguration.Initialize();

            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>))
               .As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(RequestValidationBehavior<,>))
              .As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                IComponentContext c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterType<PermissionChecker>()
                .As<IPermissionChecker>()
                .SingleInstance();

            ILocalizationConfiguration localization = new LocalizationConfiguration();
            localization.Languages.Add(new LanguageInfo("en", "English", icon: "famfamfam-flag-england", isDefault: false));
            localization.Languages.Add(new LanguageInfo("vi-VN", "Tiếng Việt", icon: "famfamfam-flag-vn", isDefault: true));
            DictionaryBasedLocalizationSource localizationSource = new DictionaryBasedLocalizationSource(
                "Document",
                new JsonFileLocalizationDictionaryProvider(
                    HttpContext.Current.Server.MapPath("~/Localization/Document")));

            localization.Sources.Add(localizationSource);

            builder.Register(c => localizationSource)
               .As<ILocalizationSource>()
               .SingleInstance();

            builder.Register(c => localization)
                .As<ILocalizationConfiguration>()
                .SingleInstance();

            builder.Register(c => new AbpWebLocalizationConfiguration())
                .As<IAbpWebLocalizationConfiguration>()
                .SingleInstance();

            builder.Register(c => new CurrentCultureSetter())
                .As<ICurrentCultureSetter>()
                .SingleInstance();

            builder.RegisterType<DefaultLanguageProvider>()
                .As<ILanguageProvider>()
                .SingleInstance();

            builder.RegisterType<LanguageManager>()
                .As<ILanguageManager>()
                .SingleInstance();

            builder.RegisterModule(new DTWebCommonModule(localization));

            builder.Register(c => new LocalizationManager(c.Resolve<ILanguageManager>(),
                                         c.Resolve<ILocalizationConfiguration>(), null))
                .As<ILocalizationManager>()
                .OnActivated(e => e.Instance.Initialize())
                .SingleInstance();

            builder.RegisterType<MenuConfigurationContext>()
               .As<IMenuConfigurationContext>()
               .InstancePerRequest();

            builder.RegisterType<MenuManager>()
                .As<IMenuManager>()
                .InstancePerRequest();

            IAppCache cache = new CachingService(CachingService.DefaultCacheProvider);
            cache.DefaultCachePolicy.DefaultCacheDurationSeconds = 60 * 36000;

            builder.Register(c => cache)
                .As<IAppCache>()
                .SingleInstance();

            builder.RegisterControllers(typeof(LocalizationController).Assembly);

            base.Load(builder);
        }
    }
}