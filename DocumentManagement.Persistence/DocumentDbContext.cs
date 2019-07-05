using DocumentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Persistence
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext() : base(ConfigurationManager.ConnectionStrings["DocumentConnectionString"].ConnectionString)
        {
            Database.SetInitializer(new DocumentDbInitializer());
        }

        public DocumentDbContext(string documentConnectionString) : base(documentConnectionString)
        {
            Database.SetInitializer(new DocumentDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Adds configurations for entities from separate class
            modelBuilder.Configurations.AddFromAssembly(typeof(DocumentDbContext).Assembly);
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Appendice> Appendices { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<FormType> FormTypes { get; set; }
        public DbSet<PromulgateStatus> PromulgateStatuses { get; set; }
    }
}
