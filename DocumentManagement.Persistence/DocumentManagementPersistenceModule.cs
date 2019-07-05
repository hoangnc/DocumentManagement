using Autofac;

namespace DocumentManagement.Persistence
{
    public class DocumentManagementPersistenceModule : Module
    {
        private string _documentConnectionString;

        public DocumentManagementPersistenceModule(string documentConnectionString)
        {
            _documentConnectionString = documentConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DocumentDbContext(_documentConnectionString))
                             .AsSelf();

            base.Load(builder);
        }
    }
}
