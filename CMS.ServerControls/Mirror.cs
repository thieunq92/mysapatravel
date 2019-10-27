using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
// Designer support 

namespace CMS.ServerControls
{
    /// <summary>
    /// Mirror control.
    /// Duplicates the rendered Html of another control on the page.
    /// </summary>
    [ToolboxData("<{0}:Mirror runat=server></{0}:Mirror>")]
    public class Mirror : WebControl
    {
        private string _controlIdToMirror;

        [Browsable(true)]
        public string ControlIDToMirror
        {
            get { return _controlIdToMirror; }
            set { _controlIdToMirror = value; }
        }

        #region METHODS

        protected override void Render(HtmlTextWriter writer)
        {
            if (!Visible)
                return;

            // Ensure that the ControlID was defined
            // otherwise abort the render
            if (ControlIDToMirror == null)
                return;

            // Locate the control identified by ControlID
            Control c = Parent.FindControl(ControlIDToMirror);

            // If the specified control was not found, abort
            if (c == null)
            {
                if (Parent.Parent != null)
                    c = Parent.Parent.FindControl(ControlIDToMirror);
                if (c==null)
                {
                    foreach (Control parent in Parent.Parent.Controls)
                    {
                        parent.FindControl(ControlIDToMirror);
                    }
                }
                if (c==null)
                return;
            }

            // Call the control's Render function in order to
            // generate the Mirror control's HTML.
            // This, in a nutshell is the mirroring process.
            c.RenderControl(writer);
        }

        #endregion
    }
}