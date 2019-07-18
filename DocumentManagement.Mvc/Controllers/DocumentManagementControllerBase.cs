using Abp.Localization;
using DocumentManagement.Mvc.Models.Localizations;
using System.Linq;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Controllers
{
    public class DocumentManagementControllerBase : Controller
    {
        private readonly ILanguageManager _languageManager = DependencyResolver.Current.GetService<ILanguageManager>();

        [ChildActionOnly]
        public PartialViewResult _LanguagesPartial()
        {
            LanguageSelectionViewModel model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList()
                    .Where(l => !l.IsDisabled)
                    .ToList(),
                CurrentUrl = Request.Path
            };

            return PartialView("_LanguagesPartial", model);
        }
    }
}