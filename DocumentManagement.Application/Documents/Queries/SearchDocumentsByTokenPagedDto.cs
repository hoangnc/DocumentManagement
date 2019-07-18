﻿using System;

namespace DocumentManagement.Application.Documents.Queries
{
    public class SearchDocumentsByTokenPagedDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string Module { get; set; }
        public string DocumentType { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string DocumentNumber { get; set; }
        public string ReviewNumber { get; set; }
        public string Description { get; set; }
        public string ContentChange { get; set; }
        public string Drafter { get; set; }
        public string Auditor { get; set; }
        public string Approver { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ScopeOfApplication { get; set; }
        public string ScopeOfDeloyment { get; set; }
        public string ReplaceOf { get; set; }
        public DateTime? ReplaceEffectiveDate { get; set; }
        public string RelateToDocuments { get; set; }
        public bool DDCAudited { get; set; }
        public string FolderName { get; set; }
        public string LinkFile { get; set; }
    }
}