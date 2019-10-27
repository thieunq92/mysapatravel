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
    /// thêm, sửa thông tin sản phẩm
    /// </summary>
    public partial class IvProductAdd : SailsAdminBase
    {
        private IvProduct _product = new IvProduct();
        private IvStorage _ivStorage = new IvStorage();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Sản phẩm";
            if (!string.IsNullOrWhiteSpace(Request["id"]))
            {
                _product = Module.IvProductGetById(Convert.ToInt32(Request["id"]));
            }
            if (!string.IsNullOrWhiteSpace(Request["storageId"]))
            {
                _ivStorage = Module.IvStorageGetById(Convert.ToInt32(Request["storageId"]));
            }
            if (!IsPostBack)
            {
                FillData();
                if (_product.Id > 0) FillInfo();
            }
        }

        private void FillInfo()
        {
            txtName.Text = _product.Name;
            txtCode.Text = _product.Code;
            txtNote.Text = _product.Note;
            ckInRoomService.Checked = _product.InRoomService;
            chkIsTool.Checked = _product.IsTool;
            ddlCategory.SelectedValue = _product.Category.Id.ToString();
            if (_product.Unit != null)
                ddlUnit.SelectedValue = _product.Unit.Id.ToString();

        }

        private void FillData()
        {
            ddlCategory.DataSource = Module.IvCategoryGetAll(null);
            ddlCategory.DataValueField = "Id";
            ddlCategory.DataTextField = "NameTree";
            ddlCategory.DataBind();

            ddlUnit.DataSource = Module.IvUnitGetAllParent(null);
            ddlUnit.DataValueField = "Id";
            ddlUnit.DataTextField = "Name";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem(" -- chọn --", ""));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _product.Name = txtName.Text;
            _product.Code = txtCode.Text;
            _product.Note = txtNote.Text;
            _product.InRoomService = ckInRoomService.Checked;
            _product.IsTool = chkIsTool.Checked;
            _product.Category = Module.IvCategoryGetById(Convert.ToInt32(ddlCategory.SelectedValue));
            if (!string.IsNullOrWhiteSpace(ddlUnit.SelectedValue))
                _product.Unit = Module.GetById<IvUnit>(Convert.ToInt32(ddlUnit.SelectedValue));

            if (!(_product.Id > 0))
            {
                _product.CreatedDate = DateTime.Now;
            }
            Module.SaveOrUpdate(_product);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
        }
    }
}