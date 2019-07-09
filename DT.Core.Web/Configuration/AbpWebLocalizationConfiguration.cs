namespace Abp.Web.Configuration
{
    public class AbpWebLocalizationConfiguration : IAbpWebLocalizationConfiguration
    {
        public string CookieName { get; set; }

        public AbpWebLocalizationConfiguration()
        {
            CookieName = "DT.Localization.CultureName";
        }
    }
}