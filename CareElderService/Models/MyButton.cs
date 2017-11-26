using ReflectSoftware.Facebook.Messenger.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FileUploadsInAspNetMvc.Models
{
    public class MyButton : Button
    {
        public MyButton(string type) : base(type)
        {

        }
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

    }
}