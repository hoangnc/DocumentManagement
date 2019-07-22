using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Application.DocumentTypes.Commands;
using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Application.Modules.Commands;
using DocumentManagement.Application.Modules.Queries;
using DocumentManagement.Application.PromulgateStatuses.Queries;
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

        public static SearchDocumentTypesByTokenPagedDto ToSearchDocumentTypesByTokenPagedDto(this DocumentType documentType)
        {
            return documentType.MapTo<DocumentType, SearchDocumentTypesByTokenPagedDto>();
        }

        public static DocumentType ToDocumentType(this CreateDocumentTypeCommand command)
        {
            return command.MapTo<CreateDocumentTypeCommand, DocumentType>();
        }

        public static DocumentType ToDocumentType(this UpdateDocumentTypeCommand command)
        {
            return command.MapTo<UpdateDocumentTypeCommand, DocumentType>();
        }

        public static GetDocumentTypeByIdDto ToGetDocumentTypeByIdDto(this DocumentType documentType)
        {
            return documentType.MapTo<DocumentType, GetDocumentTypeByIdDto>();
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

        public static GetModuleByIdDto ToGetModuleByIdDto(this Module entity)
        {
            return entity.MapTo<Module, GetModuleByIdDto>();
        }

        public static GetAllModulesDto ToGetAllModulesDto(this Module entity)
        {
            return entity.MapTo<Module, GetAllModulesDto>();
        }
        #endregion

        #region PromulgateStatuses
        public static SearchPromulgateStatusByTokenPagedDto ToSearchPromulgateStatusByTokenPagedDto(this PromulgateStatus promulgateStatus)
        {
            return promulgateStatus.MapTo<PromulgateStatus, SearchPromulgateStatusByTokenPagedDto>();
        }
        #endregion
    }
}
