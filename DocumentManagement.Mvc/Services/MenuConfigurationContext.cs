using Abp.Localization;
using DocumentManagement.Mvc.Constants;
using DT.Core.Web.Common.Models;
using DT.Core.Web.Ui.Navigation;
using System;

namespace DocumentManagement.Mvc.Services
{
    public static class MenuNameConstants
    {
        public const string DocumentManagement = "DocumentManagement";

        public const string ReleaseDocument = "ReleaseDocument";

        public const string ReleaseNewDocument = "ReleaseNewDocument";

        public const string ReviewDocument = "ReviewDocument";
    }

    public class MenuConfigurationContext : IMenuConfigurationContext
    {
        private readonly ILocalizationManager _localizationManager;
        public MenuConfigurationContext(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }
        public ModuleMenuItems Menu => BuildDocumentMenu();

        private ModuleMenuItems BuildDocumentMenu()
        {
            ModuleMenuItems moduleMenuItems = new ModuleMenuItems();

            ModuleMenuItem moduleMenuItem = new ModuleMenuItem();
            moduleMenuItem.Name = MenuNameConstants.DocumentManagement;
            moduleMenuItem.DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.ApplicationName);
            moduleMenuItem.Order = 0;

            #region Release Document
            MenuItem menuReleaseDocument = new MenuItem();
            menuReleaseDocument.Name = MenuNameConstants.ReleaseDocument;
            menuReleaseDocument.DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuReleaseDocument);
            menuReleaseDocument.Order = 0;
            menuReleaseDocument.Url = "#";
            menuReleaseDocument.Icon = "fa fa-cog";
            menuReleaseDocument.CssClass = "treeview";

            menuReleaseDocument.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.ReleaseNewDocument,
                DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuReleaseNewDocument),
                Url = "#",
                Order = 0
            });

            menuReleaseDocument.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.ReviewDocument,
                DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuReviewDocument),
                Url = "#",
                Order = 1
            });
            #endregion
            moduleMenuItem.Items.Add(menuReleaseDocument);
            moduleMenuItems.Add(moduleMenuItem);
            return moduleMenuItems;
        }
    }
}