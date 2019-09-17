using DocumentManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DocumentManagement.Persistence.Configurations
{
    public class AppendiceConfiguration : EntityTypeConfiguration<Appendice>
    {
        public AppendiceConfiguration()
        {
            HasKey(e => e.Id);

            ToTable("Appendice");
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.DocumentId);

            Property(e => e.AppendiceNumber)
                .HasMaxLength(128);

            Property(e => e.Code)
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

            Property(e => e.ReviewNumber)
                .HasMaxLength(128);

            Property(e => e.EffectiveDate)
                .HasColumnType("datetime2");
            Property(e => e.ReviewDate)
                .HasColumnType("datetime2");
            Property(e => e.ScopeOfApplication);

            Property(e => e.ScopeOfDeloyment)
                .HasMaxLength(512);

            Property(e => e.ReplaceOf)
                .HasMaxLength(128);
            Property(e => e.ReplaceEffectiveDate)
                .HasColumnType("datetime2");
            Property(e => e.RelateToDocuments)
                .HasMaxLength(1024);
            Property(e => e.DDCAudited);
            Property(e => e.LinkFile).HasMaxLength(1024);

            Property(e => e.Active);
            Property(e => e.FormType);
            Property(e => e.PromulgateStatusId);
            Property(e => e.StatusId);

            Property(e => e.CreatedBy).IsRequired()
                .HasMaxLength(64);
            Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2");

            Property(e => e.ModifiedBy)
                .HasMaxLength(64);
            Property(e => e.ModifiedOn).HasColumnType("datetime2");

            Property(e => e.Deleted).IsRequired();
        }
    }
}
