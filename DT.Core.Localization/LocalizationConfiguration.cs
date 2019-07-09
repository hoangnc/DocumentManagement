using Abp.Localization;
using System.Collections.Generic;

namespace DT.Core.Localization
{
    public class LocalizationConfiguration : ILocalizationConfiguration
    {
        /// <inheritdoc/>
        public IList<LanguageInfo> Languages { get; }

        /// <inheritdoc/>
        public ILocalizationSourceList Sources { get; }

        /// <inheritdoc/>
        public bool IsEnabled { get; set; }

        /// <inheritdoc/>
        public bool ReturnGivenTextIfNotFound { get; set; }

        /// <inheritdoc/>
        public bool WrapGivenTextIfNotFound { get; set; }

        /// <inheritdoc/>
        public bool HumanizeTextIfNotFound { get; set; }

        public bool LogWarnMessageIfNotFound { get; set; }

        public LocalizationConfiguration()
        {
            Languages = new List<LanguageInfo>();
            Sources = new LocalizationSourceList();

            IsEnabled = true;
            ReturnGivenTextIfNotFound = true;
            WrapGivenTextIfNotFound = true;
            HumanizeTextIfNotFound = true;
            LogWarnMessageIfNotFound = true;
        }
    }
}
