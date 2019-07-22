using System.Linq;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc
{
    public static class DocumentResources
    {
        public const string Documents = "Documents";
        public const string ApiDocuments = "ApiDocuments";

        public const string Modules = "Modules";
        public const string ApiModules = "ApiModules";

        public const string DocumentTypes = "DocumentTypes";
        public const string ApiDocumentTypes = "ApiDocumentTypes";
    }

    public class AuthorizationManager : ResourceAuthorizationManager
    {
        private const string DocumentMvcClaimType = "documentmvc_permission";
        private const string DocumentApiClaimType = "documentapi_permission";

        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            switch (context.Resource.First().Value)
            {
                case DocumentResources.Documents:
                    return AuthorizeDocuments(context);
                case DocumentResources.ApiDocuments:
                    return AuthorizeApiDocuments(context);
                case DocumentResources.Modules:
                    return AuthorizeModules(context);
                case DocumentResources.ApiModules:
                    return AuthorizeApiModules(context);
                case DocumentResources.DocumentTypes:
                    return AuthorizeDocumentTypes(context);
                case DocumentResources.ApiDocumentTypes:
                    return AuthorizeApiDocumentTypes(context);
                default:
                    return Nok();
            }           
        }

        #region Documents
        public Task<bool> AuthorizeDocuments(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(DocumentMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(DocumentMvcClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(DocumentMvcClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }

        public Task<bool> AuthorizeApiDocuments(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(DocumentApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(DocumentApiClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(DocumentApiClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }
        #endregion

        #region DocumentTypes
        public Task<bool> AuthorizeDocumentTypes(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(DocumentMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(DocumentMvcClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(DocumentMvcClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }

        public Task<bool> AuthorizeApiDocumentTypes(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(DocumentApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(DocumentApiClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(DocumentApiClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }
        #endregion

        #region Modules
        public Task<bool> AuthorizeModules(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(DocumentMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(DocumentMvcClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(DocumentMvcClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }
        public Task<bool> AuthorizeApiModules(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(DocumentApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(DocumentApiClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(DocumentApiClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }
        #endregion
    }
}