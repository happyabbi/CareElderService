using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class CareElderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PSId { get; set; }

        public string Sender { get; set; }

        public string CareSubject { get; set; }

        public string SubjectType { get; set; }

        public string CareContent { get; set; }

        public string ContentType { get; set; }

        public string Memo { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public string SendTime1 { get; set; }

        public string SendTime2 { get; set; }

        public string SendTime3 { get; set; }

        public string SendTime4 { get; set; }


    }
}