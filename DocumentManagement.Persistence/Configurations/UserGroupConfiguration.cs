using DocumentManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DocumentManagement.Persistence.Configurations
{
    public class UserGroupConfiguration : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupConfiguration()
        {
            HasKey(e => e.Id);

            ToTable("UserGroup");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.UserName).HasMaxLength(64);
            Property(e => e.Groups).HasMaxLength(4000);

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
