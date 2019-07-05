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
        public ModuleMenuItems Menu => BuildDocumentMenu();

        private ModuleMenuItems BuildDocumentMenu()
        {
            ModuleMenuItems moduleMenuItems = new ModuleMenuItems();

            ModuleMenuItem moduleMenuItem = new ModuleMenuItem();
            moduleMenuItem.Name = MenuNameConstants.DocumentManagement;
            moduleMenuItem.DisplayName = "Quản lý tài liệu ISO";
            moduleMenuItem.Order = 0;

            #region Release Document

            MenuItem menuReleaseDocument = new MenuItem();
            menuReleaseDocument.Name = MenuNameConstants.ReleaseDocument;
            menuReleaseDocument.DisplayName = "Ban hành tài liệu";
            menuReleaseDocument.Order = 0;
            menuReleaseDocument.Url = "#";
            menuReleaseDocument.Icon = "fa fa-cog";
            menuReleaseDocument.CssClass = "treeview";

            menuReleaseDocument.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.ReleaseNewDocument,
                DisplayName = "Ban hành tài liệu mới",
                Url = "#",
                Order = 0
            });

            menuReleaseDocument.Items.Add(new MenuItem
            {
                Name = MenuNameConstants.ReviewDocument,
                DisplayName = "Xoán xét tài liệu",
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