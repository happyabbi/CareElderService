using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FileUploadsInAspNetMvc.Models
{
    public class CareElderEatingViewModel  //
    {
        [Required]
        public string PSId { get; set; }

        [Display(Name = "轉傳人Id")]
        public string Sender { get; set; }

        [Display(Name = "主旨類型")]
        public string SubjectType { get; set; }

        [Display(Name = "主旨")]
        public string CareSubject { get; set; }

        [Display(Name = "內容類型")]
        public string ContentType { get; set; }

        [Display(Name = "內容")]
        public string CareContent { get; set; }

        [Display(Name = "備註")]
        public string Memo { get; set; }

        [Display(Name = "圖片")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        [Display(Name = "早餐")]
        public string SendTime1 { get; set; }

        [Display(Name = "午餐")]
        public string SendTime2 { get; set; }

        [Display(Name = "晚餐")]
        public string SendTime3 { get; set; }

        [Display(Name = "傳送時間4")]
        public string SendTime4 { get; set; }


    }
}