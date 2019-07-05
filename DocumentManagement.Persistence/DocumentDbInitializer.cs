using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Persistence
{
    public class DocumentDbInitializer : CreateDatabaseIfNotExists<DocumentDbContext>
    {
        protected override void Seed(DocumentDbContext context)
        {
        }
    }
}
