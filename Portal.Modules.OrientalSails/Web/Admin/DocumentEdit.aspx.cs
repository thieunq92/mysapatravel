using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class DocumentEdit : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var list = Module.DocumentGetCategory();
                var tree = new List<DocumentCategory>();
                foreach (DocumentCategory category in list)
                {
                    tree.Add(category);
                    var childs = Module.ChildGetByCategory(category.Id);
                    foreach (DocumentCategory child in childs)
                    {
                        child.Name = "  |__ " + child.Name;
                        tree.Add(child);
                    }
                }
                ddlSuppliers.DataSource = tree;
                ddlSuppliers.DataTextField = "Name";
                ddlSuppliers.DataValueField = "Id";
                ddlSuppliers.DataBind();
                ddlSuppliers.Items.Insert(0, "-- Category --");
                if (!string.IsNullOrWhiteSpace(Request.QueryString["catid"]))
                {
                    ddlSuppliers.SelectedValue = Request.QueryString["catid"];
                }
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
                }
                else
                {
                    btnDelete.Visible = false;
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
            doc.IsCategory = false;
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
            ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "<script>window.parent.location.href = window.parent.location.href;</script>");
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var doc = Module.DocumentGetById(Convert.ToInt32(Request.QueryString["docid"]));
            Module.Delete(doc);
            ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "<script>window.parent.location.href = window.parent.location.href;</script>");
        }
    }
}