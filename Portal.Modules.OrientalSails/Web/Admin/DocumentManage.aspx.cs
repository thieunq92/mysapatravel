using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.Util;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class DocumentManage : SailsAdminBase
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

                ddlSuppliers.DataSource = list;
                ddlSuppliers.DataTextField = "Name";
                ddlSuppliers.DataValueField = "Id";
                ddlSuppliers.DataBind();
                ddlSuppliers.Items.Insert(0, "-- Category --");

                if (Request.QueryString["docid"] != null)
                {
                    var doc = Module.DocumentGetById(Convert.ToInt32(Request.QueryString["docid"]));
                    txtServiceName.Text = doc.Name;
                    txtNote.Text = doc.Note;
                    if (doc.Parent != null)
                    {
                        ddlSuppliers.SelectedValue = doc.Parent.Id.ToString();
                    }
                    if (!string.IsNullOrEmpty(doc.Url))
                    {
                        hplCurrentFile.NavigateUrl = doc.Url;
                        hplCurrentFile.Text = FileHelper.GetFileName(doc.Url);
                        hplCurrentFile.Visible = true;
                    }
                    btnDelete.Visible = true;
                    rptDocument.DataSource = Module.DocumentGetByCategory(doc.Id);
                    rptDocument.DataBind();
                }
                else
                {
                    btnDelete.Visible = false;
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
                    hplEdit.NavigateUrl = string.Format("DocumentManage.aspx?NodeId={0}&SectionId={1}&docid={2}",
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
                    hplEdit.NavigateUrl = string.Format("DocumentManage.aspx?NodeId={0}&SectionId={1}&docid={2}",
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DocumentCategory doc;
            if (Request.QueryString["docid"] != null)
            {
                doc = Module.DocumentGetById(Convert.ToInt32(Request.QueryString["docid"]));
            }
            else
            {
                doc = new DocumentCategory();
            }
            doc.Name = txtServiceName.Text;
            doc.Note = txtNote.Text;
            doc.IsCategory = true;
            if (ddlSuppliers.SelectedIndex > 0)
            {
                doc.Parent = Module.DocumentGetById(Convert.ToInt32(ddlSuppliers.SelectedValue));
            }
            else
            {
                doc.Parent = null;
            }
            if (fileUpload.HasFile)
            {
                doc.Url = FileHelper.Upload(fileUpload, "Documents/");
            }
            Module.SaveOrUpdate(doc, UserIdentity);
            PageRedirect(string.Format("DocumentManage.aspx?NodeId={0}&SectionId={1}&docid={2}", Node.Id, Section.Id, doc.Id));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var doc = Module.DocumentGetById(Convert.ToInt32(Request.QueryString["docid"]));
            Module.Delete(doc);
            Response.Redirect("DocumentManage.aspx?NodeId=1&SectionId=15");
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