using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Domain.Entities;

namespace DocumentManagement.Application.Mapper
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        #region Documents
        public static SearchDocumentsByTokenPagedDto ToSearchDocumentsByTokenPagedDto(this Document entity)
        {
            return entity.MapTo<Document, SearchDocumentsByTokenPagedDto>();
        }

        #endregion
    }
}
