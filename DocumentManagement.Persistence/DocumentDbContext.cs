using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence.ComplexTypes;
using DT.Core.Text;
using EntityFramework.Functions;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DocumentManagement.Persistence
{
    public class DocumentDbContext : DbContext
    {
        private const string dbo = nameof(dbo);

        public DocumentDbContext() : base(ConfigurationManager.ConnectionStrings["DocumentConnectionString"].ConnectionString)
        {
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<DocumentDbContext, Migrations.Configuration>());
            Database.SetInitializer(new DocumentDbInitializer());
        }

        public DocumentDbContext(string documentConnectionString) : base(documentConnectionString)
        {
            //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<DocumentDbContext, Migrations.Configuration>());
            Database.SetInitializer(new DocumentDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new FunctionConvention<DocumentDbContext>());
            modelBuilder.ComplexType<SplitInformation>();

            //Adds configurations for entities from separate class
            modelBuilder.Configurations.AddFromAssembly(typeof(DocumentDbContext).Assembly);

        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Appendice> Appendices { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<FormType> FormTypes { get; set; }
        public DbSet<PromulgateStatus> PromulgateStatuses { get; set; }
        public DbSet<Module> Modules { get; set; }

        [DbFunction("DocumentDbContext", "STRING_SPLIT")]
        public IQueryable<string> Split(string text, char? delimiter)
        {
            ObjectParameter textParameter = !text.IsNullOrEmpty() ?
                new ObjectParameter("Text", text) :
                new ObjectParameter("Text", typeof(string));

            ObjectParameter delimiterParameter = delimiter.HasValue ?
               new ObjectParameter("Delimiter", delimiter) :
               new ObjectParameter("Delimiter", ' ');

            return ((IObjectContextAdapter)this).ObjectContext
                .CreateQuery<string>(
                    string.Format("[{0}].{1}", GetType().Name,
                        "[STRING_SPLIT](@Text, @Delimiter)"), textParameter, delimiterParameter);
        }

        [TableValuedFunction("STRING_SPLIT", "DocumentDbContext",  Schema = dbo, StoreFunctionName = "STRING_SPLIT")]
        public IEnumerable<SplitInformation> StringSplit([Parameter(DbType = "nvarchar(max)")] string text,
             [Parameter(DbType = "char")] string delimiter)
        {
            return Function.CallNotSupported<IEnumerable<SplitInformation>>();
        }

        [ComposableScalarValuedFunction(nameof(CompareTwoFiles), Schema = dbo)]
        [return: Parameter(DbType = "bit")]
        public bool CompareTwoFiles(string source, string des, string delimiter)
        {
            return Function.CallNotSupported<bool>();
        }

        [ComposableScalarValuedFunction(nameof(NonUnicode), Schema = dbo)]
        [return: Parameter(DbType = "nvarchar(max)")]
        public string NonUnicode(string inputVar)
        {
            return Function.CallNotSupported<string>();
        }
    }
}
