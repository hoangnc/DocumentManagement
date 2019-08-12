using AutoMapper;
using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Application.Modules.Queries;
using DocumentManagement.Application.PromulgateStatuses.Queries;
using DocumentManagement.Mvc.Models.Documents;
using DocumentManagement.Mvc.Models.DocumentTypes;
using DocumentManagement.Mvc.Models.Modules;
using DocumentManagement.Mvc.Models.PromulgateStatuses;

namespace DocumentManagement.Mvc.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GetModuleByIdDto, ModuleUpdateModel>();

                cfg.CreateMap<GetDocumentTypeByIdDto, DocumentTypeUpdateModel>();

                cfg.CreateMap<GetPromulgateStatusByIdDto, PromulgateStatusUpdateModel>();

                cfg.CreateMap<GetDocumentByIdDto, DocumentUpdateModel>();
            });

            Mapper = MapperConfiguration.CreateMapper();
        }
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}