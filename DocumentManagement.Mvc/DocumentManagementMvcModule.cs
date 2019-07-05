using Autofac;
using DocumentManagement.Application.Mapper;
using DocumentManagement.Mvc.Services;
using DT.Core.Authorization;
using DT.Core.Web.Ui.Navigation;
using MediatR;
using MediatR.Pipeline;

namespace DocumentManagement.Mvc
{
    public class DocumentManagementMvcModule : Module
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

            builder.RegisterType<MenuConfigurationContext>()
                .As<IMenuConfigurationContext>()
                .InstancePerRequest();

            builder.RegisterType<MenuManager>()
                .As<IMenuManager>()
                .InstancePerRequest();

            builder.RegisterType<PermissionChecker>()
                .As<IPermissionChecker>()
                .InstancePerRequest();

            base.Load(builder);
        }
    }
}