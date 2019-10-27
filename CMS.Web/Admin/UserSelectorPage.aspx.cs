using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.ServerControls;
using CMS.Web.UI;
using NHibernate.Criterion;

namespace CMS.Web.Admin
{
    public partial class UserSelectorPage : KitModuleAdminBasePage
    {
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected ScriptManager scriptManager;
        protected TextBox textBoxName;
        protected Button btnSearch;
        protected UpdatePanel updatePanelUsers;
        protected Mirror mirrorPager;
        protected Repeater rptUsers;
        protected Pager pagerUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetDataSource();
            this.rptUsers.DataBind();
        }

        protected override bool CanByPassCanEditCheck()
        {
            return true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.ViewState["Name"] = (object)this.textBoxName.Text;
            this.GetDataSource();
            this.rptUsers.DataBind();
        }

        protected void GetDataSource()
        {
            if (this.ViewState["Name"] != null)
                this.rptUsers.DataSource = base.CoreRepository.FindUsersByUsername(this.ViewState["Name"].ToString());
            else
                this.rptUsers.DataSource = base.CoreRepository.FindUsersByUsername("");
        }

        protected void rptUsers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (this.Request.QueryString["Command"] == null || !(e.Item.DataItem is CMS.Core.Domain.User))
                return;
            CMS.Core.Domain.User dataItem = (CMS.Core.Domain.User)e.Item.DataItem;
            string str = this.Request.QueryString["Command"];
            HtmlAnchor control = e.Item.FindControl("processData") as HtmlAnchor;
            if (control != null)
                control.Attributes.Add("onClick", "window.opener." + str + "('" + (object)dataItem.Id + "','" + dataItem.UserName + "'); self.close();");
        }

        protected void pagerUsers_PageChanged(object sender, PageChangedEventArgs e)
        {
            this.GetDataSource();
            this.rptUsers.DataBind();
        }
    }
}
