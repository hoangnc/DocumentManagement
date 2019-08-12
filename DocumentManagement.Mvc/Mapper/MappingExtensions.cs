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

        #region 
        public static DocumentUpdateModel ToDocumentUpdateModel(this GetDocumentByIdDto dto)
        {
            return dto.MapTo<GetDocumentByIdDto, DocumentUpdateModel>();
        }
        #endregion

        #region Modules
        public static ModuleUpdateModel ToModuleUpdateModel(this GetModuleByIdDto dto)
        {
            return dto.MapTo<GetModuleByIdDto, ModuleUpdateModel>();
        }
        #endregion

        #region DocumentTypes
        public static DocumentTypeUpdateModel ToDocumentTypeUpdateModel(this GetDocumentTypeByIdDto dto)
        {
            return dto.MapTo<GetDocumentTypeByIdDto, DocumentTypeUpdateModel>();
        }

        public static PromulgateStatusUpdateModel ToPromulgateStatusUpdateModel(this GetPromulgateStatusByIdDto dto)
        {
            return dto.MapTo<GetPromulgateStatusByIdDto, PromulgateStatusUpdateModel>();
        }
        #endregion
    }
}