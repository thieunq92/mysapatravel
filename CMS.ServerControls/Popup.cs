using System;
using System.Web.UI;

namespace CMS.ServerControls
{
    public class Popup: Control
    {
        private const string script = @"function openPopup(pageURL, title,h,w) {
var left = (screen.width/2)-(w/2);
var top = (screen.height/2)-(h/2);
var targetWin = window.open (pageURL, title, 'toolbar=no,scrollbars=yes, location=no, directories=no, status=no, menubar=no, resizable=no, copyhistory=no, width='+w+', height='+h+', top='+top+', left='+left);
targetWin.focus();
return targetWin;
}";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.ClientScript.RegisterStartupScript(typeof(Popup), "popup", script, true);
        }

        public static string OpenPopupScript(string url, string title, int height, int width)
        {
            // IE6 don't accept blank space, so replace them with 
            title = title.Replace(" ", "");
            return string.Format("openPopup('{0}','{1}',{2},{3});", url, title, height, width);
        }

        public static void RegisterStartupScript(Page page)
        {
            page.ClientScript.RegisterStartupScript(typeof(Popup), "popup", script, true);
        }
    }
}
