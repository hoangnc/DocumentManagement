using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.Core.Web.Common.Identity.Configurations
{
    public class OpenIdConnectionOption : ConfigurationElement
    {
        [ConfigurationProperty("clientId", IsRequired = true, IsKey = true, DefaultValue = "idserver")]
        public string ClientId
        {
            get => this["clientId"].ToString();
            set => this["clientId"] = value;
        }

        [ConfigurationProperty("scopes", DefaultValue = "openid;profile;roles;adminapi")]
        public string Scopes
        {
            get => Convert.ToString(this["scopes"]);
            set => this["scopes"] = value;
        }

        [ConfigurationProperty("authority", DefaultValue = "https://localhost:44319/identity")]
        public string Authority
        {
            get => Convert.ToString(this["authority"]);
            set => this["authority"] = value;
        }

        [ConfigurationProperty("responseType", DefaultValue = "id_token token")]
        public string ResponseType
        {
            get => Convert.ToString(this["responseType"]);
            set => this["responseType"] = value;
        }

        [ConfigurationProperty("redirectUri", DefaultValue = "http://localhost:61485/")]
        public string RedirectUri
        {
            get => Convert.ToString(this["redirectUri"]);
            set => this["redirectUri"] = value;
        }

        [ConfigurationProperty("signInAsAuthenticationType", DefaultValue = "Cookies")]
        public string SignInAsAuthenticationType
        {
            get => Convert.ToString(this["signInAsAuthenticationType"]);
            set => this["signInAsAuthenticationType"] = value;
        }

        [ConfigurationProperty("useTokenLifetime", DefaultValue = false)]
        public bool UseTokenLifetime
        {
            get => Convert.ToBoolean(this["useTokenLifetime"]);
            set => this["useTokenLifetime"] = value;
        }
    }
}
