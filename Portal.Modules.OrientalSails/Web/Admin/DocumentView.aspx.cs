using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class DocumentView : SailsAdminBase
    {
        #region -- PAGE EVENTS --
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Lấy về danh mục Category
                var list = Module.DocumentGetCategory();
                rptCategories.DataSource = list;
                rptCategories.DataBind();
                if (Request.QueryString["docid"] != null)
                {
                    var doc = Module.DocumentGetById(Convert.ToInt32(Request.QueryString["docid"]));
                    rptDocument.DataSource = Module.DocumentGetByCategory(doc.Id);
                    rptDocument.DataBind();
                }
                else
                {
                    if (list.Count > 0)
                    {
                        var doc = list[0] as DocumentCategory;
                        rptDocument.DataSource = Module.DocumentGetByCategory(doc.Id);
                        rptDocument.DataBind();
                    }
                }
            }
        }
        #endregion

        protected void rptChilds_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is DocumentCategory)
            {
                var doc = (DocumentCategory)e.Item.DataItem;

                using (var hplEdit = (HyperLink)e.Item.FindControl("hplEdit"))
                {
                    hplEdit.NavigateUrl = string.Format("DocumentView.aspx?NodeId={0}&SectionId={1}&docid={2}",
                        Node.Id, Section.Id, doc.Id);
                    if (doc.Id.ToString() == Request.QueryString["docid"])
                        hplEdit.ForeColor = Color.Red;
                }
            }
        }

        protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is DocumentCategory)
            {
                var doc = (DocumentCategory)e.Item.DataItem;


                using (var hplEdit = (HyperLink)e.Item.FindControl("hplEdit"))
                {
                    hplEdit.NavigateUrl = string.Format("DocumentView.aspx?NodeId={0}&SectionId={1}&docid={2}",
                        Node.Id, Section.Id, doc.Id);
                    if (doc.Id.ToString() == Request.QueryString["docid"])
                        hplEdit.ForeColor = Color.Red;
                }

                var list = Module.ChildGetByCategory(doc.Id);
                if (list.Count > 0)
                {
                    var rptChilds = (Repeater)e.Item.FindControl("rptChilds");
                    rptChilds.DataSource = list;
                    rptChilds.DataBind();
                }
            }
        }


        protected void rptDocument_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var doc = e.Item.DataItem as DocumentCategory;
            if (doc != null)
            {
                var hplView = e.Item.FindControl("hplView") as HyperLink;
                var hplDownload = e.Item.FindControl("hplDownload") as HyperLink;
                if (hplView != null)
                    hplView.NavigateUrl = string.Format("DocumentViewer.aspx?NodeId={0}&SectionId={1}&docid={2}",
                        Node.Id,
                        Section.Id, doc.Id);
                if (hplDownload != null) hplDownload.NavigateUrl = doc.Url;
            }
        }
    }
}