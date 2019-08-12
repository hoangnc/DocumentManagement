using DocumentManagement.Mvc.Constants;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using LazyCache;
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

        private readonly IAppCache _appCache;
        public MasterDatasApiController(IAppCache appCache)
        {
            _appCache = appCache;
        }
        [Route("api/masterdatas/getalldepartments")]
        public async Task<dynamic> GetAllDepartments()
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            Func<Task<dynamic>> departments = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getalldepartments"), token);
            dynamic result = await _appCache.GetOrAddAsync(CacheKeys.DepartmentCacheKey, departments);
            return result;
        }

        [Route("api/masterdatas/getallusers")]
        [HttpGet]
        public async Task<dynamic> GetAllUsers()
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            Func<Task<dynamic>> users = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallusers"), token);
            dynamic result = await _appCache.GetOrAddAsync(CacheKeys.UserCacheKey, users);
            return result;
        }

        [Route("api/masterdatas/getallcompanies")]
        [HttpGet]
        public async Task<dynamic> GetAllCompanies()
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            Func<Task<dynamic>> companies = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallcompanies"), token);
            dynamic result = await _appCache.GetOrAddAsync(CacheKeys.CompanyKey, companies);
            return result;
        }


        [Route("api/masterdatas/getallgroupsfromactivedirectory")]
        [HttpGet]
        public async Task<dynamic> GetAllGroupsFromActiveDirectory()
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            Func<Task<dynamic>> groups = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallgroupsfromactivedirectory"), token);
            dynamic result = await _appCache.GetOrAddAsync(CacheKeys.GroupKey, groups);
            return result;
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
