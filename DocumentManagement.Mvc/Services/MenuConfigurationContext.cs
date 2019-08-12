using Abp.Localization;
using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Mvc.Constants;
using DT.Core.Web.Common.Models;
using DT.Core.Web.Ui.Navigation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Services
{
    public static class SimpleCache
    {
        private static List<GetAllDocumentTypesDto> _documentTypes;

        public static List<GetAllDocumentTypesDto> DocumentTypes
        {
            get
            {
                if (_documentTypes == null)
                {

                    _documentTypes = new List<GetAllDocumentTypesDto>();

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "SDTC",
                        Name = "SĐTC"

                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "ST",
                        Name = "Sổ tay"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "CS",
                        Name = "Chính sách"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "TNSM",
                        Name = "Tầm nhìn sư mệnh"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "TT",
                        Name = "Thủ tục"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "QC",
                        Name = "Quy chế"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "CN",
                        Name = "Cẩm nang"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "QT",
                        Name = "Quy trình"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "QD",
                        Name = "Quy định"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "NQ",
                        Name = "Nội quy"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "DL",
                        Name = "Điều lệ"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "QCA",
                        Name = "Quy cách"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "QTD",
                        Name = "Quyết định"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "HD",
                        Name = "Hướng dẫn"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "TC",
                        Name = "Tiêu chuẩn"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "DM",
                        Name = "Định mức"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "TB",
                        Name = "Thông báo"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "CK",
                        Name = "Cam kết"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "TNQH",
                        Name = "Trách nhiệm quyền hạn"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "MSDS",
                        Name = "MSDS"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "PL",
                        Name = "Phụ lục"
                    });

                    _documentTypes.Add(new GetAllDocumentTypesDto
                    {
                        Code = "OR",
                        Name = "Khác"
                    });
                }
                return _documentTypes;
            }
        }
    }
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

        public const string OperationData = "OperationData";  
    }

    public class MenuConfigurationContext : IMenuConfigurationContext
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly IMediator _mediator;
        public MenuConfigurationContext(ILocalizationManager localizationManager, IMediator mediator)
        {
            _localizationManager = localizationManager;
            _mediator = mediator;
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

            #region Categories
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

            #region Operation Data
            MenuItem menuOperationData = new MenuItem();
            menuOperationData.Name = MenuNameConstants.OperationData;
            menuOperationData.DisplayName = _localizationManager.GetString(DocumentResourceNames.DocumentResourceName, DocumentResourceNames.MenuOperationData);
            menuOperationData.Order = 0;
            menuOperationData.Url = "#";
            menuOperationData.Icon = "fa  fa-list";
            menuOperationData.CssClass = "treeview";

            var documentTypes = SimpleCache.DocumentTypes;
            if(documentTypes != null && documentTypes.Any())
            {
                foreach(var documentType in documentTypes)
                {
                    menuOperationData.Items.Add(new MenuItem
                    {
                        Name = documentType.Code,
                        DisplayName = documentType.Name,
                        Url = $"/OperationData/list?code={documentType.Code}",
                        Order = 2
                    });
                }
            }

            #endregion
            moduleMenuItem.Items.Add(menuOperationData);

            moduleMenuItems.Add(moduleMenuItem);

            return moduleMenuItems;
        }
    }
}