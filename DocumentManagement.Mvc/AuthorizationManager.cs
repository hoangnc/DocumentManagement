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

        public const string Appendices = "Appendices";
        public const string ApiAppendices = "ApiAppendices";

        public const string Modules = "Modules";
        public const string ApiModules = "ApiModules";

        public const string DocumentTypes = "DocumentTypes";
        public const string ApiDocumentTypes = "ApiDocumentTypes";

        public const string PromulgateStatuses = "PromulgateStatuses";
        public const string ApiPromulgateStatuses = "ApiPromulgateStatuses";
    }

    public class AuthorizationManager : ResourceAuthorizationManager
    {
        private const string AdminDocumentMvcClaimType = "admindocumentmvc_permission";
        private const string AdminDocumentApiClaimType = "admindocumentapi_permission";
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
                case DocumentResources.Appendices:
                    return AuthorizeAppendices(context);
                case DocumentResources.ApiAppendices:
                    return AuthorizeApiAppendices(context);
                case DocumentResources.Modules:
                    return AuthorizeModules(context);
                case DocumentResources.ApiModules:
                    return AuthorizeApiModules(context);
                case DocumentResources.DocumentTypes:
                    return AuthorizeDocumentTypes(context);
                case DocumentResources.ApiDocumentTypes:
                    return AuthorizeApiDocumentTypes(context);
                case DocumentResources.PromulgateStatuses:
                    return AuthorizePromulgateStatuses(context);
                case DocumentResources.ApiPromulgateStatuses:
                    return AuthorizeApiPromulgateStatuses(context);
                default:
                    return Nok();
            }           
        }

        #region Appendices
        public Task<bool> AuthorizeAppendices(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }

        public Task<bool> AuthorizeApiAppendices(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Update));
                case DtPermissionBaseTypes.Delete:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Delete));
                default:
                    return Nok();
            }
        }

        #endregion

        #region PromulgateStatuses
        public Task<bool> AuthorizePromulgateStatuses(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }

        public Task<bool> AuthorizeApiPromulgateStatuses(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }
        #endregion

        #region Documents
        public Task<bool> AuthorizeDocuments(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }

        public Task<bool> AuthorizeApiDocuments(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Update));
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
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }

        public Task<bool> AuthorizeApiDocumentTypes(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Update));
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
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentMvcClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }
        public Task<bool> AuthorizeApiModules(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminDocumentApiClaimType, DtPermissionBaseTypes.Update));
                default:
                    return Nok();
            }
        }
        #endregion
    }
}