using System.Configuration;

namespace DT.Core.Web.Common.Identity.Configurations
{
    public class OpenIdConnectionOptionSection : ConfigurationSection
    {
        [ConfigurationProperty("openIdConnectionOption")]
        public OpenIdConnectionOption OpenIdConnectionOption => (OpenIdConnectionOption)this["openIdConnectionOption"];
    }
}
