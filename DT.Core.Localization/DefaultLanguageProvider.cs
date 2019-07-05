using System.Collections.Generic;
using System.Collections.Immutable;
using DT.Core.Localization;

namespace Abp.Localization
{
    public class DefaultLanguageProvider : ILanguageProvider
    {
        private readonly ILocalizationConfiguration _configuration;

        public DefaultLanguageProvider(ILocalizationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }
    }
}