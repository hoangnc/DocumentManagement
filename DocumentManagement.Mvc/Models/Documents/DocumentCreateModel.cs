using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocumentManagement.Mvc.Models.Documents
{
    public class DocumentCreateModel
    {
        [Display(Name = "Mã số tài liệu")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(128, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string Code { get; set; }

        [Display(Name = "Mã công ty")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(64, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string CompanyCode { get; set; }


    }
}