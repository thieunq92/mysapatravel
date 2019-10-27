using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CMS.ServerControls
{
    public class ButtonLink : Control
    {
        public string Text
        {
            get
            {
                if (ViewState["Text"] != null)
                {
                    return ViewState["Text"].ToString();
                }
                return string.Empty;
            }
            set
            {
                ViewState["Text"] = value;
            }
        }

        public string NavigateUrl
        {
            get
            {
                if (ViewState["NavigateUrl"] != null)
                {
                    return ViewState["NavigateUrl"].ToString();
                }
                return string.Empty;
            }
            set
            {
                ViewState["NavigateUrl"] = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Text);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("window.location = '{0}';", NavigateUrl));
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            base.Render(writer);
        }
    }
}
