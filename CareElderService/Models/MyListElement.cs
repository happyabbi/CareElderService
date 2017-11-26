using Newtonsoft.Json;
using System.Collections.Generic;

namespace FileUploadsInAspNetMvc.Models
{
    public class MyListElement
    {
        /// <summary>
        /// Bubble title
        /// has a 80 character limit
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Bubble subtitle
        /// has a 80 character limit
        /// </summary>
        [JsonProperty("subtitle", NullValueHandling = NullValueHandling.Ignore)]
        public string Subtitle { get; set; }

        /// <summary>
        /// Bubble image
        /// </summary>
        [JsonProperty("image_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Bubble image
        /// </summary>
        [JsonProperty("default_action", NullValueHandling = NullValueHandling.Ignore)]
        public URLButton DefaultAction { get; set; }
    }

    public class URLButton {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string URL { get; set; }
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
    }

}