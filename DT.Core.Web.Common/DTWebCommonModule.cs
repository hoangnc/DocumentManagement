using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Autofac;
using DT.Core.Localization;

namespace DT.Core.Web.Common
{
    public class DTWebCommonModule : Module
    {
        private readonly ILocalizationConfiguration _localizationConfiguration;
        public DTWebCommonModule(ILocalizationConfiguration localizationConfiguration)
        {
            _localizationConfiguration = localizationConfiguration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            _localizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource(
                    DTWebConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DTWebCommonModule).Assembly, "DT.Core.Web.Common.Localization.DTWebJsonSource")));

            base.Load(builder);
        }
    }
}
