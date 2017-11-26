using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class CareElderExercisingRecordViewModel
    {
        [Required]
        public string PSId { get; set; }

        public string SubjectType { get; set; }

        [Display(Name = "運動")]
        public string Subject { get; set; }

        [Display(Name = "長度(分鐘)")]
        public string Result { get; set; }

        [Display(Name = "日期時間")]
        public DateTime CreateDateTime { get; set; }
    }
}