using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// trang thêm, sửa danh mục sản phẩm
    /// </summary>
    public partial class IvCategoryAdd : SailsAdminBase
    {
        private IvCategory _category = new IvCategory();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Danh mục";
            if (!string.IsNullOrWhiteSpace(Request["id"]))
            {
                _category = Module.IvCategoryGetById(Convert.ToInt32(Request["id"]));
            }
            if (!IsPostBack)
            {
                FillParentData();
                FillInfo();
            }
        }

        private void FillInfo()
        {
            txtName.Text = _category.Name;
            txtNote.Text = _category.Note;
//            txtNumberOfProduct.Text = _category.NumberOfProduct.ToString();
            if (_category.Parent != null)
            {
                ddlParent.SelectedValue = _category.Parent.Id.ToString();
            }
        }

        private void FillParentData()
        {

            ddlParent.DataSource = Module.IvCategoryGetAll(_category);
            ddlParent.DataValueField = "Id";
            ddlParent.DataTextField = "NameTree";
            ddlParent.DataBind();
            ddlParent.Items.Insert(0, new ListItem(" -- chọn --", ""));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _category.Name = txtName.Text;
            _category.Note = txtNote.Text;
//            if (!string.IsNullOrWhiteSpace(txtNumberOfProduct.Text)) _category.NumberOfProduct = Convert.ToInt32(txtNumberOfProduct.Text);
            if (!string.IsNullOrWhiteSpace(ddlParent.SelectedValue))
            {
                _category.Parent = Module.IvCategoryGetById(Convert.ToInt32(ddlParent.SelectedValue));
            }
            Module.SaveOrUpdate(_category);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
        }
    }
}