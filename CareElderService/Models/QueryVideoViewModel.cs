using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class QueryVideoViewModel
    {
        [Display (Name ="慢性病")]
        public string VideoCategory { get; set; }
    }
}