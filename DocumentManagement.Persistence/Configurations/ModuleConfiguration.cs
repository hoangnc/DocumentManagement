using DocumentManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DocumentManagement.Persistence.Configurations
{
    public class ModuleConfiguration : EntityTypeConfiguration<Module>
    {
        public ModuleConfiguration()
        {
            HasKey(e => e.Id);

            ToTable("Module");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Code).HasMaxLength(128);
            Property(e => e.Name).HasMaxLength(200);

            Property(e => e.CreatedBy).IsRequired()
                .HasMaxLength(64);
            Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2");

            Property(e => e.ModifiedBy)
                .HasMaxLength(64);
            Property(e => e.ModifiedOn).HasColumnType("datetime2");

            Property(e => e.Deleted).IsRequired();

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
