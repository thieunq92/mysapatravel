using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Utils;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.ReportEngine;
using Portal.Modules.OrientalSails.Web.Controls;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;
using CMS.Core.Domain;
using System.Web.Services;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.Hosting;
using System.Web.UI.HtmlControls;
using Portal.Modules.OrientalSails.BusinessLogic;
using System.Linq;
using Portal.Modules.OrientalSails.Enums;
using System.Drawing;
using Portal.Modules.Orientalsails.Service.Share;
using Portal.Modules.OrientalSails.BusinessLogic.Share;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BookingView : SailsAdminBasePage
    {

        private readonly ILog _logger = LogManager.GetLogger(typeof(BookingView));

        private string[] arr;
        private IList _extraServices;
        private BookingViewBLL bookingViewBLL;
        private Booking booking;
        private UserBLL userBLL;
        private PermissionBLL permissionBLL;
        private EmailService emailService;
        private int NumberOfDay = 0;

        public BookingViewBLL BookingViewBLL
        {
            get
            {
                if (bookingViewBLL == null)
                    bookingViewBLL = new BookingViewBLL();
                return bookingViewBLL;
            }
        }

        public Booking Booking
        {
            get
            {
                try
                {
                    if (Request.QueryString["bi"] != null)
                        booking = BookingViewBLL.BookingGetById(Convert.ToInt32(Request.QueryString["bi"]));
                    if (booking == null)
                    {
                        Response.Redirect(string.Format("BookingList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
                    }
                }
                catch (Exception)
                {
                    Response.Redirect(string.Format("BookingList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
                }
                return booking;
            }
        }

        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                    userBLL = new UserBLL();
                return userBLL;
            }
        }

        public User CurrentUser
        {
            get
            {
                return UserBLL.UserGetCurrent();
            }
        }

        public PermissionBLL PermissionUtil
        {
            get
            {
                if (permissionBLL == null)
                    permissionBLL = new PermissionBLL();
                return permissionBLL;
            }
        }

        protected IList ExtraServices
        {
            get
            {
                if (_extraServices == null)
                {
                    _extraServices = Module.ExtraOptionGetAll();
                }
                return _extraServices;
            }
        }

        public EmailService EmailService
        {
            get
            {
                if (emailService == null)
                    emailService = new EmailService();
                return emailService;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlLoadData();
                BookingLoadData();
                WarningShowIfNotBookingOwner();
                WarningShowIfCruiseLocked();
                rptBusType.DataSource = BookingViewBLL.BusTypeGetAll().Future().ToList();
                rptBusType.DataBind();
                if (Booking.Transfer_Service == "One Way")
                {
                    rbtTransferService_OneWay.Checked = true;
                }
                else
                    if (Booking.Transfer_Service == "Two Way")
                    {
                        rbtTransferService_TwoWay.Checked = true;
                    }
                txtTransfer_Note.Text = Booking.Transfer_Note;
            }
            BookingHistorySave();
        }

        public void WarningShowIfCruiseLocked()
        {
            var isLocked = false;
            var cruiseId = -1;
            try
            {
                cruiseId = Booking.Cruise.Id;
            }
            catch { }

            DateTime? startDate = Booking.StartDate;
            DateTime? endDate = Booking.EndDate;
            var locks = BookingViewBLL.LockedGetBy(startDate, endDate, cruiseId);
            if (locks.Count() > 0)
                isLocked = true;

            string lockDate = "";
            foreach (var locked in locks)
            {
                lockDate = lockDate + locked.Start.ToString("dd/MM/yyyy") + ",";
            }
            if (lockDate.Length > 0)
                lockDate = lockDate.Remove(lockDate.Length - 1);

            if (isLocked)
            {
                try
                {
                    ShowWarning("Cruise " + Booking.Cruise.Name + " is locked on " + lockDate);
                }
                catch { }
            }
        }

        public void WarningShowIfNotBookingOwner()
        {
            try
            {
                var warning = "You're editing booking someone else is in charge, please noticed that if you submit any changes an email will be send to him/her";

                if (CurrentUser.Id != Booking.Agency.Sale.Id)
                    ShowWarning(warning);
            }
            catch { }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (bookingViewBLL != null)
            {
                bookingViewBLL.Dispose();
                bookingViewBLL = null;
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

        protected void rptRoomList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CustomersInfo.rptRoomList_itemDataBound(sender, e, Module, false, null, this, ddlRoomTypes.Items);
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Params[txtPickup.UniqueID]))
            {
                ShowWarning("Pick up address is required.");
            }

            //ScreenCaptureSave();
            BookingSetData();
            BookingViewBLL.BookingSaveOrUpdate(Booking);
            ShowSuccess("Booking updated. Please fix errors if exists and submit again");

            bool needEmail = !Booking.Special && chkSpecial.Checked;
            string email = string.Empty;
            if (needEmail)
            {
                email = "&confirm=1";
            }
            foreach (RepeaterItem extraService in rptExtraServices.Items)
            {
                var chkService = (HtmlInputCheckBox)extraService.FindControl("chkService");
                if (chkService.Checked)
                {
                    SaveExtraService();
                }
                else
                {
                    DeleteExtraService();
                }
            }
            foreach (RepeaterItem item in rptCustomers.Items)
            {
                CustomerInfoRowInput customerInfo1 = item.FindControl("customerData") as CustomerInfoRowInput;
                if (customerInfo1 != null)
                {
                    Customer customer1 = customerInfo1.NewCustomer(Module);
                    customer1.Booking = this.Booking;
                    Module.SaveOrUpdate(customer1);

                    Repeater rptService1 = item.FindControl("rptServices1") as Repeater;
                    if (rptService1 != null)
                    {
                        if (DetailService)
                        {
                            CustomerServiceRepeaterHandler.Save(rptService1, Module, customer1);
                        }
                    }
                }
            }
            Booking.EndDate = Booking.StartDate.AddDays(Booking.Trip.NumberOfDay - 1);
            Session["Redirect"] = true;
            PageRedirect(string.Format("BookingView.aspx?NodeId={0}&SectionId={1}&bi={2}{3}", Node.Id, Section.Id, Booking.Id, email));
        }

        public void ScreenCaptureSave()
        {
            byte[] bytes = Convert.FromBase64String(ScreenCapture.Value);
            System.Drawing.Image screenCaptureImage;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                screenCaptureImage = System.Drawing.Image.FromStream(ms);
            }

            var bookingId = Booking.Id;
            var userName = CurrentUser.FullName;
            var directoryPath = Server.MapPath(String.Format("/UserFiles/ScreenCapture/{0}/{1}/", bookingId, userName));
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            screenCaptureImage.Save(directoryPath +
                String.Format("{0}.png", DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")), System.Drawing.Imaging.ImageFormat.Png);
        }

        public void BookingSetData()
        {
            Booking.Inspection = chkInspection.Checked;
            Booking.CancelledReason = txtCancelledReason.Text;
            Booking.AgencyCode = txtAgencyCode.Text;
            Booking.PickupAddress = txtPickup.Text;
            Booking.SpecialRequest = txtSpecialRequest.Text;
            Booking.IsPaymentNeeded = chkIsPaymentNeeded.Checked;
            Booking.Note = txtCustomerInfo.Text;
            Booking.Special = chkSpecial.Checked;
         
            Booking.IsEarlyBird = chkEarlyBird.Checked;

            try
            {
                Booking.Agency = Module.AgencyGetById(Convert.ToInt32(ddlAgencies.SelectedValue));
            }
            catch { }

            if (Booking.Agency == null)
            {
                ShowErrors("Please select one agency");
            }

            try
            {
                Booking.Deadline = DateTime.ParseExact(txtDeadline.Text, "dd/MM/yyyy HH:mm",
                                                        CultureInfo.InvariantCulture);
            }
            catch { }

            try
            {
                Booking.Booker = Module.ContactGetById(Convert.ToInt32(cddlBooker.SelectedValue));
            }
            catch { }

            try
            {
                Booking.IsTotalUsd = Convert.ToBoolean(Int32.Parse(ddlCurrencies.SelectedValue));
            }
            catch { }

            try
            {
                Booking.CancelPay = Convert.ToDouble(txtPenalty.Text);
            }
            catch { }

            BookingStatusProcess();
            TripProcess();
        
            StartDateProcess();
        
            ExtraServicesProcess(); //not cleanly
            VoucherProcess();//not cleany
            TotalPriceProcess();//not cleany

            if (Booking.Trip.NumberOfDay == 3)
            {
                Booking.TripOption = (TripOption)ddlOptions.SelectedIndex;
            }

            var seriesCode = "";
            seriesCode = txtSeriesCode.Text;

            if (!String.IsNullOrEmpty(seriesCode))
            {
                var series = BookingViewBLL.SeriesGetBySeriesCode(seriesCode);
                if (series != null)
                    Booking.Series = series;
                else
                    ShowErrors("Không tồn tại series này");
            }
            var guideExpense = 0.0;
            try
            {
                guideExpense = Double.Parse(txtGuideExpense.Text);
            }catch(Exception){}
            Booking.Expense_Guide = guideExpense;
            var mealExpense = 0.0;
            try
            {
                mealExpense = Double.Parse(txtMealExpense.Text);
            }
            catch (Exception) { }
            Booking.Expense_Meal = mealExpense;
            var hotelExpense = 0.0;
            try
            {
                hotelExpense = Double.Parse(txtHotelExpense.Text);
            }
            catch (Exception) { }
            Booking.Expense_Hotel = hotelExpense;
            var busExpense = 0.0;
            try
            {
                busExpense = Double.Parse(txtBusExpense.Text);
            }
            catch (Exception) { }
            Booking.Expense_Bus = busExpense;
            var ticketExpense = 0.0;
            try
            {
                ticketExpense = Double.Parse(txtTicketExpense.Text);
            }
            catch (Exception) { }
            Booking.Expense_Ticket = ticketExpense;
            var limousineExpense = 0.0;
            try
            {
                limousineExpense = Double.Parse(txtLimousineExpense.Text);
            }
            catch (Exception) { }
            Booking.Expense_Limousine = limousineExpense;
            var busHaNoiSapaExpense = 0.0;
            try
            {
                busHaNoiSapaExpense = Double.Parse(txtBusHaNoiSapaExpense.Text);
            }
            catch (Exception) { }
            Booking.Expense_Bus_HanoiSapa = busHaNoiSapaExpense; 
            var commissionExpense = 0.0;
            try
            {
                commissionExpense = Double.Parse(txtCommissionExpense.Text);
            }
            catch (Exception) { }
            Booking.Expense_Commission = commissionExpense;

            Booking.Expense_Meal_CurrencyType = ddlMealExpenseCurrencyType.SelectedValue;
            Booking.Expense_Hotel_CurrencyType = ddlHotelExpenseCurrencyType.SelectedValue;
            Booking.Expense_Guide_CurrencyType = ddlGuideExpenseCurrencyType.SelectedValue;
            Booking.Expense_Ticket_CurrencyType = ddlTicketExpenseCurrencyType.SelectedValue;
            Booking.Expense_Bus_CurrencyType = ddlBusExpenseCurrencyType.SelectedValue;
            Booking.Expense_Limousine_CurrencyType = ddlLimousineExpenseCurrencyType.SelectedValue;
            Booking.Expense_Bus_HanoiSapa_CurrencyType = ddlBusHaNoiSapaExpenseCurrencyType.SelectedValue;
            Booking.Expense_Commission_CurrencyType = ddlCommissionExpenseCurrencyType.SelectedValue;
        }


        public void TotalPriceProcess()
        {
            if (!txtTotal.ReadOnly)
            {
                double total = Convert.ToDouble(txtTotal.Text);
                double finalTotal = total;

                if (total <= 0)
                {
                    try
                    {
                        finalTotal = Booking.Calculate(Module, null, ChildPrice,
                                                        Convert.ToDouble(Module.ModuleSettings("AgencySupplement")),
                                                        CustomPriceForRoom, true);
                    }
                    catch (Exception ex)
                    {
                        ShowErrors(Resources.errorCanNotCalculatePrice);
                    }
                }
                else
                {
                    finalTotal = total;
                }

                if (Booking.Total != finalTotal)
                {
                    Booking.AccountingStatus = AccountingStatus.Modified;
                    Booking.Total = finalTotal;
                }
            }
        }

        public void VoucherProcess()
        {
            if (txtAllVoucher.Text.ToLower() != "ov" && !string.IsNullOrEmpty(txtAllVoucher.Text))
            {

                var trimmedCode = txtAllVoucher.Text.Trim();
                if (trimmedCode.EndsWith(";"))
                    arr = trimmedCode.Remove(trimmedCode.Length - 1).Split(new char[] { ';' });
                else
                    arr = trimmedCode.Split(new char[] { ';' });


                foreach (string codeString in arr)
                {
                    var code = Convert.ToUInt32(codeString.Trim());
                    int batchid;
                    int index;
                    VoucherCodeEncryption.Decrypt(code, out batchid);

                    ICriterion crit = Expression.Eq("Code", code.ToString());
                    crit = Expression.And(crit, Expression.Not(Expression.Eq("Booking.Id", Booking.Id)));
                    bool isUsed = false;
                    foreach (BookingVoucher bv in Module.GetObject<BookingVoucher>(crit, 0, 0))
                    {
                        if (bv.Booking.Status == StatusType.Approved || bv.Booking.Status == StatusType.Pending)
                            isUsed = true;
                    }

                    if (isUsed)
                    {
                        ShowErrors(string.Format("Voucher code {0} already used!", codeString));
                        return;
                    }

                    var batch = Module.GetObject<VoucherBatch>(batchid);

                    if (batch == null)
                    {
                        ShowErrors(string.Format("Voucher code {0} invalid!", codeString));
                        return;
                    }
                    else if (batch.ValidUntil < Booking.StartDate)
                    {
                        ShowErrors(string.Format("Voucher code {0} outdated!", codeString));
                        return;
                    }

                    Booking.Batch = batch;

                    var bkv = Module.GetBookingVoucher(Booking, codeString);
                    bkv.Voucher = batch;

                    Module.SaveOrUpdate(bkv, UserIdentity);
                }
            }

            Booking.VoucherCode = txtAllVoucher.Text;
        }

        public void ExtraServicesProcess()
        {
            foreach (RepeaterItem item in rptExtraServices.Items)
            {
                HiddenField hiddenId = (HiddenField)item.FindControl("hiddenId");
                HtmlInputCheckBox chkService = (HtmlInputCheckBox)item.FindControl("chkService");
                ExtraOption option = Module.ExtraOptionGetById(Convert.ToInt32(hiddenId.Value));
                BookingExtra extra = Module.BookingExtraGet(Booking, option);
                if (extra.Id <= 0 && chkService.Checked)
                {
                    Module.SaveOrUpdate(extra);
                }
                if (extra.Id > 0 && !chkService.Checked)
                {
                    Module.Delete(extra);
                }
            }
        }

        
    

        public void TripProcess()
        {
            var trip = GetTrip();

            bool didChangeTrip = false;
            try
            {
                didChangeTrip = trip.Id != Booking.Trip.Id ? true : false;
            }
            catch { }

            Booking.Trip = trip;
        }


        public void StartDateProcess()
        {
            var startDate = GetStartDate();
            booking.StartDate = startDate.Value;
            bool didChangeStartdate = Booking.StartDate != startDate ? true : false;
        }

        public void BookingStatusProcess()
        {
            var statusType = (StatusType)Enum.Parse(typeof(StatusType), ddlStatusType.SelectedValue);
            if (statusType == StatusType.Approved)
                Booking.Status = statusType;
            if (statusType == StatusType.Cancelled)
                CancelledStatusProcess();
            if (statusType == StatusType.Pending)
                PendingStatusProcess();
            if (statusType == StatusType.CutOff)
                CutOffStatusProcess();
        }

        public void CutOffStatusProcess()
        {
            var statusType = (StatusType)Enum.Parse(typeof(StatusType), ddlStatusType.SelectedValue);
            try
            {
                Booking.CutOffDays = Int32.Parse(txtCutOffDays.Text.Trim());
            }
            catch
            {
                ShowErrors("CutOffDays is not valid");
                return;
            }
            Booking.Status = statusType;
        }

        private void PendingStatusProcess()
        {
            var statusType = (StatusType)Enum.Parse(typeof(StatusType), ddlStatusType.SelectedValue);
            var bookingHistories = BookingViewBLL.BookingHistoryGetByBookingId(Booking.Id).OrderBy(x => x.Date);
            var bookingLastStatus = bookingHistories.Last().Status;
            var canApplyStatus = bookingLastStatus == StatusType.Approved ? false : true;

            if (canApplyStatus)
                Booking.Status = statusType;
            else
                ShowErrors("Approved booking can not switch to pending");
        }

        public void CancelledStatusProcess()
        {
            bool canSendEmail = false;
            var statusType = (StatusType)Enum.Parse(typeof(StatusType), ddlStatusType.SelectedValue);
            var isEmptyReason = String.IsNullOrEmpty(txtCancelledReason.Text);
            var canApplyCancelled = statusType == StatusType.Cancelled && !isEmptyReason;

            if (canApplyCancelled)
            {
                Booking.Status = statusType;
                canSendEmail = true;
            }
            else
            {
                ShowErrors("Chưa nhập lý do hủy booking. Không thể hủy booking");
                canSendEmail = false;
            }

            if (canSendEmail)
                SendEmailCancelled();
        }

        protected void rptRoomList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    BookingRoom bookingRoom = bookingViewBLL.BookingRoomGetById(Convert.ToInt32(e.CommandArgument));
                    Booking.BookingRooms.Remove(bookingRoom);
                    bookingViewBLL.BookingRoomDelete(bookingRoom);
                    break;

            }

            BookingHistorySave();
            Response.Redirect(Request.RawUrl);
        }

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
          
            Customer customer1 = new Customer();
            customer1.Booking = Booking;
            customer1.Type = CustomerType.Adult;
            Module.SaveOrUpdate(customer1);
            var history = new BookingHistory();
            history.Booking = Booking;
            history.Date = DateTime.Now;
            history.Agency = Booking.Agency;
            history.StartDate = Booking.StartDate;
            history.Status = Booking.Status;
            history.Trip = Booking.Trip;
            history.User = UserIdentity;
            history.SpecialRequest = Booking.SpecialRequest;
            history.NumberOfPax = (Booking.Adult + Booking.Child + Booking.Baby);
            history.CustomerInfo = Booking.Note;
            Module.SaveOrUpdate(history);
            PageRedirect(Request.RawUrl);
        }

        public void BookingTrackSaveAddRoom()
        {
            BookingTrack track = new BookingTrack()
            {
                Booking = Booking,
                ModifiedDate = DateTime.Now,
                User = CurrentUser,
            };

            BookingChanged change = new BookingChanged()
            {
                Action = BookingAction.AddRoom,
                Parameter = ddlRoomTypes.SelectedItem.Text,
                Track = track,
            };
            Module.SaveOrUpdate(change);

            BookingChanged customerChange = new BookingChanged()
            {
                Action = BookingAction.AddCustomer,
                Parameter = string.Format("{0}|{1}|{2}", 2, 0, 0),
                Track = track,
            };
            Module.SaveOrUpdate(customerChange);
        }

        public void BookingRoomAdd(RoomClass roomClass, RoomTypex roomType)
        {
            BookingRoom bookingRoom = new BookingRoom()
            {
                RoomClass = roomClass,
                RoomType = roomType,
                Book = Booking
            };

            Customer customer1 = new Customer()
            {
                Type = CustomerType.Adult,
                BookingRooms = new List<BookingRoom>(),
            };
            customer1.BookingRooms.Add(bookingRoom);

            Customer customer2 = new Customer()
            {
                Type = CustomerType.Adult,
                BookingRooms = new List<BookingRoom>(),
            };
            customer2.BookingRooms.Add(bookingRoom);

            bookingRoom.Customers.Add(customer1);
            bookingRoom.Customers.Add(customer2);

            BookingViewBLL.CustomerSaveOrUpdate(customer1);
            BookingViewBLL.CustomerSaveOrUpdate(customer2);
            BookingViewBLL.BookingRoomSaveOrUpdate(bookingRoom);
        }

        protected void lbtCalculate_Click(object sender, EventArgs e)
        {

        }

        protected void rptExtraServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlInputCheckBox chkService = (HtmlInputCheckBox)e.Item.FindControl("chkService");
            ExtraOption option = (ExtraOption)e.Item.DataItem;
            chkService.Checked = Booking.ExtraServices.Select(x => x.Id).Contains(option.Id);
            chkService.Name = option.Name;
            string script = "var " + option.Name + "=" + chkService.Checked.ToString().ToLower();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "initChkServiceValue", script, true);
        }

        protected void btnLockIncome_Click(object sender, EventArgs e)
        {
            if (!PermissionUtil.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.LOCK_INCOME))
            {
                ShowErrors("You don't have permission to perform this action");
                return;
            }
            Booking.LockDate = DateTime.Now;
            Booking.LockBy = CurrentUser;
            BookingViewBLL.BookingSaveOrUpdate(Booking);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnUnlockIncome_Click(object sender, EventArgs e)
        {
            if (!PermissionUtil.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.LOCK_INCOME))
            {
                ShowErrors("You don't have permission to perform this action");
                return;
            }
            Booking.LockDate = null;
            Booking.LockBy = null;
            BookingViewBLL.BookingSaveOrUpdate(Booking);
            Response.Redirect(Request.RawUrl);
        }

       

        public void ControlLoadData()
        {
            ddlStatusType.DataSource = Enum.GetNames(typeof(StatusType));
            ddlStatusType.DataBind();

            ddlAgencies.DataSource = BookingViewBLL.AgencyGetAll().OrderBy(x => x.Name);
            ddlAgencies.DataTextField = "Name";
            ddlAgencies.DataValueField = "Id";
            ddlAgencies.DataBind();
            ddlAgencies.Items.Insert(0, "-- Agency --");

            var trips = BookingViewBLL.TripGetAll();
            foreach (var trip in trips)
            {
                var listItemTrip = new ListItem(trip.Name, trip.Id.ToString());
                if (trip.NumberOfOptions == 2)
                {
                    listItemTrip.Attributes.Add("data-option-visible", "true");
                }
                ddlTrips.Items.Add(listItemTrip);
            }

         
            cddlBooker.DataSource = Module.ContactGetAllEnabled();
            cddlBooker.DataTextField = "Name";
            cddlBooker.DataValueField = "Id";
            cddlBooker.DataParentField = "AgencyId";
            cddlBooker.ParentClientID = ddlAgencies.ClientID;
            cddlBooker.DataBind();
            cddlBooker.Items.Insert(0, "-- Booker --");

            rptExtraServices.DataSource = Module.ExtraOptionGetBooking();
            rptExtraServices.DataBind();

            TotalDisplay();
            TotalLockedDisplay();
            AddRoomControlGenerate();
            CustomerBirthdayDisplay();

        }

        public void TotalDisplay()
        {
            var canViewTotal = PermissionUtil.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.VIEW_TOTAL_BY_DATE);
            if (!canViewTotal)
                HideTotal();

        }

        public void HideTotal()
        {
            txtTotal.Visible = false;
            ddlCurrencies.Visible = false;
        }

        public void BookingLoadData()
        {
            lblBookingId.Text = string.Format("MYS{0:00000}", Booking.Id);
            txtTotal.Text = Booking.Total.ToString("#,0.#");
            ddlStatusType.SelectedValue = Enum.GetName(typeof(StatusType), Booking.Status);
            txtStartDate.Text = Booking.StartDate.ToString("dd/MM/yyyy");
            txtAllVoucher.Text = Booking.VoucherCode;
            chkIsPaymentNeeded.Checked = Booking.IsPaymentNeeded;
            txtCustomerInfo.Text = Booking.Note;
            txtCancelledReason.Text = Booking.CancelledReason;
            litPax.Text = Booking.Pax.ToString();
          
            txtAgencyCode.Text = Booking.AgencyCode;
            ddlOptions.SelectedIndex = (int)Booking.TripOption;
            txtPickup.Text = Booking.PickupAddress;
            txtSpecialRequest.Text = Booking.SpecialRequest;
            chkInspection.Checked = Booking.Inspection;
            chkSpecial.Checked = Booking.Special;
    
            chkInvoice.Checked = Booking.HasInvoice;
            chkEarlyBird.Checked = Booking.IsEarlyBird;
            txtPenalty.Text = Booking.CancelPay.ToString();
            ddlCurrencies.SelectedValue = Convert.ToInt32(Booking.IsTotalUsd).ToString();
            txtCutOffDays.Text = Booking.CutOffDays.ToString();
            try
            {
                txtSeriesCode.Text = Booking.Series.SeriesCode;
            }
            catch
            {

            }

            try
            {
                cddlBooker.SelectedValue = Booking.Booker.Id.ToString();
            }
            catch { }

            try
            {
                txtDeadline.Text = Booking.Deadline.Value.ToString("dd/MM/yyyy HH:mm");
            }
            catch { }

            try
            {
                ddlTrips.SelectedValue = Booking.Trip.Id.ToString();
            }
            catch { }

            string text = "";
            try
            {
                text += string.Format("Created by {0} at {1}", Booking.CreatedBy.FullName,
                                     Booking.CreatedDate.ToString("dd/MM/yyyy HH:mm"));
            }
            catch { }

            try
            {
                text += string.Format(" and last edited by {0} at {1}", Booking.ModifiedBy.FullName,
                                 Booking.ModifiedDate.ToString("dd/MM/yyyy HH:mm"));
            }
            catch { }


            try
            {
                ddlAgencies.SelectedValue = Booking.Agency.Id.ToString();
            }
            catch { }
            litCreated.Text = text;
            NumberOfDay = booking.Trip.NumberOfDay;
            rptCustomers.DataSource = Booking.Customers;
            rptCustomers.DataBind();
            txtGuideExpense.Text = Booking.Expense_Guide.ToString("#,0.##");
            txtHotelExpense.Text = Booking.Expense_Hotel.ToString("#,0.##");
            txtMealExpense.Text = Booking.Expense_Meal.ToString("#,0.##");
            txtBusExpense.Text = Booking.Expense_Bus.ToString("#,0.##");
            txtTicketExpense.Text = Booking.Expense_Ticket.ToString("#,0.##");
            txtLimousineExpense.Text = Booking.Expense_Limousine.ToString("#,0.##");
            txtBusHaNoiSapaExpense.Text = Booking.Expense_Bus_HanoiSapa.ToString("#,0.##");
            txtCommissionExpense.Text = Booking.Expense_Commission.ToString("#,0.##");
            ddlGuideExpenseCurrencyType.SelectedValue = Booking.Expense_Guide_CurrencyType;
            ddlHotelExpenseCurrencyType.SelectedValue = Booking.Expense_Hotel_CurrencyType;
            ddlMealExpenseCurrencyType.SelectedValue = Booking.Expense_Meal_CurrencyType;
            ddlBusExpenseCurrencyType.SelectedValue = Booking.Expense_Bus_CurrencyType;
            ddlTicketExpenseCurrencyType.SelectedValue = Booking.Expense_Ticket_CurrencyType;
            ddlLimousineExpenseCurrencyType.SelectedValue = Booking.Expense_Limousine_CurrencyType;
            ddlBusHaNoiSapaExpenseCurrencyType.SelectedValue = Booking.Expense_Bus_HanoiSapa_CurrencyType;
            ddlCommissionExpenseCurrencyType.SelectedValue = Booking.Expense_Commission_CurrencyType;
        }

        public void CustomerBirthdayDisplay()
        {
            var customersBirthday = new List<Customer>();
            foreach (var bookingRoom in Booking.BookingRooms)
            {
                foreach (var customer in bookingRoom.RealCustomers)
                {
                    DateTime? customerBirthday = null;
                    try
                    {
                        customerBirthday = customer.Birthday.Value;
                    }
                    catch { return; }

                    if (customerBirthday.Value.Day == Booking.StartDate.Day
                        && customerBirthday.Value.Month == Booking.StartDate.Month)
                    {
                        customersBirthday.Add(customer);
                    }
                }
            }

            if (customersBirthday.Count < 1)
                return;

            litInform.Text = "<i class='fa fa-lg fa-birthday-cake' aria-hidden='true'></i>  ";
            foreach (var customer in customersBirthday)
            {
                litInform.Text += customer.Fullname + ",";
            }
            litInform.Text += " have birthday on start date.";
        }

        public void AddRoomControlGenerate()
        {
            var haveRoomAvaiable = false;
            ddlRoomTypes.Items.Clear();
            //            var allRoom = Module.RoomGetAll2(Booking.Cruise);
            //            var list = new List<string>();
            //            foreach (Room room in allRoom)
            //            {
            //                if (!list.Contains(room.RoomClass.Name + " " + room.RoomType.Name))
            //                {
            //                    list.Add(room.RoomClass.Name + " " + room.RoomType.Name);
            //                    ddlRoomTypes.Items.Add(new ListItem(room.RoomType.Name + " " + room.RoomClass.Name, room.RoomClass.Id + "|" + room.RoomType.Id));
            //                }
            //
            //            }
            var roomTypes = Module.RoomTypexGetAll();
            foreach (RoomTypex room in roomTypes)
            {
                if (room.Name == "Double" || room.Name == "Twin")
                    ddlRoomTypes.Items.Add(new ListItem(room.Name, room.Id.ToString()));
            }
            //foreach (RoomClass rclass in AllRoomClasses)
            //{
            //    foreach (RoomTypex rtype in AllRoomTypes)
            //    {
            //        int avaiable = Module.RoomCount(rclass, rtype, Booking.Cruise, Booking.StartDate,
            //                                        Booking.Trip.NumberOfDay, Booking.Trip.HalfDay);
            //        if (avaiable > 0)
            //        {
            //            ddlRoomTypes.Items.Add(new ListItem(rtype.Name + " " + rclass.Name, rclass.Id + "|" + rtype.Id));
            //            haveRoomAvaiable = true;
            //        }
            //    }
            //}
            //plhAddRoom.Visible = haveRoomAvaiable ? true : false;
        }

        //public void TripLockDisplay()
        //{
        //    var haveEditTripAfterPermission = PermissionUtil.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.EDIT_TRIP_AFTER);

        //    if (!haveEditTripAfterPermission)
        //    {
        //        if (Booking.StartDate.AddHours(12) < DateTime.Now)
        //        {
        //            plhTripReadonly.Visible = true;
        //            try
        //            {
        //                litTrip.Text = Booking.Trip.Name;
        //            }
        //            catch { }
        //            ddlTrips.Visible = false;
        //        }
        //    }
        //}

        public void TotalLockedDisplay()
        {
            var isLocked = Booking.LockIncome;
            var haveEditAfterLockPermission = PermissionUtil.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.EDIT_AFTER_LOCK);
            var haveLockIncomePermission = PermissionUtil.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.LOCK_INCOME);

            if (isLocked)
            {
                if (!haveEditAfterLockPermission)
                {
                    txtTotal.ReadOnly = true;
                    txtTotal.CssClass = txtTotal.CssClass + " total-locked ";
                    ddlCurrencies.Enabled = false;
                }
            }

            if (haveLockIncomePermission)
            {
                btnLockIncome.Visible = true;
                btnUnlockIncome.Visible = false;
                if (isLocked)
                {
                    btnLockIncome.Visible = false;
                    btnUnlockIncome.Visible = true;
                }
            }
        }

        public void SendEmailCancelled()
        {
            try
            {
                string content = "";
                using (StreamReader streamReader = new StreamReader(HostingEnvironment.MapPath("/Modules/Sails/Admin/EmailTemplate/CancelledBookingNotify.txt")))
                {
                    content = streamReader.ReadToEnd();
                };
                var appPath = string.Format("{0}://{1}{2}{3}",
                                 Request.Url.Scheme,
                                 Request.Url.Host,
                                 Request.Url.Port == 80
                                     ? string.Empty
                                     : ":" + Request.Url.Port,
                                 Request.ApplicationPath);
                content = content.Replace("{link}",
                    appPath + "Modules/Sails/Admin/BookingView.aspx?NodeId=1&SectionId=15&bi=" + Booking.Id);
                content = content.Replace("{bookingcode}", Booking.BookingIdOS);
                content = content.Replace("{agency}", Booking.Agency.Name);
                content = content.Replace("{startdate}", Booking.StartDate.ToString("dd/MM/yyyy"));
                content = content.Replace("{trip}", Booking.Trip.Name);
                content = content.Replace("{cruise}", Booking.Cruise.Name);
                content = content.Replace("{customernumber}", Booking.Pax.ToString());
                content = content.Replace("{roomnumber}", Booking.BookingRooms.Count.ToString());
                content = content.Replace("{submiter}", UserIdentity.FullName);
                content = content.Replace("{reason}", Booking.CancelledReason);
                MailAddress fromAddress = new MailAddress("no-reply@orientalsails.com", "Hệ Thống MO OrientalSails");
                MailMessage message = new MailMessage();
                message.From = fromAddress;
                message.To.Add("reservation@orientalsails.com");
                if (Booking.CreatedBy != null)
                {
                    if (Booking.CreatedBy.Email != null)
                    {
                        if (Booking.CreatedBy.Email != "reservation@orientalsails.com")
                        {
                            message.To.Add(Booking.CreatedBy.Email);
                        }
                    }
                }

                if (Booking.ModifiedBy != null)
                {
                    if (Booking.ModifiedBy.Email != null)
                    {
                        if (Booking.ModifiedBy.Email != Booking.CreatedBy.Email)
                        {
                            if (Booking.ModifiedBy.Email != "reservation@orientalsails.com")
                            {
                                message.To.Add(Booking.ModifiedBy.Email);
                            }
                        }
                    }
                }

                message.To.Add("nhan@orientalsails.com");
                message.To.Add("it2@atravelmate.com");
                message.Subject = "Thông báo hủy booking";
                message.Body = content;
                message.Bcc.Add("it2@atravelmate.com");
                EmailService.SendMessage(message);
            }
            catch (Exception)
            {
            }
        }

        public string PaxGetDetails()
        {
            return string.Format("Adults : {0}</br> Childs : {1}<br/> Baby : {2}", Booking.Adult, Booking.Child, Booking.Baby);
        }

        public string CabinGetDetails()
        {
            return Booking.RoomName;
        }

        public string UserGetUserLockIncomeDetails()
        {
            if (Booking.LockDate.HasValue)
                return string.Format(
                                "Locked (individual booking) by {0} at {1:dd/MM/yyyy HH:mm}", Booking.LockByString,
                                Booking.LockDate.Value);

            return "";
        }

        public void BookingHistorySave()
        {
            var bookingHistory = new BookingHistory()
            {
                Booking = Booking,
                Date = DateTime.Now,
                User = CurrentUser,
                CabinNumber = Booking.BookingRooms.Count,
                CustomerNumber = Booking.Pax,
                StartDate = Booking.StartDate,
                Status = Booking.Status,
                Trip = Booking.Trip,
                Agency = Booking.Agency,
                Total = Booking.Total,
                TotalCurrency = Booking.IsTotalUsd ? "USD" : "VND",
                SpecialRequest = Booking.SpecialRequest,
                PickupAddress = Booking.PickupAddress,
            };
            Module.SaveOrUpdate(bookingHistory);
        }

        public SailsTrip GetTrip()
        {
            SailsTrip trip = null;
            try
            {
                trip = BookingViewBLL.TripGetById(Convert.ToInt32(ddlTrips.SelectedValue));
            }
            catch { }
            return trip;
        }


        public DateTime? GetStartDate()
        {
            DateTime? startDate = null;
            try
            {
                startDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            return startDate;
        }

        public void SaveExtraService()
        {
            var busTypeId = -1;
            try
            {
                busTypeId = Int32.Parse(Request.Form["radBusType"]);
            }
            catch { }
            Booking.Transfer_BusType = BookingViewBLL.BusTypeGetById(busTypeId);
            if (rbtTransferService_OneWay.Checked)
            {
                Booking.Transfer_Service = "One Way";
            }
            if (rbtTransferService_TwoWay.Checked)
            {
                Booking.Transfer_Service = "Two Way";
            }
            var transfer_DateTo = Booking.StartDate;
            try
            {
                transfer_DateTo = DateTime.ParseExact(txtTransfer_Dateto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var transfer_DateBack = Booking.EndDate;
            try
            {
                transfer_DateBack = DateTime.ParseExact(txtTransfer_Dateback.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            if (Booking.Transfer_Service == "One Way")
            {
                if (!String.IsNullOrEmpty(txtTransfer_Dateto.Text))
                {
                    Booking.Transfer_DateTo = transfer_DateTo;
                    Booking.Transfer_DateBack = null;
                }
                else
                    if (!String.IsNullOrEmpty(txtTransfer_Dateback.Text))
                    {
                        Booking.Transfer_DateTo = null;
                        Booking.Transfer_DateBack = transfer_DateBack;
                    }
            }
            if (Booking.Transfer_Service == "Two Way")
            {
                Booking.Transfer_DateBack = transfer_DateBack;
                Booking.Transfer_DateTo = transfer_DateTo;
            }
            Booking.Transfer_Note = txtTransfer_Note.Text;
            BookingViewBLL.BookingSaveOrUpdate(Booking);
        }

        public void DeleteExtraService()
        {
            Booking.Transfer_BusType = null;
            Booking.Transfer_DateTo = null;
            Booking.Transfer_DateBack = null;
            Booking.Transfer_Note = null;
            Booking.Transfer_Service = null;
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
        protected void rptCustomers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Customer)
            {
                Customer customer = (Customer)e.Item.DataItem;
                CustomerInfoRowInput customerData = e.Item.FindControl("customerData") as CustomerInfoRowInput;
                if (customerData != null)
                {
                    customerData.GetCustomer(customer, Module);
                }
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnDelete.Parent;
            CustomerInfoRowInput customerData = item.FindControl("customerData") as CustomerInfoRowInput;
            if (customerData != null)
            {
                Customer customer = customerData.NewCustomer(Module);
                Module.Delete(customer);
                var history = new BookingHistory();
                history.Booking = Booking;
                history.Date = DateTime.Now;
                history.Agency = Booking.Agency;
                history.StartDate = Booking.StartDate;
                history.Status = Booking.Status;
                history.Trip = Booking.Trip;
                history.User = UserIdentity;
                history.SpecialRequest = Booking.SpecialRequest;
                history.NumberOfPax = (Booking.Adult + Booking.Child + Booking.Baby);
                history.CustomerInfo = Booking.Note;
                Module.SaveOrUpdate(history);
            }
            PageRedirect(Request.RawUrl);
        }
    }
}