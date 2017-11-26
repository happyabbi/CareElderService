using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class CareElderItemRecordViewModel
    {

        [Required]
        public string PSId { get; set; }

        public string SubjectType { get; set; }

        [Display (Name ="用藥")]
        public string Subject { get; set; }

        [Display(Name = "結果")]
        public string Result { get; set; }
        /*
         *      [DataType(DataType.Upload)]
                public HttpPostedFileBase ImageUpload { get; set; }

         *
         */

        [Display(Name = "日期時間")]
        public DateTime CreateDateTime { get; set; }
    }
}