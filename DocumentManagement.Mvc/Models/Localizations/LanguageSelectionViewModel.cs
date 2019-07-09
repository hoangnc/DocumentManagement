using Abp.Localization;
using System.Collections.Generic;

namespace DocumentManagement.Mvc.Models.Localizations
{
    public class LanguageSelectionViewModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }

        public string CurrentUrl { get; set; }
    }
}