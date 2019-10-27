using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Aspose.Words.Tables;
using CMS.Web.Util;
using GemBox.Spreadsheet;
using iTextSharp.text.pdf;
using log4net;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.ReportEngine;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;
using TextBox = System.Web.UI.WebControls.TextBox;
using System.Linq;
using Portal.Modules.OrientalSails.BusinessLogic;
using System.Xml;
using OfficeOpenXml;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Web.Admin.Utility;
using Portal.Modules.OrientalSails.Enums;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BookingReport : SailsAdminBase
    {
        private double _customerCost;
        private IList _dailyCost;
        protected DateTime _date;
        private IList _guides;
        private double _runningCost;
        private IList _services;
        private IList _suppliers;
        private double _totalCost;
        private double _total;
        private IList<SailsTrip> trips;
        private IList<Booking> bookings;
        private readonly Dictionary<int, int> _customers = new Dictionary<int, int>();
        private SailsTrip trip;
        private BookingReportBLL bookingReportBLL;
        public BookingReportBLL BookingReportBLL
        {
            get
            {
                if (bookingReportBLL == null)
                    bookingReportBLL = new BookingReportBLL();
                return bookingReportBLL;
            }
        }
        private UserBLL userBLL;
        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                    userBLL = new UserBLL();
                return userBLL;
            }
        }
        private PermissionBLL permissionBLL;
        public PermissionBLL PermissionBLL
        {
            get
            {
                if (permissionBLL == null)
                    permissionBLL = new PermissionBLL();
                return permissionBLL;
            }
        }
        protected IList Suppliers
        {
            get
            {
                if (_suppliers == null)
                {
                    _suppliers = Module.SupplierGetAll();
                }
                return _suppliers;
            }
        }
        protected IList Guides
        {
            get
            {
                if (_guides == null)
                {
                    _guides = Module.GuidesGetAll();
                }
                return _guides;
            }
        }
        protected IList DailyCost
        {
            get
            {
                if (_dailyCost == null)
                {
                    _dailyCost = Module.CostTypeGetDailyInput();
                }
                return _dailyCost;
            }
        }
        /// <summary>
        /// Lấy tàu theo query string
        /// </summary>
        public Cruise Cruise
        {
            get
            {
                var cruiseId = -1;
                try
                {
                    cruiseId = Int32.Parse(Request.QueryString["cruiseid"]);
                }
                catch { }
                var cruise = BookingReportBLL.CruiseGetById(cruiseId);
                return cruise;
            }
        }
        /// <summary>
        /// Lấy ngày theo query string
        /// </summary>
        public DateTime Date
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["Date"]))
                {
                    return DateTime.Today;
                }
                return DateTime.ParseExact(Request.QueryString["Date"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
        public User CurrentUser
        {
            get
            {
                return UserBLL.UserGetCurrent();
            }
        }
        public LockingExpense LockingExpense
        {
            get
            {
                return BookingReportBLL.LockingExpenseGetAllByCriterion(Date).Future().ToList().FirstOrDefault(); ;
            }
        }
        public string LockingExpenseString
        {
            get
            {
                if (LockingExpense != null)
                {
                    return "true";
                }
                return "false";
            }

        }
        public bool LockingExpenseBoolean
        {
            get
            {
                if (LockingExpense != null)
                {
                    return true;
                }
                return false;
            }

        }
        /// <summary>
        /// Lấy danh sách các booking approved và pending theo ngày và tàu 
        /// </summary>
        public IEnumerable<Booking> ListBooking
        {
            get
            {
                return BookingReportBLL
                    .BookingGetAllByCriterion(UserIdentity, Date, Cruise, new List<StatusType>() { StatusType.Approved, StatusType.Pending }); // Tìm các booking theo ngày và theo tàu, nếu là all thì tìm theo tất cả các tàu
            }
        }
        /// <summary>
        /// Lấy danh sách các tàu
        /// </summary>
        public IEnumerable<Cruise> ListCruise
        {
            get
            {
                //return BookingReportBLL.CruiseGetAll();
                return Module.CruiseGetByUser(UserIdentity);
            }
        }
        /// <summary>
        /// Kiểm tra tài khoản có được phép xem cột total trong bảng không
        /// </summary>
        public bool CanViewTotal
        {
            get
            {
                return PermissionBLL.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.VIEW_TOTAL_BY_DATE);
            }
        }
        public void Initialize()
        {
            //Khởi tạo danh sách trip
            trips = Module.TripGetAll(false).Cast<SailsTrip>().ToList();
            //Khởi tạo trip
            var tripId = 0;
            try
            {
                tripId = Int32.Parse(Request.QueryString["tripid"]);
            }
            catch { }
            trip = Module.TripGetById(tripId);
            //Khởi tạo danh sách booking
            ICriterion criterion = Expression.Eq("Deleted", false);
            criterion = Expression.And(criterion, Expression.Eq("Status", StatusType.Approved));
            criterion = Module.AddDateExpression(criterion, Date);
            bookings = Module.BookingGetByCriterion(criterion, null, 0, 0).Cast<Booking>().ToList();
            //Khởi tạo dictionary số khách theo từng trip
            foreach (Booking booking in bookings)
            {
                if (!_customers.ContainsKey(booking.Trip.Id))
                {
                    _customers.Add(booking.Trip.Id, 0);
                }
                _customers[booking.Trip.Id] += booking.Pax;
            }
            //Khởi tạo danh sách booking nếu có trip được chọn
            if (Request.QueryString["tripid"] != null)
            {
                criterion = Expression.And(criterion, Expression.Eq("Trip", trip));
            }
            bookings = Module.BookingGetByCriterion(criterion, null, 0, 0).Cast<Booking>().ToList();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Redirect();
            Initialize();
            this.Master.Title = "Booking by date";
            Page.DataBind();
            if (!IsPostBack)
            {
                txtDate.Text = Date.ToString("dd/MM/yyyy");
                rptBookingList.DataSource = bookings.OrderBy(x => x.Trip.Id).ToList();
                rptBookingList.DataBind();
                rptTrips.DataSource = trips;
                rptTrips.DataBind();
                _services = new ArrayList();
                foreach (CostType type in DailyCost)
                {
                    if (type.IsSupplier)
                    {
                        _services.Add(type);
                    }
                }

                var listShadowBooking = BookingReportBLL.ShadowBookingGetByDate(Date);
                var listShadowBookingNeeding = new List<Booking>();
                foreach (Booking booking in listShadowBooking)
                {
                    var histories = BookingReportBLL.BookingHistoryGetAllByBooking(booking).OrderBy(x => x.Date).AsEnumerable();
                    for (int ii = histories.Count() - 1; ii >= 0; ii--)
                    {
                        if (((BookingHistory)(histories.ElementAt(ii))).StartDate == Date
                            || ((BookingHistory)(histories.ElementAt(ii))).Status == StatusType.Cancelled)
                        {
                            var bh = (BookingHistory)(histories.ElementAt(ii));
                            if (booking.BookingRooms.Count >= 6)
                            {
                                if (bh.Date > Date.AddDays(-45))
                                {
                                    listShadowBookingNeeding.Add(booking);
                                }
                            }
                            else
                            {
                                if (bh.Date > Date.AddDays(-7))
                                {
                                    listShadowBookingNeeding.Add(booking);
                                }
                            }
                            break;
                        }
                    }
                }
                rptShadows.DataSource = listShadowBookingNeeding;
                rptShadows.DataBind();
            }
        }

        public void Redirect()
        {
            var dateQuery = "";
            if (String.IsNullOrEmpty(Request.QueryString["Date"]))
            {
                dateQuery = "&Date=" + Date.ToString("dd/MM/yyyy"); ;
            }
            if (Date < new DateTime(2018, 12, 7))
            {
                Response.Redirect("Archive/BookingReport.aspx" + Request.Url.Query + dateQuery);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (bookingReportBLL != null)
            {
                bookingReportBLL.Dispose();
                bookingReportBLL = null;
            }
            if (userBLL != null)
            {
                userBLL.Dispose();
                userBLL = null;
            }
            if (permissionBLL != null)
            {
                permissionBLL.Dispose();
                permissionBLL = null;
            }
        }

        public void ShowWarning(string warning)
        {
            Session["WarningMessage"] = "<strong>Warning!</strong> " + warning + "<br/>" + Session["WarningMessage"];
        }

        public void ShowErrors(string error)
        {
            Session["ErrorMessage"] = "<strong>Error!</strong> " + error + "<br/>" + Session["ErrorMessage"];
        }

        public void ShowSuccess(string success)
        {
            Session["SuccessMessage"] = "<strong>Success!</strong> " + success + "<br/>" + Session["SuccessMessage"];
        }
        public void btnSaveExpenses_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rptItem in rptCruiseExpense.Items)
            {
                var hiddenId = (HiddenField)rptItem.FindControl("hiddenId");
                Cruise cruise = Module.CruiseGetById(Convert.ToInt32(hiddenId.Value));
                Expense expense = Module.ExpenseGetByDate(cruise, Date);

                if (expense.Id <= 0)
                {
                    Module.SaveOrUpdate(expense);
                }

                var rptServices = (Repeater)rptItem.FindControl("rptServices");
                IList<ExpenseService> expenseList = rptServicesToIList(rptServices);
                foreach (ExpenseService service in expenseList)
                {
                    service.Expense = expense;
                    if (service.IsRemoved)
                    {
                        if (service.Id > 0)
                        {
                            expense.Services.Remove(service);
                        }
                    }
                    else
                    {
                        Module.SaveOrUpdate(service);
                    }
                }
            }
        }
        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            string url = string.Format("BookingReport.aspx?NodeId={0}&SectionId={1}&Date={2}", Node.Id, Section.Id,
                                       txtDate.Text);
            PageRedirect(url);
        }
        protected void rptServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is ExpenseService)
            {
                ExpenseService service = (ExpenseService)e.Item.DataItem;
                HiddenField hiddenId = (HiddenField)e.Item.FindControl("hiddenId");
                HiddenField hiddenType = (HiddenField)e.Item.FindControl("hiddenType");
                TextBox txtName = (TextBox)e.Item.FindControl("txtName");
                TextBox txtPhone = (TextBox)e.Item.FindControl("txtPhone");
                DropDownList ddlSuppliers = (DropDownList)e.Item.FindControl("ddlSuppliers");
                DropDownList ddlGuides = (DropDownList)e.Item.FindControl("ddlGuides");
                TextBox txtCost = (TextBox)e.Item.FindControl("txtCost");
                Literal litType = (Literal)e.Item.FindControl("litType");

                hiddenId.Value = service.Id.ToString();
                hiddenType.Value = (service.Type.Id).ToString();

                txtName.Text = service.Name;
                txtPhone.Text = service.Phone;
                txtCost.Text = service.Cost.ToString("#,0.#");
                txtCost.Attributes.Add("onchange", "this.value = addCommas(this.value);");
                ddlGuides.Visible = false;

                if (service.Type.Id == SailsModule.GUIDE_COST)
                {
                    litType.Text = "Guide";
                    ddlGuides.DataSource = Guides;
                    ddlGuides.DataTextField = "Name";
                    ddlGuides.DataValueField = "Id";
                    ddlGuides.DataBind();
                    txtName.Visible = false;
                    ddlGuides.Visible = true;
                    ddlSuppliers.Visible = false;
                    if (string.IsNullOrEmpty(txtPhone.Text))
                    {
                        txtPhone.Text = "AUTO FROM DATABASE";
                    }
                    txtPhone.Enabled = false;
                }
                else
                {
                    litType.Text = service.Type.Name;
                    if (service.Type.IsSupplier)
                    {
                        ddlSuppliers.DataSource = Suppliers;
                    }
                    else
                    {
                        ddlSuppliers.Visible = false;
                        e.Item.FindControl("txtCost").Visible = false;
                    }
                }

                if (ddlSuppliers.Visible)
                {
                    ddlSuppliers.DataTextField = "Name";
                    ddlSuppliers.DataValueField = "Id";
                    ddlSuppliers.DataBind();
                }

                if (service.Type.Id == SailsModule.GUIDE_COST)
                {
                    if (service.Supplier != null)
                    {
                        ddlGuides.SelectedValue = service.Supplier.Id.ToString();
                        txtPhone.Text = service.Supplier.Phone;
                    }
                }
                else
                {
                    if (service.Type.IsSupplier)
                    {
                        if (service.Supplier != null)
                        {
                            ddlSuppliers.SelectedValue = service.Supplier.Id.ToString();
                        }
                    }
                }

                if (service.IsRemoved)
                {
                    e.Item.Visible = false;
                }
                else if (service.Type.IsSupplier)
                {
                    _totalCost += service.Cost;
                }
            }
        }
        protected int GetData(out IList list, bool loadService)
        {
            Cruise cruise = null;

            if (Request.QueryString["code"] != null)
            {
                ICriterion crit = Expression.Eq("Deleted", false);
                crit = SailsModule.AddBookingCodeCriterion(crit, Request.QueryString["code"]);

                var temp = Module.GetObject<Booking>(crit, 2, 0);
                if (temp.Count > 1)
                {
                    ShowErrors("Please input booking code correctly");
                    list = new ArrayList();
                    return 0;
                }
                else if (temp.Count == 0)
                {
                    ShowErrors("No booking with the code you provided");
                    list = new ArrayList();
                    return 0;
                }
                else
                {
                    cruise = temp[0].Cruise;
                    _date = temp[0].StartDate;
                }
            }

            if (cruise == null)
            {
                if (Request.QueryString["cruiseid"] != null)
                {
                    cruise = Module.CruiseGetById(Convert.ToInt32(Request.QueryString["cruiseid"]));
                }

                if (string.IsNullOrEmpty(txtDate.Text))
                {
                    _date = DateTime.Today;
                }
                else
                {
                    _date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }

            ICriterion criterion = Module.LockCrit();
            criterion = Expression.And(criterion, Expression.Not(Expression.Eq("IsTransferred", true)));

            if (cruise != null)
            {
                criterion = Expression.And(criterion, Expression.Eq("Cruise", cruise));
            }

            criterion = Module.AddDateExpression(criterion, _date);

            int count;
            list = Module.BookingGetByCriterion(criterion, null, out count, 0, 0);
            List<Booking> bookings = new List<Booking>();

            foreach (Booking booking in list)
            {
                bookings.Add(booking);
            }

            list = bookings.OrderBy(o => o.Trip.Id).ToList<Booking>();

            if (loadService)
            {
                LoadService(_date);
            }
            return count;
        }

        private void LoadService(DateTime date)
        {
            var listCruise = ListCruise;
            IList<Cruise> cruises;
            if (Cruise == null)
            {
                cruises = listCruise.ToList();
            }
            else
            {
                cruises = new List<Cruise>();
                cruises.Add(Cruise);
            }

            _date = date;
            if (Cruise != null)
            {
                Expense expense = Module.ExpenseGetByDate(Cruise, date);
                if (expense.Id <= 0)
                {
                    Module.SaveOrUpdate(expense);
                }

                ExpenseCalculator calculator = new ExpenseCalculator(Module, PartnershipManager);

                _customerCost = 0;
                _runningCost = 0;
                Dictionary<CostType, double> cost = calculator.ExpenseCalculate(null, expense);
                foreach (KeyValuePair<CostType, double> pair in cost)
                {
                    if (pair.Key.IsSupplier && !pair.Key.IsDailyInput && !pair.Key.IsDaily && !pair.Key.IsMonthly &&
                        !pair.Key.IsYearly)
                    {
                        _customerCost += pair.Value;
                    }
                    else if (pair.Key.IsSupplier && !pair.Key.IsDailyInput && pair.Key.IsDaily)
                    {
                        _runningCost += pair.Value;
                    }
                }
            }

            if (DailyCost.Count > 0)
            {
                rptCruiseExpense.DataSource = cruises;
                rptCruiseExpense.DataBind();
            }
            else
            {
                plhDailyExpenses.Visible = false;
                rptCruiseExpense.Visible = false;
            }
        }

        private void LoadService(Repeater rptServices, Cruise cruise, DateTime date)
        {
            Expense expense = Module.ExpenseGetByDate(cruise, date);

            Dictionary<CostType, bool> serviceMap = new Dictionary<CostType, bool>();

            foreach (CostType type in DailyCost)
            {
                serviceMap.Add(type, false);
            }

            IList services = new ArrayList();
            foreach (ExpenseService service in expense.Services)
            {
                try
                {
                    if (service.Type.IsDailyInput && !service.Type.IsMonthly && !service.Type.IsYearly)
                    {
                        serviceMap[service.Type] = true;
                        services.Add(service);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            foreach (CostType type in DailyCost)
            {
                if (!serviceMap[type])
                {
                    ExpenseService service = new ExpenseService();
                    service.Type = type;
                    service.IsOwnService = !type.IsSupplier;
                    services.Add(service);
                }
            }

            var tempServices = new List<ExpenseService>();
            ExpenseService firstOperator = null;
            foreach (ExpenseService service in services)
            {
                if (service.Type.Name == "Operator")
                {
                    if (firstOperator != null)
                    {
                        var tempExpense = service.Expense;
                        tempExpense.Services.Remove(service);
                        Module.SaveOrUpdate(tempExpense);
                        continue;
                    }
                    else
                    {
                        firstOperator = service;
                    }
                }
                tempServices.Add(service);
            }
            rptServices.DataSource = tempServices;
            rptServices.DataBind();
        }

        protected IList<ExpenseService> rptServicesToIList(Repeater rptServices)
        {
            IList<ExpenseService> list = new List<ExpenseService>();
            foreach (RepeaterItem item in rptServices.Items)
            {
                HiddenField hiddenId = (HiddenField)item.FindControl("hiddenId");
                HiddenField hiddenType = (HiddenField)item.FindControl("hiddenType");
                TextBox txtName = (TextBox)item.FindControl("txtName");
                TextBox txtPhone = (TextBox)item.FindControl("txtPhone");
                DropDownList ddlSuppliers = (DropDownList)item.FindControl("ddlSuppliers");
                DropDownList ddlGuides = (DropDownList)item.FindControl("ddlGuides");
                TextBox txtCost = (TextBox)item.FindControl("txtCost");

                int serviceId = Convert.ToInt32(hiddenId.Value);

                ExpenseService service;
                if (serviceId <= 0)
                {
                    service = new ExpenseService();
                }
                else
                {
                    service = Module.ExpenseServiceGetById(serviceId);
                }
                service.Type = Module.CostTypeGetById(Convert.ToInt32(hiddenType.Value));
                service.Name = txtName.Text;
                service.Phone = txtPhone.Text;
                if (service.Type.Id == SailsModule.GUIDE_COST)
                {
                    service.Supplier = Module.AgencyGetById(Convert.ToInt32(ddlGuides.SelectedValue));
                }
                else if (service.Type.IsSupplier)
                {
                    if (ddlSuppliers.SelectedIndex >= 0)
                    {
                        service.Supplier = Module.AgencyGetById(Convert.ToInt32(ddlSuppliers.SelectedValue));
                    }
                    else
                    {
                        service.Supplier = null;
                    }
                }
                service.IsOwnService = !service.Type.IsSupplier;
                service.Cost = Convert.ToDouble(txtCost.Text);
                service.IsRemoved = !item.Visible;
                list.Add(service);
            }
            return list;
        }

        protected void rptCruises_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Cruise)
            {
                var cruise = (Cruise)e.Item.DataItem;
                HyperLink hplCruises = e.Item.FindControl("hplCruises") as HyperLink;
                hplCruises.CssClass = "btn btn-default";
                if (hplCruises != null)
                {
                    if (cruise.Id.ToString() == Request.QueryString["cruiseid"])
                    {
                        hplCruises.CssClass = "btn btn-default active";
                    }

                    DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var numberOfRoom = BookingReportBLL.BookingRoomGetRowCountByCriterion(cruise, Date);
                    var numberOfPax = BookingReportBLL.CustomerGetRowCountByCriterion(cruise, Date);
                    hplCruises.Text = string.Format("{0} ({1} pax/{2} cabins)", cruise.Name, numberOfPax.ToString(), numberOfRoom.ToString());
                    hplCruises.Attributes.Add("data-pax", numberOfPax.ToString());
                    hplCruises.NavigateUrl = string.Format(
                        "BookingReport.aspx?NodeId={0}&SectionId={1}&Date={2}&cruiseid={3}", Node.Id, Section.Id,
                        date.ToString("dd/MM/yyyy"), cruise.Id);
                }
            }
            else
            {
                HyperLink hplCruises = e.Item.FindControl("hplCruises") as HyperLink;
                if (hplCruises != null)
                {
                    if (Request.QueryString["cruiseid"] == null)
                    {
                        hplCruises.CssClass = "btn btn-default active";
                    }
                    DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    hplCruises.NavigateUrl = string.Format(
                        "BookingReport.aspx?NodeId={0}&SectionId={1}&Date={2}", Node.Id, Section.Id, date.ToString("dd/MM/yyyy"));
                }
            }
        }

        protected void rptCruiseExpense_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Cruise)
            {
                Cruise cruise = (Cruise)e.Item.DataItem;
                PlaceHolder plhCruiseExpense = e.Item.FindControl("plhCruiseExpense") as PlaceHolder;
                if (cruise.Name == "Transfer" || cruise.Name == "OS Scorpio" || cruise.Name == "Daily Charter")
                {
                    plhCruiseExpense.Visible = false;
                }
                Repeater rptServices = e.Item.FindControl("rptServices") as Repeater;
                LoadService(rptServices, cruise, _date);
            }
            else
            {
                Literal litSTotal = e.Item.FindControl("litTotal") as Literal;
                if (litSTotal != null)
                {
                    litSTotal.Text = _totalCost.ToString("#,###.##");
                }
            }
        }
        protected void btnViewFeedback_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string url = string.Format("FeedbackReport.aspx?NodeId={0}&SectionId={1}&from={2}&to={2}", Node.Id, Section.Id,
                                       date.ToOADate());
            PageRedirect(url);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ShowSuccess("Saved successfully");
            btnSaveExpenses_Click(null, null);
            Session["Redirect"] = true;
            var script = "angular.element(\"[ng-controller='expenseController']\").scope().save();";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "initBtnSaveClicked", script, true);
        }
        protected void btnLockDate_Click(object sender, EventArgs e)
        {
            var lockingExpense = LockingExpense;
            if (lockingExpense == null)
            {
                lockingExpense = new LockingExpense();
            }
            lockingExpense.Date = Date;
            BookingReportBLL.LockingExpenseSaveOrUpdate(lockingExpense);
            ShowSuccess("Locked date successfully");
            Response.Redirect(Request.RawUrl);
        }
        protected void btnUnlockDate_Click(object sender, EventArgs e)
        {
            var lockingExpense = LockingExpense;
            if (lockingExpense == null)
            {
                Response.Redirect(Request.RawUrl);
            }
            BookingReportBLL.LockingExpenseDelete(lockingExpense);
            ShowSuccess("Unlocked date successfully");
            Response.Redirect(Request.RawUrl);
        }
        protected void btnExportCustomerData_Click(object sender, EventArgs e)
        {
            var listCustomer = ListBooking.SelectMany(x => x.BookingRooms.SelectMany(y => y.RealCustomers));// Lấy danh sách khách hàng từ danh sách booking hiện tại
            MemoryStream mem = new MemoryStream();
            using (var excelPackage = new ExcelPackage(new FileInfo(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/ClientDetails.xlsx"))))
            {
                var sheet = excelPackage.Workbook.Worksheets["Client Details"];
                sheet.Cells["G2"].Value = "Start date:" + " " + Date.ToString("dd-MMM");
                sheet.Cells["H2"].Value = Cruise.Name;
                int startRow = 5;
                int currentRow = startRow;
                int templateRow = startRow;
                currentRow++;
                sheet.InsertRow(currentRow, listCustomer.Count() - 1, templateRow);
                currentRow--;
                for (int i = 0; i < listCustomer.Count(); i++)
                {
                    var customer = listCustomer.ElementAt(i) as Customer;
                    sheet.Cells[currentRow, 1].Value = i + 1;
                    sheet.Cells[currentRow, 2].Value = customer.Fullname.ToUpper();
                    sheet.Cells[currentRow, 4].Value = customer.Birthday.HasValue
                        ? customer.Birthday.Value.ToString("dd/MM/yyyy") : "";
                    sheet.Cells[currentRow, 3].Value = StringUtil.GetFirstLetter(customer.Gender);
                    if (customer.Nationality != null)
                        if (customer.Nationality.Name.ToLower() != "Khong ro")
                            sheet.Cells[currentRow, 6].Value = customer.Nationality.Name;
                        else
                            sheet.Cells[currentRow, 6].Value = customer.Passport;
                    sheet.Cells[currentRow, 5].Value = customer.Passport;
                    if (customer.Booking != null && customer.Booking.Cruise != null && customer.Booking.Trip != null)
                    {
                        sheet.Cells[currentRow, 7].Value = customer.Booking.Cruise.GetModifiedCruiseName() + " " + customer.Booking.Trip.NumberOfDay + "D";
                    }
                    currentRow++;
                }
                excelPackage.SaveAs(mem);
            }
            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "";
            if (Cruise != null)
            {
                fileName = string.Format("\"Client details - {0} - {1}.xlsx\"", Date.ToString("dd_MM_yyyy"), Cruise.Name.Replace(" ", "_"));
            }
            Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            mem.Position = 0;
            byte[] buffer = mem.ToArray();
            Response.BinaryWrite(buffer);
            Response.End();
        }
        protected void btnProvisionalRegister_Click(object sender, EventArgs e)
        {
            if (Cruise.Name.Contains("Calypso"))
            {
                ProvisionalRegisterCalypsoCruise();
            }
            else
            {
                ProvisionalRegisterOtherCruise();
            }
        }
        private void ProvisionalRegisterCalypsoCruise()
        {
            var bookings = BookingReportBLL.BookingGetAllStartInDate(Date, Cruise);
            var customers = bookings.SelectMany(x => x.BookingRooms.SelectMany(y => y.Customers));
            MemoryStream mem = new MemoryStream();
            using (var excelPackage = new ExcelPackage(new FileInfo(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/ProvisionalRegisterTemplate_Calypso.xlsx"))))
            {
                var sheet = excelPackage.Workbook.Worksheets["ProvisionalRegister"];
                sheet.Name = "PR-Calypso-" + Date.ToString("dd_MM_yyyy");
                int startRow = 2;
                int currentRow = startRow;
                int templateRow = startRow;
                currentRow++;
                sheet.InsertRow(currentRow, customers.Count() - 1, templateRow);
                currentRow--;
                for (int i = 0; i < customers.Count(); i++)
                {
                    var customer = customers.ElementAt(i) as Customer;
                    sheet.Cells[currentRow, 1].Value = i + 1;
                    sheet.Cells[currentRow, 2].Value = customer.Fullname.ToUpper();
                    sheet.Cells[currentRow, 3].Value = customer.Birthday.HasValue
                        ? customer.Birthday.Value.Year.ToString() : "";
                    sheet.Cells[currentRow, 4].Value = customer.IsMale.HasValue ? (customer.IsMale.Value ? "Nam" : "Nữ") : "";
                    sheet.Cells[currentRow, 5].Value = customer.Passport;
                    if (customer.Nationality != null)
                        if (customer.Nationality.Name.ToLower() != "Khong ro")
                            sheet.Cells[currentRow, 6].Value = customer.Nationality.NaNameViet;
                        else
                            sheet.Cells[currentRow, 6].Value = "";
                    currentRow++;
                }
                excelPackage.SaveAs(mem);
            }
            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "";
            if (Cruise != null)
            {
                fileName = string.Format("\"ProvisionalRegister - {0} - {1}.xlsx\"", Date.ToString("dd_MM_yyyy"), Cruise.Name.Replace(" ", "_"));
            }
            Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            mem.Position = 0;
            byte[] buffer = mem.ToArray();
            Response.BinaryWrite(buffer);
            Response.End();
        }
        private void ProvisionalRegisterOtherCruise()
        {
            DateTime startDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            int cruiseId = -1;
            try
            {
                cruiseId = Convert.ToInt32(Request.QueryString["cruiseid"]);
            }
            catch (Exception) { }
            var bookingStatus = (int)StatusType.Approved;
            var bookings = BookingReportBLL.BookingReportBLL_BookingSearchBy(startDate, cruiseId, bookingStatus);
            var bookings2Days = new List<Booking>();
            var bookings3Days = new List<Booking>();
            foreach (var booking in bookings)
            {
                if (booking.Trip.NumberOfDay == 2)
                    bookings2Days.Add(booking);

                if (booking.Trip.NumberOfDay == 3)
                    bookings3Days.Add(booking);
            }
            var VietNamCustomerOfBookings2Days = new List<Customer>();
            var ForeignCustomerOfBookings2Days = new List<Customer>();
            var VietNamCustomerOfBookings3Days = new List<Customer>();
            var ForeignCustomerOfBookings3Days = new List<Customer>();
            ProvisalRegisterSortCustomer(bookings2Days, ref VietNamCustomerOfBookings2Days, ref ForeignCustomerOfBookings2Days);
            ProvisalRegisterSortCustomer(bookings3Days, ref VietNamCustomerOfBookings3Days, ref ForeignCustomerOfBookings3Days);
            var excelFile = new ExcelFile();
            excelFile.LoadXls(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/DangKyTamTruTemplate.xls"));
            var sheetVietNam2Days = excelFile.Worksheets[0];
            var sheetVietNam3Days = excelFile.Worksheets[1];
            var sheetNuocNgoai2Days = excelFile.Worksheets[2];
            var sheetNuocNgoai3Days = excelFile.Worksheets[3];
            var stt = 1;
            ExportFillProvisalRegister(VietNamCustomerOfBookings2Days, sheetVietNam2Days, ref stt);
            ExportFillProvisalRegister(VietNamCustomerOfBookings3Days, sheetVietNam3Days, ref stt);
            ExportFillProvisalRegister(ForeignCustomerOfBookings2Days, sheetNuocNgoai2Days, ref stt);
            ExportFillProvisalRegister(ForeignCustomerOfBookings3Days, sheetNuocNgoai3Days, ref stt);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            var cruise = bookingReportBLL.CruiseGetById(cruiseId);
            if (cruise != null)
                Response.AppendHeader("content-disposition", "attachment; filename=" + string.Format("Form_tam_tru_{0}_{1}.xls", startDate.ToString("dd/MM/yyyy"), cruise.GetModifiedCruiseName().Replace(" ", "_")));
            if (cruise == null)
            {
                return;
            }
            MemoryStream m = new MemoryStream();
            excelFile.SaveXls(m);
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            m.Close();
            Response.End();
        }
        public void ProvisalRegisterSortCustomer(IList<Booking> bookings, ref List<Customer> vietNamCustomers, ref List<Customer> foreignCustomer)
        {
            foreach (var booking in bookings)
            {
                foreach (var bookingRoom in booking.BookingRooms)
                {
                    foreach (var customer in bookingRoom.Customers)
                    {
                        if (customer.Nationality == null)
                        {
                            foreignCustomer.Add(customer);
                            continue;
                        }

                        if (customer.Nationality.Name == "VIET NAM")
                            vietNamCustomers.Add(customer);
                        else
                            foreignCustomer.Add(customer);
                    }
                }
            }
        }
        public void ExportFillProvisalRegister(IList<Customer> customers, GemBox.Spreadsheet.ExcelWorksheet sheet, ref int stt)
        {
            var activeRow = 1;
            foreach (var customer in customers)
            {
                sheet.Cells[activeRow, 0].Value = stt.ToString();
                stt++;
                customer.Fullname = customer.Fullname ?? "";
                sheet.Cells[activeRow, 1].Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(customer.Fullname.ToLower());

                var birthday = "";
                try
                {
                    birthday = customer.Birthday.Value.ToString("dd/MM/yyyy");
                }
                catch (Exception) { }
                sheet.Cells[activeRow, 2].Value = birthday;

                sheet.Cells[activeRow, 3].Value = "D";

                var isMale = false;
                try
                {
                    isMale = customer.IsMale.Value;
                }
                catch (Exception) { }

                if (isMale)
                    sheet.Cells[activeRow, 4].Value = "M";
                else
                    sheet.Cells[activeRow, 4].Value = "F";

                var maquoctich = "";
                try
                {
                    maquoctich = customer.Nationality.AbbreviationCode;
                }
                catch (Exception) { }
                sheet.Cells[activeRow, 5].Value = maquoctich;
                sheet.Cells[activeRow, 6].Value = customer.Passport;

                sheet.Cells[activeRow, 7].Value = ((BookingRoom)customer.BookingRooms[0]).Room != null ? ((BookingRoom)customer.BookingRooms[0]).Room.Name : "";
                sheet.Cells[activeRow, 8].Value = ((BookingRoom)customer.BookingRooms[0]).Book.StartDate.ToString("dd/MM/yyyy");
                sheet.Cells[activeRow, 9].Value = ((BookingRoom)customer.BookingRooms[0]).Book.EndDate.ToString("dd/MM/yyyy");
                sheet.Cells[activeRow, 10].Value = ((BookingRoom)customer.BookingRooms[0]).Book.EndDate.ToString("dd/MM/yyyy");
                sheet.Cells[activeRow, 10].Value = customer.NguyenQuan;
                activeRow++;
            }
        }
        protected void btnExport3Day_Click(object sender, EventArgs e)
        {
            IList list;
            int count = GetData(out list, false);
            if (count == 0)
            {
                ShowErrors(Resources.errorNoBookingSave);
                return;
            }
            DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            IList<ExpenseService> expenseList = new List<ExpenseService>();
            IList<ExpenseService> expenseListExport = new List<ExpenseService>();
            foreach (RepeaterItem rptItem in rptCruiseExpense.Items)
            {
                HiddenField hiddenId = (HiddenField)rptItem.FindControl("hiddenId");
                Cruise cruise = Module.CruiseGetById(Convert.ToInt32(hiddenId.Value));
                Expense expense = Module.ExpenseGetByDate(cruise, date);
                Module.SaveOrUpdate(expense);
                Repeater rptServices = (Repeater)rptItem.FindControl("rptServices");
                expenseList = rptServicesToIList(rptServices);

                if (String.IsNullOrEmpty(Request.QueryString["cruiseid"]))
                {
                    for (int i = 0; i < rptServicesToIList(rptServices).Count; i++)
                    {
                        expenseListExport.Add(rptServicesToIList(rptServices)[i]);
                    }
                }
                else
                {
                    expenseListExport = rptServicesToIList(rptServices);
                }
                foreach (ExpenseService service in expenseList)
                {
                    service.Expense = expense;
                    if (service.IsRemoved)
                    {
                        if (service.Id > 0)
                        {
                            expense.Services.Remove(service);
                        }
                    }
                    else
                    {
                        // Phải có tên hoặc giá thì mới lưu
                        if (!string.IsNullOrEmpty(service.Name) || service.Cost > 0 ||
                            service.Type.Id == SailsModule.GUIDE_COST)
                        {
                            Module.SaveOrUpdate(service);
                        }
                    }
                }

                Module.SaveOrUpdate(expense);
            }

            if (Cruise != null)
            {
                DayNote note = Module.DayNoteGetByDay(Cruise, date);

                if (!string.IsNullOrEmpty(note.Note) || note.Id > 0)
                {
                    Module.SaveOrUpdate(note);
                }
            }

            LoadService(date);

            if (Cruise != null)
            {
                BarRevenue bar = Module.BarRevenueGetByDate(Cruise, date);
                Module.SaveOrUpdate(bar);
            }

            string tplPath = Server.MapPath("/Modules/Sails/Admin/ExportTemplates/Lenhdieutour_dayboat.xls");
            if (String.IsNullOrEmpty(Request.QueryString["cruiseid"]))
                TourCommand.Export2(list, count, expenseListExport, _date, BookingFormat, Response, tplPath, null);
            else
            {
                var cruise = Module.GetObject<Cruise>(Convert.ToInt32(Request.QueryString["cruiseid"]));
                TourCommand.Export2(list, count, expenseListExport, _date, BookingFormat, Response, tplPath, cruise);
            }
        }
        public IEnumerable<BookingRoom> BookingRoomGetAllByBooking(Booking booking)
        {
            return BookingReportBLL.BookingRoomGetAllByBooking(booking);
        }
        public string GetAgencyNotes(Agency agency)
        {
            var roles = CurrentUser.Roles;
            var note = "";
            foreach (Role role in roles)
            {
                var agencyNoteses = BookingReportBLL.AgencyNotesGetAllByAgencyAndRole(agency, role);
                note += string.Join("<br/>", agencyNoteses.Select(an => an.Note));
                if (role.Name == "Administrator")
                {
                    agencyNoteses = BookingReportBLL.AgencyNotesGetAllByAgency(agency);
                    note = string.Join("<br/>", agencyNoteses.Select(an => an.Note));
                    break;
                }
            }
            return note;
        }

        protected void rptTrips_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var hplTrip = e.Item.FindControl("hplTrip") as HyperLink;
                hplTrip.CssClass = "btn btn-default";
                var trip = (SailsTrip)e.Item.DataItem;
                if (trip.Id.ToString() == Request.QueryString["tripid"])
                {
                    hplTrip.CssClass = hplTrip.CssClass + " " + "active";
                }
                hplTrip.Text = string.Format("{0}({1})", trip.Name, _customers.ContainsKey(trip.Id) ? _customers[trip.Id] : 0);
                hplTrip.NavigateUrl = string.Format(
                    "BookingReport.aspx?NodeId={0}&SectionId={1}&Date={2}&tripid={3}", Node.Id, Section.Id,
                    Date.ToString("dd/MM/yyyy"), trip.Id);
            }
            else
            {
                HyperLink hplCruises = e.Item.FindControl("hplTrip") as HyperLink;
                if (hplCruises != null)
                {
                    if (Request.QueryString["tripid"] == null)
                    {
                        hplCruises.CssClass = "btn btn-default active";
                    }
                    hplCruises.NavigateUrl = string.Format(
                        "BookingReport.aspx?NodeId={0}&SectionId={1}&Date={2}", Node.Id, Section.Id, Date.ToString("dd/MM/yyyy"));
                }
            }
        }
    }
}