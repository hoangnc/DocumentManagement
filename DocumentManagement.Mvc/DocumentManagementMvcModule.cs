using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Localization.Sources;
using Abp.Web.Configuration;
using Abp.Web.Localization;
using Autofac;
using Autofac.Integration.Mvc;
using DocumentManagement.Mvc.Services;
using DT.Core.Application.Validation;
using DT.Core.Authorization;
using DT.Core.Localization;
using DT.Core.Web.Common;
using DT.Core.Web.Common.Mvc.Controllers;
using DT.Core.Web.Localization;
using DT.Core.Web.Ui.Navigation;
using MediatR;
using MediatR.Pipeline;
using System.Web;

namespace DocumentManagement.Mvc
{
    public class DocumentManagementMvcModule : Module
    {
        public DocumentManagementMvcModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>))
               .As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(RequestValidationBehavior<,>))
              .As(typeof(IPipelineBehavior<,>));
                        
            builder.RegisterType<PermissionChecker>()
                .As<IPermissionChecker>()
                .InstancePerRequest();

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
                .SingleInstance();

            builder.RegisterType<MenuConfigurationContext>()
               .As<IMenuConfigurationContext>()
               .InstancePerRequest();

            builder.RegisterType<MenuManager>()
                .As<IMenuManager>()
                .InstancePerRequest();

            builder.RegisterControllers(typeof(LocalizationController).Assembly);

            base.Load(builder);
        }
    }
}