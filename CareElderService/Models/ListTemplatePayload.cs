using Newtonsoft.Json;
using System.Collections.Generic;
using ReflectSoftware.Facebook.Messenger.Common.Models;

namespace FileUploadsInAspNetMvc.Models
{
    public class ListTemplatePayload : TemplatePayload
    {
        public ListTemplatePayload() : base("list")
        {

        }

        public ListTemplatePayload(List<MyListElement> elements, string topElementStyle, List<Button> buttons) : this()
        {
            Elements = elements;
            TopElementStyle = topElementStyle;
            Buttons = buttons;
        }

        [JsonProperty("top_element_style", NullValueHandling = NullValueHandling.Ignore)]
        public string TopElementStyle { get; set; }

        // <summary>
        /// Data for each bubble in message
        /// Bubbles per message (horizontal scroll): 10 elements
        /// </summary>
        [JsonProperty("elements", NullValueHandling = NullValueHandling.Ignore)]
        public List<MyListElement> Elements { get; set; }

        [JsonProperty("buttons", NullValueHandling = NullValueHandling.Ignore)]
        public List<Button> Buttons { get; set; }

    }
}