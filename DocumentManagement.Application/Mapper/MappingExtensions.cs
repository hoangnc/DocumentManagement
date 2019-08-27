using DocumentManagement.Application.Appendices.Commands;
using DocumentManagement.Application.Appendices.Queries;
using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Application.DocumentTypes.Commands;
using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Application.Groups.Queries;
using DocumentManagement.Application.Modules.Commands;
using DocumentManagement.Application.Modules.Queries;
using DocumentManagement.Application.PromulgateStatuses.Commands;
using DocumentManagement.Application.PromulgateStatuses.Queries;
using DocumentManagement.Application.Statuses.Queries;
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

        public static SearchDocumentsByDocumentTypeAndTokenPagedDto ToSearchDocumentsByDocumentTypeAndTokenPagedDto(this Document entity)
        {
            return entity.MapTo<Document, SearchDocumentsByDocumentTypeAndTokenPagedDto>();
        }

        public static GetDocumentByIdDto ToGetDocumentByIdDto(this Document entity)
        {
            return entity.MapTo<Document, GetDocumentByIdDto>();
        }

        public static GetDocumentByCodeDto ToGetDocumentByCodeDto(this Document entity)
        {
            return entity.MapTo<Document, GetDocumentByCodeDto>();
        }

        public static GetAllDocumentDto ToGetAllDocumentDto(this Document entity)
        {
            return entity.MapTo<Document, GetAllDocumentDto>();
        }

        public static GetDocumentsByMonthDto ToGetDocumentsByMonthDto(this Document entity)
        {
            return entity.MapTo<Document, GetDocumentsByMonthDto>();
        }

        public static Document ToDocument(this CreateDocumentCommand command)
        {
            return command.MapTo<CreateDocumentCommand, Document>();
        }

        public static Document ToDocument(this ReviewDocumentCommand command)
        {
            return command.MapTo<ReviewDocumentCommand, Document>();
        }
        #endregion

        #region Appendices
        public static SearchAppendicesByTokenPagedDto ToSearchAppendicesByTokenPagedDto(this Appendice entity)
        {
            return entity.MapTo<Appendice, SearchAppendicesByTokenPagedDto>();
        }

        public static Appendice ToAppendice(this CreateAppendiceCommand command)
        {
            return command.MapTo<CreateAppendiceCommand, Appendice>();
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

        public static PromulgateStatus ToPromulgateStatus(this CreatePromulgateStatusCommand command)
        {
            return command.MapTo<CreatePromulgateStatusCommand, PromulgateStatus>();
        }

        public static GetPromulgateStatusByIdDto ToGetPromulgateStatusByIdDto(this PromulgateStatus promulgateStatus)
        {
            return promulgateStatus.MapTo<PromulgateStatus, GetPromulgateStatusByIdDto>();
        }
        #endregion

        #region PromulgateStatuses
        public static GetAllPromulgateStatusesDto ToGetAllPromulgateStatusesDto(this PromulgateStatus entity)
        {
            return entity.MapTo<PromulgateStatus, GetAllPromulgateStatusesDto>();
        }
        #endregion

        #region Statuses
        public static GetAllStatusesDto ToGetAllStatusesDto(this Status entity)
        {
            return entity.MapTo<Status, GetAllStatusesDto>();
        }
        #endregion

        #region Groups
        public static GetAllGroupsDto ToGetAllGroupsDto(this Group entity)
        {
            return entity.MapTo<Group, GetAllGroupsDto>();
        }
        #endregion
    }
}
