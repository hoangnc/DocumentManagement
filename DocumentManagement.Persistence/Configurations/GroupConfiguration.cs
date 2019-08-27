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
    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            HasKey(e => e.Id);

            ToTable("Group");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Code).IsRequired().HasMaxLength(128);
            Property(e => e.Name).IsRequired().HasMaxLength(200);
            Property(e => e.Email).IsRequired()
               .HasMaxLength(64);

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
