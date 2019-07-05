using Autofac;
using MediatR;
using System;
using System.Reflection;

namespace DocumentManagement.Application
{
    public class DocumentManagementApplicationModule : Autofac.Module
    {
        public DocumentManagementApplicationModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            Type[] mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (Type mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(DocumentManagementApplicationModule).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            base.Load(builder);
        }
    }
}
