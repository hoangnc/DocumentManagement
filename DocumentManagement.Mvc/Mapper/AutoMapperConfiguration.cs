using AutoMapper;
using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Application.Modules.Queries;
using DocumentManagement.Mvc.Models.DocumentTypes;
using DocumentManagement.Mvc.Models.Modules;

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
            });

            Mapper = MapperConfiguration.CreateMapper();
        }
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}