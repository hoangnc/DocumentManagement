using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Autofac;
using DT.Core.Localization;
using DT.Core.Web.Common;
using DT.Core.Web.Common.Identity.Configurations;
using DT.Core.Web.Common.Validation;
using FluentValidation;
using IdentityModel.Client;
using IdentityServer3.Core;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(DocumentManagement.Mvc.Startup))]
namespace DocumentManagement.Mvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();
            // Adjust the configuration for anti-CSRF protection to the new unique sub claim type
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            IContainer container = AutofacConfig.ConfigureContainer();

            app.UseAutofacMiddleware(container);

            LocalizationManager localizationManager = container.Resolve<ILocalizationManager>() as LocalizationManager;
            localizationManager.Initialize();

            var customLanguageManager = new CustomLanguageManager(container.Resolve<ILanguageManager>());
            ValidatorOptions.LanguageManager = customLanguageManager;

            app.UseResourceAuthorization(new AuthorizationManager());
            app.UseKentorOwinCookieSaver();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            object section = ConfigurationManager.GetSection("openIdConnectionOptionSection");

            if (section != null)
            {
                OpenIdConnectionOption openIdConnectionOption = (section as OpenIdConnectionOptionSection).OpenIdConnectionOption;
                app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                {
                    Authority = openIdConnectionOption.Authority,
                    ClientId = openIdConnectionOption.ClientId,
                    Scope = openIdConnectionOption.Scopes,
                    ResponseType = openIdConnectionOption.ResponseType,
                    RedirectUri = openIdConnectionOption.RedirectUri,
                    SignInAsAuthenticationType = openIdConnectionOption.SignInAsAuthenticationType,
                    UseTokenLifetime = openIdConnectionOption.UseTokenLifetime,

                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        SecurityTokenValidated = async n =>
                        {
                            ClaimsIdentity nid = new ClaimsIdentity(
                                n.AuthenticationTicket.Identity.AuthenticationType,
                                IdentityServer3.Core.Constants.ClaimTypes.GivenName,
                                IdentityServer3.Core.Constants.ClaimTypes.Role);

                            // get userinfo data
                            UserInfoClient userInfoClient = new UserInfoClient(
                                new Uri(n.Options.Authority + "/connect/userinfo"),
                                n.ProtocolMessage.AccessToken);

                            UserInfoResponse userInfo = await userInfoClient.GetAsync();
                            userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

                            // keep the id_token for logout
                            nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                            // add access token for sample API
                            nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                            // keep track of access token expiration
                            nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

                            n.AuthenticationTicket = new AuthenticationTicket(
                                nid,
                                n.AuthenticationTicket.Properties);
                        },

                        RedirectToIdentityProvider = n =>
                        {
                            if (n.ProtocolMessage.RequestType == Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectRequestType.Logout)
                            {
                                Claim idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                                if (idTokenHint != null)
                                {
                                    n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                                }
                            }

                            return Task.FromResult(0);
                        }
                    }
                });

            }
        }
    }
}