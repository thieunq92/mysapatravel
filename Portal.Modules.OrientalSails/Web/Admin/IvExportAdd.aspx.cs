using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using GemBox.Spreadsheet;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// thêm,sửa phiếu xuất
    /// </summary>
    public partial class IvExportAdd : SailsAdminBasePage, ICallbackEventHandler
    {
        #region --- PRIVATE MEMEBER ---
        private IList _ivUnits;

        private IvExport _currentExport;
        private BookingRoom _bookingRoom;

        private double _total; // Luu tong gia tri hoa don khi item databound

        #endregion

        #region --- PAGE EVENT ---

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = @"Quản lý phiếu xuất";
            if (!string.IsNullOrEmpty(Request.QueryString["ExportId"]) && Request.QueryString["ExportId"] != "0")
            {
                _currentExport = Module.GetById<IvExport>(Convert.ToInt32(Request.QueryString["ExportId"]));
            }
            else _currentExport = new IvExport();
            if (!string.IsNullOrEmpty(Request.QueryString["bookingRoomId"]))
            {
                _bookingRoom = Module.GetById<BookingRoom>(Convert.ToInt32(Request.QueryString["bookingRoomId"]));
                phInfoRoom.Visible = true;
                //var info = _bookingRoom.Room.Name + Environment.NewLine;
                //foreach (Customer customer in _bookingRoom.Customers)
                //{
                //    info += customer.Fullname + Environment.NewLine;
                //}
                litRoom.Text = _bookingRoom.Room.Name;
            }

            string argCode = string.Format("document.getElementById({0}).value", "arg");
            //            string storageId = string.Format("document.getElementById('{0}').value", ddlStorage.ClientID);
            string storageId = string.Format("document.getElementById('{0}').value", 0);
            // Tạo đoạn code đăng ký phương thức callback, trong đó txtCode là tham số truyền vào call back, ValidateProductCode là phương thức xử lý dữ liệu callback
            string cbReference = Page.ClientScript.GetCallbackEventReference(this,
                argCode + "+" + "'|'" + "+" + storageId, "LoadProduct", "context",
                "LoadProduct", true);
            // Đăng ký phương thức vừa tạo
            string cbScript = "function UseCallback(arg,context)" + "{" + cbReference + ";" + "}";

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "UseCallback", cbScript, true);

            _ivUnits = Module.IvUnitGetAll(null);

            if (!IsPostBack)
            {
                LoadStorage();
                int count = Module.CountExportByDateTime(Request.QueryString, UserIdentity, DateTime.Today) + 1;
                txtCode.Text = string.Format("PX{0}-{1}", DateTime.Today.ToString("ddMMyyyy"), count);
                txtName.Text = string.Format("Phiếu xuất ngày {0}-{1}", DateTime.Today.ToString("dd/MM/yyyy"), count);

                txtExportedDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                LoadInfoExport();
                if (_currentExport.Id > 0)
                {
                    BindrptProductList();
                }
            }
            if (!UserIdentity.HasPermission(AccessLevel.Administrator))
            {
                btnSaveProductExport.Visible = false;
            }

        }

        private void LoadStorage()
        {
            //ddlStorage.DataSource = Module.IvStorageGetByUser(UserIdentity);
            //ddlStorage.DataTextField = "Name";
            //ddlStorage.DataValueField = "Id";
            //ddlStorage.DataBind();
        }

        #endregion

        #region --- CONTROL EVENT ---

        #region ---Repeater---

        protected void rptProductList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    var hddProductExportId = e.Item.FindControl("hddProductExportId") as HiddenField;
                    if (hddProductExportId != null && !string.IsNullOrEmpty(hddProductExportId.Value))
                    {
                        IvProductExport productExport =
                            Module.GetById<IvProductExport>(Convert.ToInt32(hddProductExportId.Value));
                        Module.Delete(productExport);
                        RefeshProductExportList(Convert.ToInt32(hddProductExportId.Value));
                    }
                    break;
                default:
                    break;
            }
        }

        private void RefeshProductExportList(int id)
        {
            IList data = new ArrayList();
            IvStorage storage = null;
            //if (!string.IsNullOrEmpty(ddlStorage.SelectedValue))
            //    storage = Module.GetById<IvStorage>(Convert.ToInt32(ddlStorage.SelectedValue));
            foreach (RepeaterItem item in rptProductList.Items)
            {
                IvProductExport productExport = new IvProductExport();

                HiddenField hddProductExportId = item.FindControl("hddProductExportId") as HiddenField;
                HiddenField hddProductId = (HiddenField)item.FindControl("hddProductId");

                TextBox txtCodeProduct = item.FindControl("txtCodeProduct") as TextBox;
                TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                TextBox txtUnitPrice = item.FindControl("txtUnitPrice") as TextBox;
                TextBox txtItemTotal = item.FindControl("txtTotal") as TextBox;
                var ddlUnit = (DropDownList)item.FindControl("ddlUnit");
                TextBox txtDiscount = item.FindControl("txtDiscount") as TextBox;
                var ddlDiscountType = item.FindControl("ddlDiscountType") as DropDownList;

                if (!string.IsNullOrEmpty(hddProductExportId.Value))
                {
                    productExport.Id = Convert.ToInt32(hddProductExportId.Value);
                }
                if (!string.IsNullOrEmpty(txtCodeProduct.Text))
                {
                    productExport.Product = Module.IvProductGetByCode(txtCodeProduct.Text.Trim(), storage);
                }
                if (!string.IsNullOrEmpty(hddProductId.Value))
                {
                    productExport.Product = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                }
                if (!string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    productExport.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                }
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    productExport.Quantity = Convert.ToInt32(txtQuantity.Text);
                }
                if (!string.IsNullOrEmpty(txtItemTotal.Text))
                {
                    productExport.Total = Convert.ToDouble(txtItemTotal.Text);
                }
                if (!string.IsNullOrEmpty(ddlUnit.SelectedValue))
                {
                    productExport.Unit = Module.GetById<IvUnit>(Convert.ToInt32(ddlUnit.SelectedValue));
                }
                if (!string.IsNullOrEmpty(txtDiscount.Text))
                {
                    productExport.Discount = Convert.ToInt32(txtDiscount.Text);
                }
                if (!string.IsNullOrEmpty(ddlDiscountType.SelectedValue))
                {
                    productExport.DiscountType = Convert.ToInt32(ddlDiscountType.SelectedValue);
                }
                productExport.Export = _currentExport;
                if (productExport.Id != id) data.Add(productExport);
            }

            rptProductList.DataSource = data;
            _total = 0;
            rptProductList.DataBind();
            rptProductListItems.Value = data.Count.ToString();
        }

        protected void rptProductList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is IvProductExport)
            {
                IvProductExport productExport = (IvProductExport)e.Item.DataItem;
                double quantity = 0;
                double unitprice = 0;

                HiddenField hddId = e.Item.FindControl("hddProductExportId") as HiddenField;
                if (hddId != null)
                {
                    if (productExport.Id > 0)
                    {
                        hddId.Value = productExport.Id.ToString();
                    }
                }
                HiddenField hddProductId = e.Item.FindControl("hddProductId") as HiddenField;
                if (hddProductId != null)
                {
                    if (productExport.Id > 0)
                    {
                        hddProductId.Value = productExport.Product.Id.ToString();
                    }
                }
                Label lblName = e.Item.FindControl("lblName") as Label;
                if (lblName != null)
                {
                    if (productExport.Product != null)
                    {
                        lblName.Text = productExport.Product.Name;
                    }
                    else
                    {
                        lblName.Text = "";
                    }
                }

                //TextBox txtCode = e.Item.FindControl("txtCodeProduct") as TextBox;
                //if (txtCode != null)
                //{
                //    if (productExport.Product != null)
                //    {
                //        txtCode.Text = productExport.Product.BarCode;
                //    }
                //    else
                //    {
                //        txtCode.Text = "";
                //    }
                //}

                TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
                if (txtQuantity != null)
                {
                    if (productExport.Quantity > 0)
                    {
                        txtQuantity.Text = productExport.Quantity.ToString();
                        quantity = Convert.ToDouble(productExport.Quantity.ToString());
                    }
                    else
                    {
                        txtQuantity.Text = "";
                    }
                }
                var ddlUnit = (DropDownList)e.Item.FindControl("ddlUnit");
                if (ddlUnit != null)
                {
                    ddlUnit.DataSource = _ivUnits;
                    ddlUnit.DataValueField = "Id";
                    ddlUnit.DataTextField = "NameTree";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, new ListItem(" -- chọn --", ""));
                    if (productExport.Unit != null)
                        ddlUnit.SelectedValue = productExport.Unit.Id.ToString();
                }
                TextBox txtUnitPrice = e.Item.FindControl("txtUnitPrice") as TextBox;
                if (txtUnitPrice != null)
                {
                    if (productExport.UnitPrice > 0)
                    {
                        txtUnitPrice.Text = productExport.UnitPrice.ToString();
                        unitprice = Convert.ToDouble(productExport.UnitPrice.ToString());
                    }
                    else
                    {
                        txtUnitPrice.Text = "";
                    }
                }

                TextBox txtItemTotal = e.Item.FindControl("txtTotal") as TextBox;
                double total;
                if (txtItemTotal != null)
                {
                    total = productExport.Total;
                    _total += total;
                    txtItemTotal.Text = total.ToString();
                }
                TextBox txtDiscount = e.Item.FindControl("txtDiscount") as TextBox;
                var ddlDiscountType = e.Item.FindControl("ddlDiscountType") as DropDownList;

                if (productExport.Discount > 0)
                {
                    txtDiscount.Text = productExport.Discount.ToString();
                    ddlDiscountType.SelectedValue = productExport.DiscountType.ToString();
                }
                txtQuantity.Attributes.Add("onkeyup",
                    string.Format("Calculate('{0}','{1}','{2}','{3}','{4}');", txtUnitPrice.ClientID,
                        txtQuantity.ClientID, txtItemTotal.ClientID, txtDiscount.ClientID, ddlDiscountType.ClientID));
                txtQuantity.Attributes.Add("onchange",
                    string.Format("Calculate('{0}','{1}','{2}','{3}','{4}');", txtUnitPrice.ClientID,
                        txtQuantity.ClientID, txtItemTotal.ClientID, txtDiscount.ClientID, ddlDiscountType.ClientID));

                txtUnitPrice.Attributes.Add("onkeyup",
                    string.Format("Calculate('{0}','{1}','{2}','{3}','{4}');", txtUnitPrice.ClientID,
                        txtQuantity.ClientID, txtItemTotal.ClientID, txtDiscount.ClientID, ddlDiscountType.ClientID));
                txtUnitPrice.Attributes.Add("onchange",
                    string.Format("Calculate('{0}','{1}','{2}','{3}','{4}');", txtUnitPrice.ClientID,
                        txtQuantity.ClientID, txtItemTotal.ClientID, txtDiscount.ClientID, ddlDiscountType.ClientID));
                txtDiscount.Attributes.Add("onkeyup",
                    string.Format("Calculate('{0}','{1}','{2}','{3}','{4}');", txtUnitPrice.ClientID,
                        txtQuantity.ClientID, txtItemTotal.ClientID, txtDiscount.ClientID, ddlDiscountType.ClientID));
                txtDiscount.Attributes.Add("onchange",
                    string.Format("Calculate('{0}','{1}','{2}','{3}','{4}');", txtUnitPrice.ClientID,
                        txtQuantity.ClientID, txtItemTotal.ClientID, txtDiscount.ClientID, ddlDiscountType.ClientID));
                ddlDiscountType.Attributes.Add("onchange",
                    string.Format("Calculate('{0}','{1}','{2}','{3}','{4}');", txtUnitPrice.ClientID,
                        txtQuantity.ClientID, txtItemTotal.ClientID, txtDiscount.ClientID, ddlDiscountType.ClientID));


                TextBox txtCodeProduct = e.Item.FindControl("txtCodeProduct") as TextBox;
                if (txtCodeProduct != null && lblName != null && txtUnitPrice != null)
                {
                    if (productExport.Product != null)
                    {
                        txtCodeProduct.Text = productExport.Product.Code;
                    }
                    else
                    {
                        txtCodeProduct.Text = "";
                    }
                    txtCodeProduct.Attributes.Add("onchange",
                        string.Format("GetProduct('{0}','{1}','{2},{0},{3}');", "ajaxloader",
                            txtCodeProduct.ClientID, lblName.ClientID,
                            txtUnitPrice.ClientID));
                    txtCodeProduct.Attributes.Add("onfocus",
                        string.Format("GetProduct('{0}','{1}','{2},{0},{3}');", "ajaxloader",
                            txtCodeProduct.ClientID, lblName.ClientID,
                            txtUnitPrice.ClientID));
                }
                txtGrandTotal.Text = _total.ToString("#,0.#");
                txtTotal.Text = _total.ToString("#,0.#");
                ImageButton btnDelete = e.Item.FindControl("btnDelete") as ImageButton;
                {
                    if (btnDelete != null)
                    {
                        btnDelete.CommandArgument = productExport.Id.ToString();
                    }
                }
            }
        }

        #endregion

        protected void btnSaveProductExport_Click(object sender, EventArgs e)
        {
            try
            {
                #region ---Save Customer Infomation--

                IvStorage storage = null;
                //if (!string.IsNullOrEmpty(ddlStorage.SelectedValue))
                //    storage = Module.GetById<IvStorage>(Convert.ToInt32(ddlStorage.SelectedValue));
                _currentExport.Storage = storage;

                _currentExport.CustomerName = txtCustomerName.Text;

                #endregion

                #region --- Save Current Export---

                _currentExport.Name = txtName.Text;
                int flag = Module.CountExportGetByCode(txtCode.Text);
                if (flag > 0 && _currentExport.Id <= 0)
                {
                    ShowErrors("Mã phiếu đã tồn tại");
                    return;
                }
                _currentExport.Code = txtCode.Text;
                _currentExport.Detail = txtDetail.Text;
                _currentExport.ExportedBy = UserIdentity.UserName;
                if (chkIsDebt.Checked)
                {
                    _currentExport.IsDebt = true;
                    _currentExport.Agency = txtAgency.Text;
                }
                if (!string.IsNullOrEmpty(txtExportedDate.Text))
                {
                    _currentExport.ExportDate = DateTime.ParseExact(txtExportedDate.Text, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);
                }
                else
                {
                    _currentExport.ExportDate = DateTime.Now;
                }
                if (!string.IsNullOrWhiteSpace(txtPay.Text)) _currentExport.Pay = Convert.ToDouble(txtPay.Text);
                if (_bookingRoom != null && _bookingRoom.Id > 0)
                {
                    _currentExport.BookingRoom = _bookingRoom;
                    _currentExport.Room = _bookingRoom.Room;
                }
                Module.SaveOrUpdate(_currentExport, UserIdentity);

                ShowErrors("Cập nhật dữ liệu thành công");

                #endregion

                double sum = 0;

                foreach (RepeaterItem item in rptProductList.Items)
                {
                    HiddenField hddProductExportId = item.FindControl("hddProductExportId") as HiddenField;
                    HiddenField hddProductId = (HiddenField)item.FindControl("hddProductId");

                    TextBox txtCodeProduct = item.FindControl("txtCodeProduct") as TextBox;
                    TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                    TextBox txtUnitPrice = item.FindControl("txtUnitPrice") as TextBox;
                    TextBox txtTotal = item.FindControl("txtTotal") as TextBox;
                    var ddlUnit = (DropDownList)item.FindControl("ddlUnit");
                    TextBox txtDiscount = item.FindControl("txtDiscount") as TextBox;
                    var ddlDiscountType = item.FindControl("ddlDiscountType") as DropDownList;

                    if (!string.IsNullOrEmpty(hddProductExportId.Value))
                    {
                        int id = Convert.ToInt32(hddProductExportId.Value);
                        IvProductExport productExport = Module.GetById<IvProductExport>(id);

                        IvProduct proCheck = Module.IvProductGetByCode(txtCodeProduct.Text.Trim(), storage);
                        if (!string.IsNullOrWhiteSpace(hddProductId.Value))
                            proCheck = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                        if (proCheck != null && proCheck.Id > 0)
                        {

                            productExport.Product = proCheck;
                            if (!string.IsNullOrWhiteSpace(txtQuantity.Text))
                                productExport.Quantity = Convert.ToInt32(txtQuantity.Text);
                            if (!string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                                productExport.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                            if (!string.IsNullOrWhiteSpace(txtQuantity.Text) && !string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                                productExport.Total = Convert.ToInt32(txtQuantity.Text) * Convert.ToDouble(txtUnitPrice.Text);
                            productExport.Export = _currentExport;
                            productExport.Storage = storage;
                            if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
                            {
                                if (ddlDiscountType.SelectedValue == "0")
                                {
                                    productExport.Total = productExport.Total - ((productExport.Total / 100) * Convert.ToInt32((txtDiscount.Text)));
                                }
                                else
                                {
                                    productExport.Total = productExport.Total - Convert.ToInt32(txtDiscount.Text);
                                }
                            }
                            sum += productExport.Total;
                            if (!string.IsNullOrWhiteSpace(ddlUnit.SelectedValue))
                            {
                                productExport.Unit = Module.GetById<IvUnit>(Convert.ToInt32(ddlUnit.SelectedValue));
                                productExport.QuanityRateParentUnit =
                                    IvProductUtil.ConvertRateParentUnit(productExport.Quantity, productExport.Unit);
                            }
                            if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
                            {
                                productExport.Discount = Convert.ToInt32(txtDiscount.Text);
                                productExport.DiscountType = Convert.ToInt32(ddlDiscountType.SelectedValue);
                            }
                            Module.SaveOrUpdate(productExport, UserIdentity);
                        }

                    }
                    else
                    {
                        IvProductExport productExport = new IvProductExport();

                        IvProduct proCheck = Module.IvProductGetByCode(txtCodeProduct.Text.Trim(), storage);
                        if (!string.IsNullOrWhiteSpace(hddProductId.Value))
                            proCheck = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                        if (proCheck != null && proCheck.Id > 0)
                        {
                            productExport.Product = proCheck;
                            if (!string.IsNullOrWhiteSpace(txtQuantity.Text))
                                productExport.Quantity = Convert.ToInt32(txtQuantity.Text);
                            if (!string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                                productExport.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                            if (!string.IsNullOrWhiteSpace(txtQuantity.Text) && !string.IsNullOrWhiteSpace(txtUnitPrice.Text))
                                productExport.Total = Convert.ToInt32(txtQuantity.Text) * Convert.ToDouble(txtUnitPrice.Text);
                            productExport.Export = _currentExport;
                            if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
                            {
                                if (ddlDiscountType.SelectedValue == "0")
                                {
                                    productExport.Total = productExport.Total - ((productExport.Total / 100) * Convert.ToInt32((txtDiscount.Text)));
                                }
                                else
                                {
                                    productExport.Total = productExport.Total - Convert.ToInt32(txtDiscount.Text);
                                }
                            }
                            sum += productExport.Total;
                            productExport.Storage = storage;
                            if (!string.IsNullOrWhiteSpace(ddlUnit.SelectedValue))
                            {
                                productExport.Unit = Module.GetById<IvUnit>(Convert.ToInt32(ddlUnit.SelectedValue));
                                productExport.QuanityRateParentUnit =
                                    IvProductUtil.ConvertRateParentUnit(productExport.Quantity, productExport.Unit);
                            }
                            if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
                            {
                                productExport.Discount = Convert.ToInt32(txtDiscount.Text);
                                productExport.DiscountType = Convert.ToInt32(ddlDiscountType.SelectedValue);
                            }
                            Module.SaveOrUpdate(productExport, UserIdentity);
                        }

                    }
                }
                _currentExport.Total = sum;
                if (_currentExport.Pay >= _currentExport.Total && _currentExport.Total > 0)
                {
                    _currentExport.Status = IvExportType.Paid;
                }
                else
                {
                    _currentExport.Status = IvExportType.Pay;
                }
                Module.SaveOrUpdate(_currentExport, UserIdentity);
                LoadInfoExport();
                BindrptProductList();

                PageRedirect(string.Format("IvExportAdd.aspx?NodeId={0}&SectionId={1}&ExportId={2}", Node.Id,
                    Section.Id, _currentExport.Id));
            }
            catch (Exception ex)
            {
                ShowErrors(ex.Message);
            }
        }

        protected void btnAddNewProduct_Click(object sender, EventArgs e)
        {
            IList data = new ArrayList();
            IvStorage storage = null;
            //            if (!string.IsNullOrEmpty(ddlStorage.SelectedValue))
            //                storage = Module.GetById<IvStorage>(Convert.ToInt32(ddlStorage.SelectedValue));
            foreach (RepeaterItem item in rptProductList.Items)
            {
                IvProductExport productExport = new IvProductExport();

                HiddenField hddProductExportId = item.FindControl("hddProductExportId") as HiddenField;
                HiddenField hddProductId = (HiddenField)item.FindControl("hddProductId");

                TextBox txtCodeProduct = item.FindControl("txtCodeProduct") as TextBox;
                TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                TextBox txtUnitPrice = item.FindControl("txtUnitPrice") as TextBox;
                TextBox txtItemTotal = item.FindControl("txtTotal") as TextBox;
                var ddlUnit = (DropDownList)item.FindControl("ddlUnit");
                TextBox txtDiscount = item.FindControl("txtDiscount") as TextBox;
                var ddlDiscountType = item.FindControl("ddlDiscountType") as DropDownList;

                if (!string.IsNullOrEmpty(hddProductExportId.Value))
                {
                    productExport.Id = Convert.ToInt32(hddProductExportId.Value);
                }
                if (!string.IsNullOrEmpty(txtCodeProduct.Text))
                {
                    productExport.Product = Module.IvProductGetByCode(txtCodeProduct.Text.Trim(), storage);
                }
                if (!string.IsNullOrEmpty(hddProductId.Value))
                {
                    productExport.Product = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                }
                if (!string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    productExport.UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                }
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    productExport.Quantity = Convert.ToInt32(txtQuantity.Text);
                }
                if (!string.IsNullOrEmpty(txtItemTotal.Text))
                {
                    productExport.Total = Convert.ToDouble(txtItemTotal.Text);
                }
                if (!string.IsNullOrEmpty(ddlUnit.SelectedValue))
                {
                    productExport.Unit = Module.GetById<IvUnit>(Convert.ToInt32(ddlUnit.SelectedValue));
                }
                if (!string.IsNullOrEmpty(txtDiscount.Text))
                {
                    productExport.Discount = Convert.ToInt32(txtDiscount.Text);
                }
                if (!string.IsNullOrEmpty(ddlDiscountType.SelectedValue))
                {
                    productExport.DiscountType = Convert.ToInt32(ddlDiscountType.SelectedValue);
                }
                productExport.Export = _currentExport;
                data.Add(productExport);
            }

            data.Add(new IvProductExport());
            rptProductList.DataSource = data;
            _total = 0;
            rptProductList.DataBind();
            rptProductListItems.Value = data.Count.ToString();
        }

        #endregion

        #region --- PRIVATE METHOD ---

        private void LoadInfoExport()
        {
            if (_currentExport.Id > 0)
            {
                txtName.Text = _currentExport.Name;
                txtCode.Text = _currentExport.Code;
                int count = Module.CountExportByDateTime(Request.QueryString, UserIdentity, DateTime.Today) + 1;

                if (string.IsNullOrWhiteSpace(_currentExport.Code))
                {
                    txtCode.Text = string.Format("PX{0}-{1}", DateTime.Today.ToString("ddMMyyyy"), count);
                }
                if (string.IsNullOrWhiteSpace(_currentExport.Name)) txtName.Text = string.Format("Phiếu xuất ngày {0}-{1}", DateTime.Today.ToString("dd/MM/yyyy"), count);

                txtExportedDate.Text = _currentExport.ExportDate.ToString("dd/MM/yyyy");
                txtTotal.Text = _currentExport.Total.ToString();
                txtPay.Text = _currentExport.Pay.ToString();
                txtDetail.Text = _currentExport.Detail;
                txtExportBy.Text = _currentExport.ExportedBy;
                if (string.IsNullOrWhiteSpace(_currentExport.ExportedBy))
                    txtExportBy.Text = UserIdentity.UserName;
                txtCustomerName.Text = _currentExport.CustomerName;
                chkIsDebt.Checked = _currentExport.IsDebt;
                txtAgency.Text = _currentExport.Agency;
                if (string.IsNullOrWhiteSpace(_currentExport.CustomerName))
                {
                    var info = "";
                    if (!string.IsNullOrEmpty(Request.QueryString["bookingRoomId"]))
                    {
                        _bookingRoom = Module.GetById<BookingRoom>(Convert.ToInt32(Request.QueryString["bookingRoomId"]));
                    }
                    if (_currentExport.BookingRoom != null)
                    {
                        _bookingRoom = _currentExport.BookingRoom;
                        foreach (Customer customer in _bookingRoom.Customers)
                        {
                            if (!string.IsNullOrWhiteSpace(customer.Fullname))
                            {
                                info += customer.Fullname + "; ";
                            }
                        }
                    }

                    txtCustomerName.Text = info;
                }
                //                if (_currentExport.Storage != null) ddlStorage.SelectedValue = _currentExport.Storage.Id.ToString();
            }
        }
        private void BindrptProductList()
        {
            int count;
            var list = Module.IvProductExportGetByExport(_currentExport, Request.QueryString, 0, 0,
                out count);
            _total = 0;
            rptProductList.DataSource = list;
            rptProductList.DataBind();
            rptProductListItems.Value = list.Count.ToString();

        }

        #endregion

        #region Implementation of ICallbackEventHandler

        private string _callbackResult;

        public void RaiseCallbackEvent(string eventArgument)
        {
            if (!string.IsNullOrEmpty(eventArgument))
            {
                var str = eventArgument.Split('|');
                //IvStorage storage = null;
                //if (!string.IsNullOrEmpty(str[1]))
                //{
                //    storage = Module.GetById<IvStorage>(Convert.ToInt32(str[1]));
                //}
                var storage = "";
                if (str.Length > 2 && !string.IsNullOrEmpty(str[1]))
                {
                    storage = str[1];
                }
                IvInStock product = Module.GetProductInStockByCode(str[0], storage);
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
            Response.Redirect(string.Format("IvProductExportList.aspx?NodeId={0}&SectionId={1}", Node.Id,
                Section.Id));
        }
        public void ShowErrors(string error)
        {
            Session["ErrorMessage"] = "<strong>Error!</strong> " + error + "<br/>" + Session["ErrorMessage"];
        }
        protected void btnExportExcel_OnClick(object sender, EventArgs e)
        {
            ExcelFile excelFile = ExcelFile.Load(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/phieu_xuat.xlsx"));
            ExcelWorksheet sheet = excelFile.Worksheets[0];


            var index = 1;
            sheet.Cells["B3"].Value = txtName.Text;
            sheet.Cells["B4"].Value = txtExportedDate.Text;
            sheet.Cells["B5"].Value = txtCustomerName.Text;
            sheet.Cells["D3"].Value = txtCode.Text;
            sheet.Cells["D4"].Value = txtExportBy.Text;
            //            sheet.Cells["D5"].Value = ddlStorage.SelectedItem.Text;

            const int firstrow = 7;
            int crow = firstrow;
            sheet.Rows.InsertCopy(crow, rptProductList.Items.Count, sheet.Rows[firstrow]);
            var total = 0.0; var discount = 0.0;
            foreach (RepeaterItem item in rptProductList.Items)
            {
                try
                {
                    TextBox txtCodeProduct = (TextBox)item.FindControl("txtCodeProduct");
                    HiddenField hddProductId = (HiddenField)item.FindControl("hddProductId");
                    HiddenField hddProductExportId = (HiddenField)item.FindControl("hddProductExportId");
                    TextBox txtQuantity = (TextBox)item.FindControl("txtQuantity");
                    TextBox txtUnitPrice = (TextBox)item.FindControl("txtUnitPrice");
                    var ddlUnit = (DropDownList)item.FindControl("ddlUnit");
                    var txtTotal = (TextBox)item.FindControl("txtTotal");
                    TextBox txtDiscount = item.FindControl("txtDiscount") as TextBox;
                    var ddlDiscountType = item.FindControl("ddlDiscountType") as DropDownList;
                    sheet.Cells[crow, 0].Value = txtCodeProduct.Text;
                    if (!string.IsNullOrWhiteSpace(hddProductId.Value))
                    {
                        var product = Module.GetById<IvProduct>(Convert.ToInt32(hddProductId.Value));
                        sheet.Cells[crow, 1].Value = product.Name;
                    }
                    sheet.Cells[crow, 2].Value = txtQuantity.Text;
                    sheet.Cells[crow, 3].Value = ddlUnit.SelectedItem.Text.Replace(".", "").Replace("|", "").Replace("_", "");
                    sheet.Cells[crow, 4].Value = txtUnitPrice.Text;

                    if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
                    {
                        sheet.Cells[crow, 5].Value = string.Format("{0} {1}", txtDiscount.Text, ddlDiscountType.SelectedValue);
                    }
                    sheet.Cells[crow, 6].Value = txtTotal.Text;
                    if (hddProductExportId != null && !string.IsNullOrWhiteSpace(hddProductExportId.Value))
                    {
                        var ep = Module.GetById<IvProductExport>(Convert.ToInt32(hddProductExportId.Value));
                        if (ep != null && ep.Storage != null)
                        {
                            sheet.Cells[crow, 7].Value = ep.Storage.Name;
                        }
                    }
                    total += (Convert.ToInt32(txtQuantity.Text) * Convert.ToDouble(txtUnitPrice.Text));
                }
                catch (Exception ex)
                {
                    total += 0;
                }
                crow++;
            }
            sheet.Cells[crow, 4].Value = "Thành tiền";
            sheet.Cells[crow, 5].Value = total.ToString("#,##0.##");
            crow++;
            sheet.Cells[crow, 4].Value = "Tổng";
            sheet.Cells[crow, 5].Value = total.ToString("#,##0.##");
            excelFile.Save(Response, string.Format("phieu_xuat{0}.xlsx", txtExportedDate.Text));
        }
    }
}