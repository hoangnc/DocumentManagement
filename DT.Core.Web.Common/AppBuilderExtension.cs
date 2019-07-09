using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using DT.Core.Localization;
using Owin;

namespace DT.Core.Web.Common
{
    public static class AppBuilderExtension
    {
        public static ILocalizationConfiguration UseDTWebCommonLocalization(this IAppBuilder app, ILocalizationConfiguration localizationConfiguration)
        {
            var localizationSource = new DictionaryBasedLocalizationSource(
                    DTWebConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DTWebCommonModule).Assembly, "DT.Web.Common.Localization.DTWebJsonSource"));

            localizationConfiguration.Sources.Add(localizationSource);

            return localizationConfiguration;
        }
    }
}
