using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Application.Modules.Queries;
using DocumentManagement.Mvc.Models.DocumentTypes;
using DocumentManagement.Mvc.Models.Modules;

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
        #endregion
    }
}