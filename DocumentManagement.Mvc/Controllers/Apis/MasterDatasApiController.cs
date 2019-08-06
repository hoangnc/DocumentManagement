using DT.Core.Web.Common.Api.WebApi.Controllers;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class MasterDatasApiController : BaseApiController
    {
        private string MasterDataEndpoint => ConfigurationManager.AppSettings["MasterDataEndpoint"].ToString();

        [Route("api/masterdatas/getalldepartments")]
        public async Task<dynamic> GetAllDepartments()
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            return await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getalldepartments"), token);
        }

        [Route("api/masterdatas/getallusers")]
        [HttpGet]
        public async Task<dynamic> GetAllUsers()
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            return await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallusers"), token);
        }

        [Route("api/masterdatas/getallcompanies")]
        [HttpGet]
        public async Task<dynamic> GetAllCompanies()
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            return await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallcompanies"), token);
        }


        [Route("api/masterdatas/getallgroupsfromactivedirectory")]
        [HttpGet]
        public async Task<dynamic> GetAllGroupsFromActiveDirectory()
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            return await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallgroupsfromactivedirectory"), token);
        }

        private async Task<TokenResponse> GetTokenAsync()
        {
            TokenClient client = new TokenClient(
                "https://localhost:44319/identity/connect/token",
                "masterdata",
                "967ef861-1b5f-4ea1-9fd9-b66c24a8335c");

            return await client.RequestClientCredentialsAsync("documentapi");
        }

        private async Task<dynamic> CallApi(Uri uri, string token)
        {
            HttpClient client = new HttpClient();
            client.SetBearerToken(token);
            object json = JsonConvert.DeserializeObject(await client.GetStringAsync(uri));
            return json;
        }
    }
}
