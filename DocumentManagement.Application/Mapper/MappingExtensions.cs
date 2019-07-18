using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Application.Modules.Commands;
using DocumentManagement.Application.Modules.Queries;
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

        public static Document ToDocument(this CreateDocumentCommand command)
        {
            return command.MapTo<CreateDocumentCommand, Document>();
        }
        #endregion

        #region DocumentTypes
        public static GetAllDocumentTypesDto ToGetAllDocumentTypesDto(this DocumentType documentType)
        {
            return documentType.MapTo<DocumentType, GetAllDocumentTypesDto>();
        }
        #endregion

        #region Module
        public static SearchModulesByTokenPagedDto ToSearchModulesByTokenPagedDto(this Module entity)
        {
            return entity.MapTo<Module, SearchModulesByTokenPagedDto>();
        }
        public static Module ToModule(this CreateModuleCommand command) {
            return command.MapTo<CreateModuleCommand, Module>();
        }
        #endregion
    }
}
