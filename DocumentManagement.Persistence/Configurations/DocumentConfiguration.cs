using DocumentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Persistence.Configurations
{
    public class DocumentConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentConfiguration()
        {
            HasKey(e => e.Id);

            ToTable("Document");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e=>e.Code)
                .IsRequired()
                .HasMaxLength(128);

            Property(e => e.CompanyCode)
                .IsRequired()
                .HasMaxLength(64);

            Property(e => e.CompanyName)
                .HasMaxLength(200);

            Property(e => e.DepartmentCode)
                .HasMaxLength(64);

            Property(e => e.DepartmentName)
                .HasMaxLength(200);

            Property(e => e.Module)
                .HasMaxLength(128);

            Property(e => e.DocumentType)
                .IsRequired()
                .HasMaxLength(128);

            Property(e => e.Name)
                .HasMaxLength(512);

            Property(e => e.FileName)
                .HasMaxLength(128);

            Property(e => e.DocumentNumber)
                .HasMaxLength(128);

            Property(e => e.ReviewNumber)
                .HasMaxLength(128);

            Property(e => e.Description);

            Property(e => e.ContentChange)
                .HasMaxLength(4000);

            Property(e => e.Drafter)
                .HasMaxLength(128);
            Property(e => e.Auditor)
                .HasMaxLength(128);
            Property(e => e.Approver)
                .HasMaxLength(128);

            Property(e => e.EffectiveDate)
                .HasColumnType("datetime2");
            Property(e => e.ReviewDate)
                .HasColumnType("datetime2");
            Property(e => e.ScopeOfApplication)
                .HasMaxLength(512);
            Property(e => e.ScopeOfDeloyment)
                .HasMaxLength(512);

            Property(e => e.Active);
            Property(e => e.FormType);
            Property(e => e.PromulgateStatusId);
            Property(e => e.StatusId);

            Property(e=>e.ReplaceOf)
                .HasMaxLength(128);
            Property(e => e.ReplaceEffectiveDate)
                .HasColumnType("datetime2");
            Property(e => e.RelateToDocuments)
                .HasMaxLength(1024);
            Property(e => e.DDCAudited);
            Property(e => e.FolderName).HasMaxLength(64);
            Property(e => e.LinkFile).HasMaxLength(1024);
        }
    }
}
