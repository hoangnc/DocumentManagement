using DT.Core.Web.Common.Api.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IdentityModel.Client;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Configuration;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class MasterDatasApiController : BaseApiController
    {
        private string MasterDataEndpoint => ConfigurationManager.AppSettings["MasterDataEndpoint"].ToString();

        [Route("api/masterdatas/getalldepartments")]
        public async Task<dynamic> GetAllDepartments()
        {
            var user = User as ClaimsPrincipal;
            var token = user.FindFirst("access_token").Value;
            return await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getalldepartments"), token);
        }

        [Route("api/masterdatas/getallusers")]
        [HttpGet]
        public async Task<dynamic> GetAllUsers()
        {
            var user = User as ClaimsPrincipal;
            var token = user.FindFirst("access_token").Value;
            return await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallusers"), token);
        }

        private async Task<TokenResponse> GetTokenAsync()
        {
            var client = new TokenClient(
                "https://localhost:44319/identity/connect/token",
                "masterdata",
                "967ef861-1b5f-4ea1-9fd9-b66c24a8335c");

            return await client.RequestClientCredentialsAsync("documentapi");
        }

        private async Task<dynamic> CallApi(Uri uri, string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);

            var json = Newtonsoft.Json.JsonConvert.DeserializeObject(await client.GetStringAsync(uri));
            return json;
        }
    }
}
