using AutoMapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Application.Documents.Queries;

namespace DocumentManagement.Application.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Document, SearchDocumentsByTokenPagedDto>();
            });
        }
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}
