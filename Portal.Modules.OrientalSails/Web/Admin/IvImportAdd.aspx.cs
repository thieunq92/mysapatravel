using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GemBox.Spreadsheet;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// thêm , sửa phiếu nhập
    /// </summary>
    public partial class IvImportAdd : SailsAdminBasePage, ICallbackEventHandler
    {
        #region ---PRIVATE MEMBER---

        private IvImport _currentImport;
        private IList _ivUnits;

        private double _total; // Luu tong gia tri hoa don khi item databound

        private IvImport CurrentImport
        {
            get
            {
                if (_currentImport == null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["ImportId"]))
                    {
                        _currentImport = Module.GetById<IvImport>(Convert.ToInt32(Request.QueryString["ImportId"]));
                    }
                    else
                    {
                        _currentImport = new IvImport();
                    }
                }
                return _currentImport;
            }
        }

        #endregion

        #region ---PAGE EVENT---

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = @"Quản lý phiếu nhập";
            //rptProductList.RenderedItem =
            //    @"<tr class=""item"">                <td> <input type=""text"" placeholder=""Mã sản phẩm"" onfocus=""GetProduct(\'ajaxloader\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtCodeProduct\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_lblName,ajaxloader,ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtUnitPrice,ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_hddProductId\');"" onchange=""GetProduct(\'ajaxloader\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtCodeProduct\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_lblName,ajaxloader,ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtUnitPrice,ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_hddProductId\');"" id=""ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtCodeProduct"" value="""" name=""ct100$ctl00$AdminContentMain$AdminContent$rptProductList$ctl█$txtCodeProduct"">     <input type=""button"" class=""button"" value=""Chọn"" onclick=""buildUrl(\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_hddProductId\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_lblName\')"">                <input type=""hidden"" placeholder=""Tên sản phẩm"" value=""0"" id=""ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_hddProductImportId"" name=""ct100$ctl00$AdminContentMain$AdminContent$rptProductList$ctl█$hddProductImportId""> <input type=""hidden"" value=""0"" id=""ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_hddProductId"" name=""ct100$ctl00$AdminContentMain$AdminContent$rptProductList$ctl█$hddProductId""></td>            <td> <span id=""ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_lblName""></span> </td>            <td> <input type=""text"" placeholder=""Số luợng"" onchange=""Calculate(\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtUnitPrice\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtQuantity\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtTotal\');"" onkeyup=""Calculate(\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtUnitPrice\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtQuantity\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtTotal\');"" id=""ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtQuantity"" value=""0"" name=""ct100$ctl00$AdminContentMain$AdminContent$rptProductList$ctl█$txtQuantity""> </td>             <td> <input type=""text"" placeholder=""Đơn giá"" onchange=""Calculate(\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtUnitPrice\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtQuantity\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtTotal\');"" onkeyup=""Calculate(\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtUnitPrice\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtQuantity\',\'ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtTotal\');"" id=""ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtUnitPrice"" value=""0"" name=""ct100$ctl00$AdminContentMain$AdminContent$rptProductList$ctl█$txtUnitPrice""> </td>             <td> <input type=""text"" placeholder=""Thành tiền"" id=""ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_txtTotal"" value=""0"" name=""ct100$ctl00$AdminContentMain$AdminContent$rptProductList$ctl█$txtTotal""> </td>             <td> <input align=""absmiddle"" type=""image"" style=""border-width: 0px;"" onclick="""" alt=""Delete"" src=""/Images/delete_file.gif"" class=""image_button16"" title=""Delete"" id=""ctl00_ctl00_AdminContentMain_AdminContent_rptProductList_ctl█_btnDelete"" name=""ct100$ctl00$AdminContentMain$AdminContent$rptProductList$ctl█$btnDelete""> </td> </tr>";
            //rptProductList.HasHeader = true;
            //btnAddProductJava.Attributes.Add("onclick", rptProductList.AddItemScript);


            string argCode = string.Format("document.getElementById({0}).value", "arg");
            string storageId = string.Format("document.getElementById('{0}').value", ddlStorage.ClientID);
            // Tạo đoạn code đăng ký phương thức callback, trong đó txtCode là tham số truyền vào call back, ValidateProductCode là phương thức xử lý dữ liệu callback
            string cbReference = Page.ClientScript.GetCallbackEventReference(this, argCode + "+" + "'|'" + "+" + storageId, "LoadProduct", "context",
                "LoadProduct", true);
            // Đăng ký phương thức vừa tạo
            string cbScript = "function UseCallback(arg,context)" + "{" + cbReference + ";" + "}";

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "UseCallback", cbScript, true);

            if (!string.IsNullOrEmpty(Request.QueryString["ImportId"]))
            {
                _currentImport = Module.GetById<IvImport>(Convert.ToInt32(Request.QueryString["ImportId"]));
            }
            _ivUnits = Module.IvUnitGetAll(null);

            if (!IsPostBack)
            {
                LoadDataSelectList();
                int count = Module.CountImportByDateTime(Request.QueryString, UserIdentity, DateTime.Today) + 1;
                txtCode.Text = string.Format(@"PN{0}-{1}", DateTime.Today.ToString("ddMMyyyy"), count);
                txtName.Text = string.Format(@"Phiếu nhập ngày {0}-{1}", DateTime.Today.ToString("dd/MM/yyyy"), count);

                txtImportedDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                LoadInfoImport();
                if (CurrentImport.Id > 0)
                {
                    BindrptProductImportList();
                }
            }

        }
        private void LoadDataSelectList()
        {
            ddlCruise.DataSource = Module.CruiseGetByUser(UserIdentity);
            ddlCruise.DataTextField = "Name";
            ddlCruise.DataValueField = "Id";
            ddlCruise.DataBind();
            ddlStorage.DataSource = Module.IvStorageGetByUser(UserIdentity);
            ddlStorage.DataTextField = "Name";
            ddlStorage.DataValueField = "Id";
            ddlStorage.DataBind();
            var role = Module.RoleGetByName("Cruise Suppliers");
            var agencys = Module.AgencyGetAllByRole(role);
            ddlAgency.DataSource = agencys;
            ddlAgency.DataTextField = "Name";
            ddlAgency.DataValueField = "Id";
            ddlAgency.DataBind();

        }
        #endregion

        #region ---PRIVATE METHOD---

        private void BindrptProductImportList()
        {
            var count = 0;
            IList list = new List<IvProductImport>();
            list = Module.IvProductImportGetByImport(UserIdentity, CurrentImport, Request.QueryString,
                0, 0, out count);
            if (count == 0)
            {
                list = new ArrayList();
                list.Add(new Domain.IvProductImport());
            }
            _total = 0;
            Session["IvProductImport"] = list;
            rptProductList.DataSource = list;
            rptProductList.DataBind();
            rptProductListItems.Value = list.Count.ToString();

        }
        private void LoadInfoImport()
        {
            if (CurrentImport.Id > 0)
            {
                txtName.Text = _currentImport.Name;
                txtCode.Text = _currentImport.Code;
                txtImportedDate.Text = _currentImport.ImportDate.ToString("dd/MM/yyyy");
                if (_currentImport.Agency != null) ddlAgency.SelectedValue = _currentImport.Agency.ToString();
                txtImportBy.Text = _currentImport.ImportedBy;
                txtDetail.Text = _currentImport.Detail;
                txtTotal1.Text = _currentImport.Total.ToString();
                ddlStorage.SelectedValue = _currentImport.Storage != null ? _currentImport.Storage.Id.ToString() : "";
            }
        }

        #endregion

        #region ---CONTROL EVENT---

        #region ---Repeater---

        protected void rptProductList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    var hddProductImportId = e.Item.FindControl("hddProductImportId") as HiddenField;
                    if (hddProductImportId != null && !string.IsNullOrEmpty(hddProductImportId.Value))
                    {
                        IvProductImport productImport = Module.GetById<IvProductImport>(Convert.ToInt32(hddProductImportId.Value));
                        Module.Delete(productImport);
                        RefeshProductImportList(Convert.ToInt32(hddProductImportId.Value));
                    }
                    break;
                default:
                    break;
            }
        }

        private void RefeshProductImportList(int id)
        {
            IList data = new ArrayList();
            var total = 0.0;
            foreach (RepeaterItem item in rptProductList.Items)
            {
                IvProductImport productImport = new IvProductImport();

                HiddenField hddProductImportId = (HiddenField)item.FindControl("hddProductImportId");
                HiddenField hddProductId = (HiddenField)item.FindControl("hddProductId");
                TextBox txtCodeProduct = (TextBox)item.FindControl("txtCodeProduct");
                TextBox txtQuantity = (TextBox)item.FindControl("txtQuantity");
                TextBox txtUnitPrice = (TextBox)item.FindControl("txtUnitPrice");
                TextBox txtItemTotal = (TextBox)item.FindControl("txtTotal");
                var ddlUnit = (DropDownList)item.FindControl("ddlUnit");

                if (!string.IsNullOrEmpty(hddProductImportId.Value))
                {
                    productImport.Id = Convert.ToInt32(hddProductImportId.Value);
                }
                if (!string.IsNullOrEmpty(txtCodeProduct.Text))
                {
                    productImport.Product = Module.IvProductGetByCode(txtCodeProduct.Text.Trim(), _currentImport.Storage);
                }
                if (!string.IsNullOrEmpty(hddProductId.Value))
                {
                    productImport.Product = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                }
                if (!string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    productImport.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                }
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    productImport.Quantity = Convert.ToInt32(txtQuantity.Text);
                }
                if (!string.IsNullOrEmpty(txtItemTotal.Text))
                {
                    productImport.Total = Convert.ToDouble(txtItemTotal.Text);
                }
                if (!string.IsNullOrEmpty(ddlUnit.SelectedValue))
                {
                    productImport.Unit = Module.GetById<IvUnit>(Convert.ToInt32(ddlUnit.SelectedValue));
                }
                productImport.Import = CurrentImport;
                if (productImport.Id != id)
                {
                    total += productImport.Total;
                    data.Add(productImport);
                }
            }
            rptProductList.DataSource = data;
            rptProductList.DataBind();
            txtGrandTotal.Text = string.Format("{0:0.00}", total);
            rptProductListItems.Value = data.Count.ToString();
        }

        protected void rptProductList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Domain.IvProductImport)
            {
                Domain.IvProductImport productImport = (Domain.IvProductImport)e.Item.DataItem;
                double quantity = 0;
                double unitprice = 0;

                HiddenField hddId = e.Item.FindControl("hddProductImportId") as HiddenField;
                HiddenField hddProductId = e.Item.FindControl("hddProductId") as HiddenField;
                if (hddId != null)
                {
                    if (productImport.Id > 0)
                    {
                        hddId.Value = productImport.Id.ToString();
                    }
                }
                if (hddProductId != null)
                {
                    if (productImport.Product != null)
                    {
                        hddProductId.Value = productImport.Product.Id.ToString();
                    }
                }
                Label lblName = e.Item.FindControl("lblName") as Label;
                if (lblName != null)
                {
                    if (productImport.Product != null)
                    {
                        lblName.Text = productImport.Product.Name;
                    }
                    else
                    {
                        lblName.Text = "";
                    }
                }

                TextBox txtUnitPrice = e.Item.FindControl("txtUnitPrice") as TextBox;
                TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
                TextBox txtItemTotal = e.Item.FindControl("txtTotal") as TextBox;
                var ddlUnit = (DropDownList)e.Item.FindControl("ddlUnit");
                if (ddlUnit != null)
                {
                    ddlUnit.DataSource = _ivUnits;
                    ddlUnit.DataValueField = "Id";
                    ddlUnit.DataTextField = "NameTree";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, new ListItem(" -- chọn --", ""));
                    if (productImport.Unit != null)
                        ddlUnit.SelectedValue = productImport.Unit.Id.ToString();
                }
                if (txtUnitPrice != null && txtQuantity != null && txtItemTotal != null)
                {
                    if (productImport.UnitPrice > 0)
                    {
                        txtUnitPrice.Text = productImport.UnitPrice.ToString();
                        unitprice = Convert.ToDouble(productImport.UnitPrice.ToString());
                    }
                    else
                    {
                        txtUnitPrice.Text = "";
                    }
                    txtUnitPrice.Attributes.Add("onkeyup", string.Format("Calculate('{0}','{1}','{2}');", txtUnitPrice.ClientID, txtQuantity.ClientID, txtItemTotal.ClientID));
                    txtUnitPrice.Attributes.Add("onchange", string.Format("Calculate('{0}','{1}','{2}');", txtUnitPrice.ClientID, txtQuantity.ClientID, txtItemTotal.ClientID));

                    if (productImport.Quantity > 0)
                    {
                        txtQuantity.Text = productImport.Quantity.ToString();
                        quantity = Convert.ToDouble(productImport.Quantity.ToString());
                    }
                    else
                    {
                        txtQuantity.Text = "";
                    }
                    txtQuantity.Attributes.Add("onkeyup", string.Format("Calculate('{0}','{1}','{2}');", txtUnitPrice.ClientID, txtQuantity.ClientID, txtItemTotal.ClientID));
                    txtQuantity.Attributes.Add("onchange", string.Format("Calculate('{0}','{1}','{2}');", txtUnitPrice.ClientID, txtQuantity.ClientID, txtItemTotal.ClientID));

                    double total = quantity * unitprice;
                    _total += total;
                    txtItemTotal.Text = total.ToString();
                }

                TextBox txtCodeProduct = e.Item.FindControl("txtCodeProduct") as TextBox;
                if (txtCodeProduct != null && lblName != null && txtUnitPrice != null && hddProductId != null)
                {
                    if (productImport.Product != null)
                    {
                        txtCodeProduct.Text = productImport.Product.Code;
                    }
                    else
                    {
                        txtCodeProduct.Text = "";
                    }
                    txtCodeProduct.Attributes.Add("onchange",
                        string.Format("GetProduct('{0}','{1}','{2},{0},{3},{4}');", "ajaxloader",
                            txtCodeProduct.ClientID, lblName.ClientID, txtUnitPrice.ClientID, hddProductId.ClientID));
                    txtCodeProduct.Attributes.Add("onfocus",
                        string.Format("GetProduct('{0}','{1}','{2},{0},{3},{4}');", "ajaxloader",
                            txtCodeProduct.ClientID, lblName.ClientID, txtUnitPrice.ClientID, hddProductId.ClientID));
                }
            }

            txtGrandTotal.Text = _total.ToString("#,0.#");
            txtTotal1.Text = _total.ToString("#,0.#");
        }

        #endregion

        protected void btnSaveProductImport_Click(object sender, EventArgs e)
        {
            try
            {
                #region ---Save Current Export---

                CurrentImport.Name = txtName.Text;
                int flag = Module.CountImportGetByCode(Request.QueryString, UserIdentity, txtCode.Text);
                if (flag > 0 && CurrentImport.Id <= 0)
                {
                    ShowError("Mã phiếu đã tồn tại");
                    return;
                }
                _currentImport.Code = txtCode.Text;
                _currentImport.Detail = txtDetail.Text;
                if (!string.IsNullOrWhiteSpace(ddlAgency.SelectedValue))
                    _currentImport.Agency = Module.AgencyGetById(Convert.ToInt32(ddlAgency.SelectedValue));
                _currentImport.ImportedBy = txtImportBy.Text;
                if (!string.IsNullOrEmpty(txtImportedDate.Text))
                {
                    _currentImport.ImportDate = DateTime.ParseExact(txtImportedDate.Text, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);
                }
                else
                {
                    _currentImport.ImportDate = DateTime.Now;
                }
                IvStorage storage = null;
                if (!string.IsNullOrEmpty(ddlStorage.SelectedValue))
                {
                    storage = Module.GetById<IvStorage>(Convert.ToInt32(ddlStorage.SelectedValue));
                }
                _currentImport.Storage = storage;
                if (storage != null && storage.Cruise != null) _currentImport.Cruise = storage.Cruise;
                Module.SaveOrUpdate(_currentImport, UserIdentity);

                #endregion

                double sum = 0;

                foreach (RepeaterItem item in rptProductList.Items)
                {
                    HiddenField hddProductImportId = (HiddenField)item.FindControl("hddProductImportId");
                    HiddenField hddProductId = (HiddenField)item.FindControl("hddProductId");
                    TextBox txtCodeProduct = (TextBox)item.FindControl("txtCodeProduct");
                    TextBox txtQuantity = (TextBox)item.FindControl("txtQuantity");
                    TextBox txtUnitPrice = (TextBox)item.FindControl("txtUnitPrice");
                    var ddlUnit = (DropDownList)item.FindControl("ddlUnit");

                    if (!string.IsNullOrEmpty(hddProductImportId.Value))
                    {
                        int id = Convert.ToInt32(hddProductImportId.Value);
                        IvProductImport productImport = Module.GetById<IvProductImport>(id);

                        productImport.Product = Module.IvProductGetByCode(txtCodeProduct.Text.Trim(), _currentImport.Storage);
                        if (!string.IsNullOrWhiteSpace(hddProductId.Value))
                            productImport.Product = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                        if (!string.IsNullOrWhiteSpace(ddlUnit.SelectedValue))
                            productImport.Unit = Module.GetById<IvUnit>(Convert.ToInt32(ddlUnit.SelectedValue));
                        if (!string.IsNullOrWhiteSpace(txtQuantity.Text))
                            productImport.Quantity = Convert.ToInt32(txtQuantity.Text);
                        if (!string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                            productImport.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                        if (!string.IsNullOrWhiteSpace(txtQuantity.Text) && !string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                            productImport.Total = Convert.ToInt32(txtQuantity.Text) * Convert.ToDouble(txtUnitPrice.Text);
                        productImport.Import = CurrentImport;
                        productImport.Storage = storage;
                        sum += productImport.Total;

                        if (productImport.Product != null && productImport.Product.Id > 0) Module.SaveOrUpdate(productImport, UserIdentity);

                    }
                    else
                    {
                        IvProductImport productImport = new Domain.IvProductImport();

                        productImport.Product = Module.IvProductGetByCode(txtCodeProduct.Text.Trim(), _currentImport.Storage);
                        if (!string.IsNullOrWhiteSpace(hddProductId.Value))
                            productImport.Product = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                        if (ddlUnit != null && !string.IsNullOrWhiteSpace(ddlUnit.SelectedValue))
                            productImport.Unit = Module.GetById<IvUnit>(Convert.ToInt32(ddlUnit.SelectedValue));
                        if (!string.IsNullOrWhiteSpace(txtQuantity.Text))
                            productImport.Quantity = Convert.ToInt32(txtQuantity.Text);
                        if (!string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                            productImport.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                        if (!string.IsNullOrWhiteSpace(txtQuantity.Text) && !string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                            productImport.Total = Convert.ToInt32(txtQuantity.Text) * Convert.ToDouble(txtUnitPrice.Text);
                        productImport.Import = CurrentImport;
                        productImport.Storage = storage;

                        sum += productImport.Total;

                        if (productImport.Product != null && productImport.Product.Id > 0) Module.SaveOrUpdate(productImport, UserIdentity);
                    }

                }

                CurrentImport.Total = sum;
                Module.SaveOrUpdate(CurrentImport, UserIdentity);

                BindrptProductImportList();

                PageRedirect(string.Format("IvImportAdd.aspx?NodeId={0}&SectionId={1}&ImportId={2}", Node.Id, Section.Id,
                    _currentImport.Id));
            }
            catch (Exception ex)
            {
                ShowErrors(ex.Message);
            }
        }

        protected void btnAddNewProduct_Click(object sender, EventArgs e)
        {
            IList data = new ArrayList();
            foreach (RepeaterItem item in rptProductList.Items)
            {
                Domain.IvProductImport productImport = new Domain.IvProductImport();

                HiddenField hddProductImportId = (HiddenField)item.FindControl("hddProductImportId");
                HiddenField hddProductId = (HiddenField)item.FindControl("hddProductId");
                TextBox txtCodeProduct = (TextBox)item.FindControl("txtCodeProduct");
                TextBox txtQuantity = (TextBox)item.FindControl("txtQuantity");
                TextBox txtUnitPrice = (TextBox)item.FindControl("txtUnitPrice");
                TextBox txtItemTotal = (TextBox)item.FindControl("txtTotal");
                var ddlUnit = (DropDownList)item.FindControl("ddlUnit");


                if (!string.IsNullOrEmpty(hddProductImportId.Value))
                {
                    productImport.Id = Convert.ToInt32(hddProductImportId.Value);
                }
                if (!string.IsNullOrEmpty(txtCodeProduct.Text))
                {
                    productImport.Product = Module.IvProductGetByCode(txtCodeProduct.Text.Trim(), _currentImport.Storage);
                }
                if (!string.IsNullOrEmpty(hddProductId.Value))
                {
                    productImport.Product = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                }
                if (!string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    productImport.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                }
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    productImport.Quantity = Convert.ToInt32(txtQuantity.Text);
                }
                if (!string.IsNullOrEmpty(txtItemTotal.Text))
                {
                    productImport.Total = Convert.ToDouble(txtItemTotal.Text);
                }
                if (!string.IsNullOrEmpty(ddlUnit.SelectedValue))
                {
                    productImport.Unit = Module.GetById<IvUnit>(Convert.ToInt32(ddlUnit.SelectedValue));
                }
                productImport.Import = CurrentImport;
                data.Add(productImport);
            }

            data.Add(new Domain.IvProductImport());
            rptProductList.DataSource = data;
            _total = 0;
            rptProductList.DataBind();
            rptProductListItems.Value = data.Count.ToString();

        }

        #endregion
        protected void ddlCRuise_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (IList<IvStorage>)Module.IvStorageGetByUser(UserIdentity);
            ddlStorage.DataSource = list.Where(s => s.Cruise.Id == Convert.ToInt32(ddlCruise.SelectedValue));
            ddlStorage.DataTextField = "Name";
            ddlStorage.DataValueField = "Id";
            ddlStorage.DataBind();
        }
        #region Implementation of ICallbackEventHandler

        private string _callbackResult;

        public void RaiseCallbackEvent(string eventArgument)
        {
            if (!string.IsNullOrEmpty(eventArgument))
            {
                var str = eventArgument.Split('|');
                IvStorage storage = null;
                if (!string.IsNullOrEmpty(str[1]))
                {
                    storage = Module.GetById<IvStorage>(Convert.ToInt32(str[1]));
                }
                IvProduct product = Module.IvProductGetByCode(str[0], storage);
                if (product != null)
                {
                    _callbackResult = product.Name + "#" + 0 + "#" + product.Id;
                }
                else
                {
                    _callbackResult = "Không tồn tại sản phẩm#0";
                }
            }
        }

        public string GetCallbackResult()
        {
            return _callbackResult;
        }

        #endregion

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("IvProductImportList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
        }

        protected void btnExportExcel_OnClick(object sender, EventArgs e)
        {
            ExcelFile excelFile = ExcelFile.Load(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/phieu_nhap.xlsx"));
            ExcelWorksheet sheet = excelFile.Worksheets[0];


            var index = 1;
            sheet.Cells["B3"].Value = txtName.Text;
            sheet.Cells["B4"].Value = txtImportedDate.Text;
            if (ddlAgency.SelectedItem != null) sheet.Cells["B5"].Value = ddlAgency.SelectedItem.Text;
            sheet.Cells["D3"].Value = txtCode.Text;
            sheet.Cells["D4"].Value = txtImportBy.Text;
            sheet.Cells["D5"].Value = ddlStorage.SelectedItem.Text;

            const int firstrow = 7;
            int crow = firstrow;
            sheet.Rows.InsertCopy(crow, rptProductList.Items.Count, sheet.Rows[firstrow]);
            var total = 0.0;
            foreach (RepeaterItem item in rptProductList.Items)
            {
                try
                {
                    TextBox txtCodeProduct = (TextBox)item.FindControl("txtCodeProduct");
                    HiddenField hddProductId = (HiddenField)item.FindControl("hddProductId");
                    TextBox txtQuantity = (TextBox)item.FindControl("txtQuantity");
                    TextBox txtUnitPrice = (TextBox)item.FindControl("txtUnitPrice");
                    var ddlUnit = (DropDownList)item.FindControl("ddlUnit");
                    var txtTotal = (TextBox)item.FindControl("txtTotal");
                    sheet.Cells[crow, 0].Value = txtCodeProduct.Text;
                    if (!string.IsNullOrWhiteSpace(hddProductId.Value))
                    {
                        var product = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                        sheet.Cells[crow, 1].Value = product.Name;
                    }
                    sheet.Cells[crow, 2].Value = txtQuantity.Text;
                    sheet.Cells[crow, 3].Value = ddlUnit.SelectedItem.Text.Replace(".", "").Replace("|", "").Replace("_", "");
                    sheet.Cells[crow, 4].Value = txtUnitPrice.Text;
                    sheet.Cells[crow, 5].Value = txtTotal.Text;
                    total += (Convert.ToInt32(txtQuantity.Text) * Convert.ToDouble(txtUnitPrice.Text));
                }
                catch (Exception ex)
                {
                    total += 0;
                }
                crow++;
            }

            sheet.Cells[crow, 4].Value = "Tổng";
            sheet.Cells[crow, 5].Value = total.ToString("#,##0.##");
            excelFile.Save(Response, string.Format("phieu_nhap{0}.xlsx", txtImportedDate.Text));
        }
    }
}