using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Application.Groups.Queries;
using DocumentManagement.Mvc.Constants;
using DT.Core.Helper;
using DT.Core.Text;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using DT.Core.Web.Common.Identity.Extensions;
using LazyCache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    public class DocumentManagementApiControllerBase : BaseApiController
    {
        private readonly string MailAdmin = ConfigurationManager.AppSettings["MailAdmin"].ToString();
        private readonly string MailSender = ConfigurationManager.AppSettings["MailSender"].ToString();
        private readonly string MailNotify = ConfigurationManager.AppSettings["MailNotify"].ToString();
        private string MasterDataEndpoint => ConfigurationManager.AppSettings["MasterDataEndpoint"].ToString();

        private IAppCache AppCache => Request.GetDependencyScope().GetService(typeof(IAppCache)) as IAppCache;
        
        protected string GetMailTemplate(string mailTemplateFileName)
        {
            string mailTemplteFilePath = HttpContext.Current.Server.MapPath("~/" + $"Templates/Email/{mailTemplateFileName}");
            string mailTemplateContent = File.ReadAllText(mailTemplteFilePath);
            return mailTemplateContent;
        }

        private string GetUserFullName(string userName, IEnumerable<dynamic> users)
        {
            dynamic user = users.FirstOrDefault(u => u.userName == userName);
            string userFullName = string.Empty;
            if (user != null)
            {
                userFullName = $"{user.lastName} {user.firstName}";
            }
            return userFullName;
        }

        protected async Task<int> SendMailDocumentsỊnMonth(List<GetDocumentsByMonthDto> documents, int month, int year)
        {
            string host = ConfigurationManager.AppSettings["Host"].ToString();
            string mailTemplate = GetMailTemplate("DocumentInMonth.html");
            string mailRowTemplate = GetMailTemplate("DocumentInMonthRow.html");

            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            Func<Task<IEnumerable<dynamic>>> users = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallusers"), token);
            IEnumerable<dynamic> result = await AppCache.GetOrAddAsync(CacheKeys.UserCacheKey, users);

            StringBuilder rowBuilder = new StringBuilder();
 
            if (!mailTemplate.IsNullOrEmpty())
            {
                int index = 1;
                foreach (var document in documents)
                {
                    string effectiveDateString = string.Empty;
                    if (document.EffectiveDate.HasValue)
                    {
                        effectiveDateString = $"{document.EffectiveDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                    }
                    string approverFullName = GetUserFullName(document.Approver, result);
                    string linkFile = $"<a href=\"{host}/operationdata/detail?code={document.Code}\">{host}/operationdata/detail?code={document.Code}</a>";//$"<a href=\"{host}/Uploads/{document.DocumentType}/{document.FileName}\">{document.FileName}</a>";
                    rowBuilder.AppendLine(string.Format(mailRowTemplate,
                        index,
                        document.Name,
                        document.DocumentNumber,
                        document.ReviewNumber,
                        document.DocumentType,
                        effectiveDateString,
                        document.ReplaceOf,
                        document.ScopeOfDeloyment,
                        approverFullName,
                        linkFile
                        ));
                    index++;
                }
            }

            string createdEmail = User.Identity.GetUserEmail();

            MailHelper mailHelper = new MailHelper
            {
                Sender = MailSender,
                Recipient = string.Join(",", new string[] { "nguyenconghoang@duytan.com" }),
                RecipientCC = string.Join(",", new string[] { "nguyenconghoang@duytan.com", createdEmail }),// "daocatkhanh@duytan.com"
                Subject = $"DCC - Ban hành tài liệu mới",
                Body = string.Format(mailTemplate, $"{month}/{year}", rowBuilder.ToString())
            };
            mailHelper.Send();

            return 1;
        }

        protected async Task<int> SendMailReleaseDocument(CreateDocumentCommand command)
        {
            string host = ConfigurationManager.AppSettings["Host"].ToString();
            string mailTemplate = GetMailTemplate("ReleaseDocument.html");

            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            Func<Task<IEnumerable<dynamic>>> users = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallusers"), token);
            IEnumerable<dynamic> result = await AppCache.GetOrAddAsync(CacheKeys.UserCacheKey, users);

            Func<Task<IEnumerable<dynamic>>> groupsFunc = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getalldepartments"), token);
            IEnumerable<dynamic> groups = await AppCache.GetOrAddAsync(CacheKeys.DepartmentCacheKey, groupsFunc);

            var groupEmails = new List<string>();
            var deployments = command.ScopeOfDeloyment.Split(";");

            if (deployments != null && deployments.Any())
            {
                foreach (var deployment in deployments)
                {
                    var group = groups.FirstOrDefault(g => g.code == deployment);
                    if (group != null)
                    {
                        string email = group.email;
                        groupEmails.Add(email.Replace(";", ","));
                    }
                }
            }

            string draterFullName = GetUserFullName(command.Drafter, result);

            string auditorFullName = GetUserFullName(command.Auditor, result);

            string approverFullName = GetUserFullName(command.Approver, result);

            string createdEmail = User.Identity.GetUserEmail();

            if (!createdEmail.IsNullOrEmpty())
                groupEmails.Add(createdEmail);

            if (!mailTemplate.IsNullOrEmpty())
            {
                string effectiveDateString = string.Empty;
                if (command.EffectiveDate.HasValue)
                {
                    effectiveDateString = $"{command.EffectiveDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string reviewDateString = string.Empty;
                if (command.ReviewDate.HasValue)
                {
                    reviewDateString = $"{command.ReviewDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string linkFile = $"<a href=\"{host}/downloadfile/viewfile?sourceDoc={command.FolderName}/{command.FileName}\">{command.FileName}</a>";
                MailHelper mailHelper = new MailHelper
                {
                    Sender = MailSender,
                    Recipient = string.Join(",", groupEmails.ToArray()),
                    RecipientCC = string.Join(",", new string[] { }),
                    Subject = $"DCC - Ban hành tài liệu mới",
                    Body = string.Format(mailTemplate,
                        $"DCC - Ban hành tài liệu mới;#{command.DepartmentName} : {command.FileName}",
                        "Ban hành tài liệu mới",
                        command.Name,
                        command.ScopeOfApplication.Replace("\n", "<br>"),
                        effectiveDateString,
                        command.Description.Replace("\n", "<br>"),
                        command.ScopeOfDeloyment,
                        command.DocumentNumber,
                        command.ReviewNumber,
                        reviewDateString,
                        draterFullName,
                        auditorFullName,
                        approverFullName,
                        linkFile,
                        $"<a href=\"{host}/operationdata/detail?code={command.Code}\">{host}/operationdata/detail?code={command.Code}</a>"
                     )
                };
                mailHelper.Send();
            }

            return 1;
        }

        protected async Task<int> SendMailReleaseDocument(GetDocumentByIdDto command)
        {
            string host = ConfigurationManager.AppSettings["Host"].ToString();
            string mailTemplate = GetMailTemplate("ReleaseDocument.html");

            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            Func<Task<IEnumerable<dynamic>>> users = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallusers"), token);
            IEnumerable<dynamic> result = await AppCache.GetOrAddAsync(CacheKeys.UserCacheKey, users);

            Func<Task<IEnumerable<dynamic>>> groupsFunc = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getalldepartments"), token);
            IEnumerable<dynamic> groups = await AppCache.GetOrAddAsync(CacheKeys.DepartmentCacheKey, groupsFunc);

            var groupEmails = new List<string>();
            var deployments = command.ScopeOfDeloyment.Split(";");

            if (deployments != null && deployments.Any())
            {
                foreach (var deployment in deployments)
                {
                    var group = groups.FirstOrDefault(g => g.code == deployment);
                    if (group != null)
                    {
                        string email = group.email;
                        groupEmails.Add(email.Replace(";", ","));
                    }
                }
            }

            string draterFullName = GetUserFullName(command.Drafter, result);

            string auditorFullName = GetUserFullName(command.Auditor, result);

            string approverFullName = GetUserFullName(command.Approver, result);

            string createdEmail = User.Identity.GetUserEmail();

            if (!createdEmail.IsNullOrEmpty())
                groupEmails.Add(createdEmail);

            if (!mailTemplate.IsNullOrEmpty())
            {
                string effectiveDateString = string.Empty;
                if (command.EffectiveDate.HasValue)
                {
                    effectiveDateString = $"{command.EffectiveDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string reviewDateString = string.Empty;
                if (command.ReviewDate.HasValue)
                {
                    reviewDateString = $"{command.ReviewDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string linkFile = $"<a href=\"{host}/downloadfile/viewfile?sourceDoc={command.FolderName}/{command.FileName}\">{command.FileName}</a>";
                MailHelper mailHelper = new MailHelper
                {
                    Sender = MailSender,
                    Recipient = string.Join(",", groupEmails.ToArray()),
                    RecipientCC = string.Join(",", new string[] {}),
                    Subject = $"DCC - Ban hành tài liệu mới",
                    Body = string.Format(mailTemplate,
                        $"DCC - Ban hành tài liệu mới;#{command.DepartmentName} : {command.FileName}",
                        "Ban hành tài liệu mới",
                        command.Name,
                        command.ScopeOfApplication.Replace("\n", "<br>"),
                        effectiveDateString,
                        command.Description.Replace("\n", "<br>"),
                        command.ScopeOfDeloyment,
                        command.DocumentNumber,
                        command.ReviewNumber,
                        reviewDateString,
                        draterFullName,
                        auditorFullName,
                        approverFullName,
                        linkFile,
                        $"<a href=\"{host}/operationdata/detail?code={command.Code}\">{host}/operationdata/detail?code={command.Code}</a>"
                     )
                };
                mailHelper.Send();
            }

            return 1;
        }

        protected async Task<int> SendMailReviewDocument(ReviewDocumentCommand command)
        {
            string host = ConfigurationManager.AppSettings["Host"].ToString();
            string mailTemplate = GetMailTemplate("ReleaseDocument.html");

            ClaimsPrincipal user = User as ClaimsPrincipal;
            string token = user.FindFirst("access_token").Value;
            Func<Task<IEnumerable<dynamic>>> users = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallusers"), token);
            IEnumerable<dynamic> result = await AppCache.GetOrAddAsync(CacheKeys.UserCacheKey, users);

            Func<Task<IEnumerable<dynamic>>> groupsFunc = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getalldepartments"), token);
            IEnumerable<dynamic> groups = await AppCache.GetOrAddAsync(CacheKeys.DepartmentCacheKey, groupsFunc);

            var groupEmails = new List<string>();
            var deployments = command.ScopeOfDeloyment.Split(";");

            if (deployments != null && deployments.Any())
            {
                foreach (var deployment in deployments)
                {
                    var group = groups.FirstOrDefault(g => g.code == deployment);
                    if (group != null)
                    {
                        string email = group.email;
                        groupEmails.Add(email.Replace(";", ","));
                    }
                }
            }

            string draterFullName = GetUserFullName(command.Drafter, result);

            string auditorFullName = GetUserFullName(command.Auditor, result);

            string approverFullName = GetUserFullName(command.Approver, result);

            string createdEmail = User.Identity.GetUserEmail();

            if (!createdEmail.IsNullOrEmpty())
                groupEmails.Add(createdEmail);

            if (!mailTemplate.IsNullOrEmpty())
            {
                string effectiveDateString = string.Empty;
                if (command.EffectiveDate.HasValue)
                {
                    effectiveDateString = $"{command.EffectiveDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string reviewDateString = string.Empty;
                if (command.ReviewDate.HasValue)
                {
                    reviewDateString = $"{command.ReviewDate.Value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)}";
                }

                string linkFile = $"<a href=\"{host}/downloadfile/viewfile?sourceDoc={command.FolderName}/{command.FileName}\">{command.FileName}</a>";
                MailHelper mailHelper = new MailHelper
                {
                    Sender = MailSender,
                    Recipient = string.Join(",", groupEmails.ToArray()),
                    RecipientCC = string.Join(",", new string[] { }),
                    Subject = $"DCC - Thay đổi tài liệu",
                    Body = string.Format(mailTemplate,
                        $"DCC - Thay đổi tài liệu;#{command.DepartmentName} : {command.FileName}",
                        "Thay đổi tài liệu",
                        command.Name,
                        command.ScopeOfApplication.Replace("\n", "<br>"),
                        effectiveDateString,
                        command.Description.Replace("\n", "<br>"),
                        command.ScopeOfDeloyment,
                        command.DocumentNumber,
                        command.ReviewNumber,
                        reviewDateString,
                        draterFullName,
                        auditorFullName,
                        approverFullName,
                        linkFile,
                        $"<a href=\"{host}/operationdata/detail?code={command.Code}\">{host}/operationdata/detail?code={command.Code}</a>"
                     )
                };
                mailHelper.Send();
            }

            return 1;
        }

        private async Task<dynamic> CallApi(Uri uri, string token)
        {
            HttpClient client = new HttpClient();
            client.SetBearerToken(token);
            object json = JsonConvert.DeserializeObject(await client.GetStringAsync(uri));
            return json;
        }

        protected string GetFolderPath(string documentCode)
        {
            string uploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
            string yearFolder = $"{uploadFolderPath}/{DateTime.Now.Year.ToString()}";
            string monthFolder = $"{yearFolder}/{DateTime.Now.Month.ToString()}";
            string dateFolder = $"{monthFolder}/{DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture)}";
            string documentFolder = $"{dateFolder}/{documentCode}";

            if (!Directory.Exists(yearFolder))
                Directory.CreateDirectory(yearFolder);

            if (!Directory.Exists(monthFolder))
                Directory.CreateDirectory(monthFolder);

            if (!Directory.Exists(dateFolder))
                Directory.CreateDirectory(dateFolder);

            if (!Directory.Exists(documentFolder))
                Directory.CreateDirectory(documentFolder);

            return documentFolder;
        }
    }
}