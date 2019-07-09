namespace DocumentManagement.Persistence.Migrations
{
    using DocumentManagement.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DocumentDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DocumentManagement.Persistence.DocumentDbContext";
        }

        protected override void Seed(DocumentDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            SeedDocumentType(context);
        }

        private void SeedDocumentType(DocumentDbContext context)
        {
            List<DocumentType> documentTypes = new List<DocumentType>();

            documentTypes.Add(new DocumentType {
                Code = "SDTC",
                Name = "SĐTC",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType {
                Code = "ST",
                Name = "Sổ tay",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "CS",
                Name = "Chính sách",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "TNSM",
                Name = "Tầm nhìn sư mệnh",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "TT",
                Name = "Thủ tục",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "QC",
                Name = "Quy chế",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "CN",
                Name = "Cẩm nang",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "QT",
                Name = "Quy trình",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "QD",
                Name = "Quy định",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "NQ",
                Name = "Nội quy",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "DL",
                Name = "Điều lệ",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "QCA",
                Name = "Quy cách",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "QTD",
                Name = "Quyết định",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "HD",
                Name = "Hướng dẫn",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "TC",
                Name = "Tiêu chuẩn",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "DM",
                Name = "Định mức",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "TB",
                Name = "Thông báo",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "CK",
                Name = "Cam kết",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "TNQH",
                Name = "Trách nhiệm quyền hạn",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "MSDS",
                Name = "MSDS",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
                Code = "OR",
                Name = "Khác",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });
            context.DocumentTypes.AddOrUpdate(document => new { document.Code }, documentTypes.ToArray());
        }
    }
}
