using AutoMapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Application.Modules.Queries;
using DocumentManagement.Application.Modules.Commands;

namespace DocumentManagement.Application.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Document, SearchDocumentsByTokenPagedDto>();

                cfg.CreateMap<CreateDocumentCommand, Document>();

                cfg.CreateMap<DocumentType, GetAllDocumentTypesDto>();

                cfg.CreateMap<Module, SearchModulesByTokenPagedDto>();

                cfg.CreateMap<CreateModuleCommand, Module>();
            });

            Mapper = MapperConfiguration.CreateMapper();
        }
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}
