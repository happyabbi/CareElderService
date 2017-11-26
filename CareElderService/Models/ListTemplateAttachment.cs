using System.Collections.Generic;
using ReflectSoftware.Facebook.Messenger.Common.Models;
using FileUploadsInAspNetMvc.Models;

namespace FileUploadsInAspNetMvc.Models
{
    public class ListTemplateAttachment : Attachment<ListTemplatePayload>
    {
        public ListTemplateAttachment() : base("template") { }

        public ListTemplateAttachment(List<MyListElement> elements,string topElementStyle, List<Button> buttons) : this()
        {
            Payload = new ListTemplatePayload(elements, topElementStyle,buttons);
        }
    }
}