using AutoMapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Application.Modules.Queries;
using DocumentManagement.Application.Modules.Commands;
using DocumentManagement.Application.DocumentTypes.Commands;
using DocumentManagement.Application.PromulgateStatuses.Queries;
using DocumentManagement.Application.PromulgateStatuses.Commands;
using DocumentManagement.Application.Appendices.Queries;
using DocumentManagement.Application.Appendices.Commands;

namespace DocumentManagement.Application.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Document, SearchDocumentsByTokenPagedDto>();

                cfg.CreateMap<Document, GetDocumentByIdDto>();

                cfg.CreateMap<Document, GetDocumentByCodeDto>();

                cfg.CreateMap<Document, GetAllDocumentDto>();

                cfg.CreateMap<CreateDocumentCommand, Document>();

                cfg.CreateMap<ReviewDocumentCommand, Document>();

                cfg.CreateMap<DocumentType, GetAllDocumentTypesDto>();

                cfg.CreateMap<Module, SearchModulesByTokenPagedDto>();

                cfg.CreateMap<CreateModuleCommand, Module>();

                cfg.CreateMap<Module, GetModuleByIdDto>();

                cfg.CreateMap<Module, GetAllModulesDto>();

                cfg.CreateMap<DocumentType, SearchDocumentTypesByTokenPagedDto>();

                cfg.CreateMap<CreateDocumentTypeCommand, DocumentType>();

                cfg.CreateMap<UpdateDocumentTypeCommand, DocumentType>();

                cfg.CreateMap<DocumentType, GetDocumentTypeByIdDto>();

                cfg.CreateMap<PromulgateStatus, SearchPromulgateStatusByTokenPagedDto>();

                cfg.CreateMap<CreatePromulgateStatusCommand, PromulgateStatus>();

                cfg.CreateMap<PromulgateStatus, GetPromulgateStatusByIdDto>();

                cfg.CreateMap<Appendice, SearchAppendicesByTokenPagedDto>();

                cfg.CreateMap<CreateAppendiceCommand, Appendice>();
            });

            Mapper = MapperConfiguration.CreateMapper();
        }
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}
