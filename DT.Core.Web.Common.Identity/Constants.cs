namespace DT.Core.Web.Common.Identity
{
    public static class Constants
    {
        public static class DtClaimTypes
        {
            public const string UserImage = "user_image";
            public const string Permission = "permission";
            public const string Department = "department";
        }

        public static class DtPermissionBaseTypes
        {
            public const string Read = "read";
            public const string Write = "write";
            public const string Update = "update";
            public const string Delete = "delete";
            public const string View = "view";
            public const string Import = "import";
            public const string Export = "export";
            public const string Sync = "sync";
        }

        public static class MainPages
        {
            public const string Users = "Danh sách người dùng";
            public const string Settings = "Cấu hình hệ thống";
        }
    }
}
