
using DT.Core.Data.Entities;
using System;

namespace DocumentManagement.Domain.Entities
{
    public class Appendice : BaseEntity<int>
    {
        public int DocumentId { get; set; }
        public string Code { get; set; }  
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string Module { get; set; }
        public string DocumentType { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string AppendiceNumber { get; set; }
        public string ReviewNumber { get; set; }
        public string Approver { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ScopeOfApplication { get; set; }
        public string ScopeOfDeloyment { get; set; }
        public string ReplaceOf { get; set; }
        public DateTime? ReplaceEffectiveDate { get; set; }
        public string RelateToDocuments { get; set; }
        public bool DDCAudited { get; set; }
        public string LinkFile { get; set; }

        public int StatusId { get; set; }
        public int FormType { get; set; }
        public bool Active { get; set; }
        public int PromulgateStatusId { get; set; }
    }
}
