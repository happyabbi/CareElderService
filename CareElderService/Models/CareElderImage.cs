using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class CareElderImage
    {
        [Key]
        public int Id { get; set; }

        public string PSId { get; set; }

        public string SubjectType { get; set; }

        public string ImageUrl { get; set; }
    }
}