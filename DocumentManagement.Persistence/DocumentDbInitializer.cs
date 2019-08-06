using DocumentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DocumentManagement.Persistence
{
    public class DocumentDbInitializer : CreateDatabaseIfNotExists<DocumentDbContext>
    {
        public override void InitializeDatabase(DocumentDbContext context)
        {
            base.InitializeDatabase(context);
            /*context.Database.ExecuteSqlCommand(
           @"DROP FUNCTION IF EXISTS [dbo].[STRING_SPLIT]
            CREATE FUNCTION [dbo].[STRING_SPLIT](@Text NVARCHAR(max), @Delimiter CHAR(1))
           RETURNS @Output TABLE(SplitData NVARCHAR(MAX))
           BEGIN
                   DECLARE @start INT, @end INT 
            SELECT @start = 1, @end = CHARINDEX(@delimiter, @Text) 
            WHILE @start < LEN(@Text) + 1 BEGIN 
                IF @end = 0  
                    SET @end = LEN(@Text) + 1
       
                INSERT INTO @output (splitdata)  
                VALUES(SUBSTRING(@Text, @start, @end - @start)) 
                SET @start = @end + 1 
                SET @end = CHARINDEX(@delimiter, @Text, @start)
        
            END
			           RETURN 
           END");

            context.Database.ExecuteSqlCommand(
                @"
DROP FUNCTION IF EXISTS [dbo].[CompareTwoFiles];
CREATE FUNCTION [dbo].[CompareTwoFiles](
                    @SourceFiles NVARCHAR(MAX), 
                    @DesFiles NVARCHAR(MAX),
                    @Delimiter CHAR(1))
                    RETURNS BIT
                    AS 
                    BEGIN 
                       DECLARE @Count INT

                       SELECT @Count = COUNT(1) FROM [dbo].[STRING_SPLIT](@SourceFiles, @Delimiter) AS SO
                       INNER JOIN (SELECT SplitData FROM [dbo].[STRING_SPLIT](@DesFiles, @Delimiter)) AS DE
                       ON DE.SplitData = SO.SplitData
                       IF(@Count > 0)
                       BEGIN
	                    RETURN CAST(1 AS BIT)
                       END
                       RETURN CAST(0 AS BIT)
                    END"
                );

            context.Database.ExecuteSqlCommand(
                @"
DROP FUNCTION IF EXISTS [dbo].[NonUnicode];
CREATE FUNCTION [dbo].[NonUnicode](@InputVar NVARCHAR(MAX) )
RETURNS NVARCHAR(MAX)
AS
BEGIN    
    IF (@inputVar IS NULL OR @inputVar = '')  RETURN ''
   
    DECLARE @RT NVARCHAR(MAX)
    DECLARE @SIGN_CHARS NCHAR(256)
    DECLARE @UNSIGN_CHARS NCHAR (256)
 
    SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' + NCHAR(272) + NCHAR(208)
    SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'
 
    DECLARE @COUNTER int
    DECLARE @COUNTER1 int
   
    SET @COUNTER = 1
    WHILE (@COUNTER <= LEN(@inputVar))
    BEGIN  
        SET @COUNTER1 = 1
        WHILE (@COUNTER1 <= LEN(@SIGN_CHARS) + 1)
        BEGIN
            IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@inputVar,@COUNTER ,1))
            BEGIN          
                IF @COUNTER = 1
                    SET @inputVar = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@inputVar, @COUNTER+1,LEN(@inputVar)-1)      
                ELSE
                    SET @inputVar = SUBSTRING(@inputVar, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@inputVar, @COUNTER+1,LEN(@inputVar)- @COUNTER)
                BREAK
            END
            SET @COUNTER1 = @COUNTER1 +1
        END
        SET @COUNTER = @COUNTER +1
    END
    -- SET @inputVar = replace(@inputVar,' ','-')
    RETURN @inputVar
END
");*/
        }
        protected override void Seed(DocumentDbContext context)
        {
            SeedDocumentType(context);
            context.SaveChanges();
        }

        private void SeedDocumentType(DocumentDbContext context)
        {
            List<DocumentType> documentTypes = new List<DocumentType>();

            documentTypes.Add(new DocumentType
            {
                Code = "SDTC",
                Name = "SĐTC",
                CreatedBy = "nguyenconghoang",
                CreatedOn = DateTime.Now,
                Deleted = false
            });

            documentTypes.Add(new DocumentType
            {
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
