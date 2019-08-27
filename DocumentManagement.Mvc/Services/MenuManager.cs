using DT.Core.Authorization;
using DT.Core.Web.Common.Models;
using DT.Core.Web.Ui.Navigation;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Services
{
    public class MenuManager : IMenuManager
    {
        public const string SidebarMenuBeginTemplate = @"<ul class=""sidebar-menu"" data-widget=""tree"">";
        public const string SidebarMenuEndTemplate = @"</ul>";
        public const string SidebarHeaderTemplate = @"<li class=""header"">{0}</li>";
        public const string LeafMenuTemplate = @" <li class=""{3}"">
                                                    <a href=""{0}"">
                                                        <i class=""{1}""></i>
                                                        <span>{2}</span>                                               
                                                    </a>
                                                    </li>
                                                ";
        public IMenuConfigurationContext MenuConfigurationContext => DependencyResolver.Current.GetService<IMenuConfigurationContext>();

        public string SelectedMenu { get; set; }
               
        public string RenderToHtml()
        {
            StringBuilder htmlBuilder = new StringBuilder();

            ModuleMenuItems menu = MenuConfigurationContext.Menu;

            if (menu != null)
            {
                foreach (ModuleMenuItem moduleMenu in menu)
                {
                    // Begin Menu
                    htmlBuilder.AppendLine(SidebarMenuBeginTemplate);

                    // Header Menu
                    htmlBuilder.AppendLine(string.Format(SidebarHeaderTemplate, moduleMenu.DisplayName));

                    if (moduleMenu.Items.Any())
                    {
                        foreach (MenuItem menuItem in moduleMenu.Items)
                        {
                            if (menuItem.IsLeaf)
                            {
                                if (!string.IsNullOrEmpty(SelectedMenu)
                                    && SelectedMenu == menuItem.Name)
                                {
                                    htmlBuilder.AppendLine(string.Format(LeafMenuTemplate,
                                        menuItem.Url,
                                        menuItem.Icon,
                                        menuItem.DisplayName,
                                        "active"));
                                }
                                else
                                {
                                    htmlBuilder.AppendLine(string.Format(LeafMenuTemplate,
                                       menuItem.Url,
                                       menuItem.Icon,
                                       menuItem.DisplayName,
                                       string.Empty));
                                }
                            }
                            else
                            {
                                var hasSelectedMenu = menuItem.Items.Any(m => m.Name == SelectedMenu);

                                if (hasSelectedMenu)
                                    htmlBuilder.AppendLine(@"<li class=""treeview active menu-open"">");
                                else
                                    htmlBuilder.AppendLine(@"<li class=""treeview"">");

                                htmlBuilder.AppendLine(string.Format(@" <a href=""{0}"">
                                                        <i class=""{1}""></i>
                                                        <span>{2}</span> 
<span class=""pull-right-container"">
                        <i class=""fa fa-angle-left pull-right""></i>
                    </span>
                                                    </a>",
                                    menuItem.Url,
                                    menuItem.Icon,
                                    menuItem.DisplayName));

                                htmlBuilder.AppendLine(@"<ul class=""treeview-menu"">");

                                foreach (var childMenuItem in menuItem.Items)
                                {
                                    if (!string.IsNullOrEmpty(SelectedMenu)
                                    && SelectedMenu == childMenuItem.Name)
                                    {
                                        htmlBuilder.AppendLine(string.Format(LeafMenuTemplate,
                                        childMenuItem.Url,
                                        childMenuItem.Icon,
                                        childMenuItem.DisplayName,
                                        "active"));
                                    }
                                    else
                                    {
                                        htmlBuilder.AppendLine(string.Format(LeafMenuTemplate,
                                        childMenuItem.Url,
                                        childMenuItem.Icon,
                                        childMenuItem.DisplayName,
                                        string.Empty));
                                    }
                                }
                                htmlBuilder.AppendLine(@"</ul>");

                                htmlBuilder.AppendLine("</li>");
                            }
                        }
                    }
                    // End Menu
                    htmlBuilder.AppendLine(SidebarMenuEndTemplate);
                }
            }

            return htmlBuilder.ToString();
        }

        public MenuItem FindMenu(string menuName)
        {
            throw new NotImplementedException();
        }

    }
}