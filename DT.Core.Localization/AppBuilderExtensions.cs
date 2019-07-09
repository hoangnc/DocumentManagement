using Abp.Localization;
using Autofac;
using Owin;

namespace DT.Core.Localization
{
    public static class AppBuilderExtensions
    {
        public static ILocalizationManager UseLocalization(this IAppBuilder app, 
           ILocalizationManager localizationManager )
        {
            return localizationManager;
        }
    }
}
