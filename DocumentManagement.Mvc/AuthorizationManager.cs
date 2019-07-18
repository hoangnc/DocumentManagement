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
                default:
                    return Nok();
            }           
        }

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
                default:
                    return Nok();
            }
        }
    }
}