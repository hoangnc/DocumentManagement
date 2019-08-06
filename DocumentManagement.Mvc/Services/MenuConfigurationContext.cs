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

        public const string ReleaseAppendice = "ReleaseAppendice";
        public const string ReleaseNewAppendice = "ReleaseNewAppendice";
        public const string ReviewAppendice = "ReviewAppendice";

        public const string Category = "Category";
        public const string Module = "Module";
        public const string DocumentType = "DocumentType";
        public const string PromulgateStatus = "PromulgateStatus";
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
                Url = "/document/index",
                Order = 0
            });

            menuReleaseDocument.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.ReviewDocument,
                DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuReviewDocument),
                Url = "/document/reviewdocument",
                Order = 1
            });
            #endregion
            moduleMenuItem.Items.Add(menuReleaseDocument);
            #region Appendice
            MenuItem menuAppendiceDocument = new MenuItem();
            menuAppendiceDocument.Name = MenuNameConstants.ReleaseAppendice;
            menuAppendiceDocument.DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuReleaseAppendice);
            menuAppendiceDocument.Order = 0;
            menuAppendiceDocument.Url = "#";
            menuAppendiceDocument.Icon = "fa fa-cog";
            menuAppendiceDocument.CssClass = "treeview";

            menuAppendiceDocument.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.ReleaseNewAppendice,
                DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuReleaseNewAppendice),
                Url = "/appendice/index",
                Order = 0
            });

            menuAppendiceDocument.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.ReviewAppendice,
                DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuReviewAppendice),
                Url = "/document/reviewdocument",
                Order = 1
            });
            #endregion
            moduleMenuItem.Items.Add(menuAppendiceDocument);

            #region Category
            MenuItem menuCategory = new MenuItem();
            menuCategory.Name = MenuNameConstants.Category;
            menuCategory.DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuCategory);
            menuCategory.Order = 0;
            menuCategory.Url = "#";
            menuCategory.Icon = "fa  fa-list";
            menuCategory.CssClass = "treeview";

            menuCategory.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.Module,
                DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuModule),
                Url = "/module/index",
                Order = 0
            });

            menuCategory.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.DocumentType,
                DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuDocumentType),
                Url = "/documenttype/index",
                Order = 1
            });

            menuCategory.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.PromulgateStatus,
                DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuPromulgateStatus),
                Url = "/promulgatestatus/index",
                Order = 2
            });
            #endregion
            moduleMenuItem.Items.Add(menuCategory);

            moduleMenuItems.Add(moduleMenuItem);

            return moduleMenuItems;
        }
    }
}