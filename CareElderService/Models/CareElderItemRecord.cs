using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class CareElderItemRecord
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string PSId { get; set; }


        public string SubjectType { get; set; }

        [Display (Name ="項目")]
        public string Subject { get; set; }

        [Display(Name = "結果")]
        public string Result { get; set; }

        [Display(Name = "圖片")]
        public string ImageUrl { get; set; }

        [Display(Name = "建立時間")]
        public DateTime CreateDateTime { get; set; }
    }
}