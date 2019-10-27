using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Media3D;
using CMS.Core.Domain;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.Utils;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BookingSelectedPayment : SailsAdminBasePage
    {
        //public bool AllowPaymentBooking
        //{
        //    get
        //    {
        //        return PermissionBLL.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.AllowPaymentBooking);
        //    }
        //}
        private IList<Booking> _listBooking;
        private BookingPaymentBLL bookingPaymentBLL;
        private User currentUser;
        private UserBLL userBLL;

        public BookingPaymentBLL BookingPaymentBLL
        {
            get
            {
                if (bookingPaymentBLL == null)
                    bookingPaymentBLL = new BookingPaymentBLL();
                return bookingPaymentBLL;
            }
        }
        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                {
                    userBLL = new UserBLL();
                }
                return userBLL;
            }
        }
        public User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    currentUser = UserBLL.UserGetCurrent();
                }
                return currentUser;
            }
        }

        public IList<Booking> ListBooking
        {
            get
            {
                _listBooking = new List<Booking>();
                if (!string.IsNullOrWhiteSpace(Request["day"]))
                {

                    //var to = DateTimeUtil.DateGetDefaultToDate();
                    //if (!string.IsNullOrEmpty(Request.QueryString["t"]))
                    //{
                    //    to = DateTime.ParseExact(Request.QueryString["t"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //}
                    //var agencyId = -1;
                    //if (!string.IsNullOrEmpty(Request.QueryString["ai"]))
                    //{
                    //    agencyId = Int32.Parse(Request.QueryString["ai"]);
                    //}
                    //_listBooking = BookingPaymentBLL.GetBookingDebtReceivables(to, agencyId).Where(b => b.MoneyLeft > 0).OrderBy(b => b.CreatedDate).ToList();
                }
                else
                {
                    try
                    {
                        var bookingIds = Request.QueryString["lbi"].Split(new char[] { ',' });
                        var listBookingId = bookingIds.Select(x => Int32.Parse(x)).ToList();
                        _listBooking = BookingPaymentBLL.BookingGetAllByListId(listBookingId)
                            .Future()
                            .Where(b => b.MoneyLeft > 0)
                            .OrderBy(x => x.CreatedDate)
                            .ToList();
                    }
                    catch { }
                }
                return _listBooking;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(btnPayment, string.Empty);
            if (!string.IsNullOrWhiteSpace(Request["day"]))
            {
                btnPayment20.Visible = true;
                btnPayment.Visible = false;
                phList.Visible = false;
            }
            else
            {
                btnPayment20.Visible = false;
                btnPayment.Visible = true;
                LoadData();
            }
            if (!IsPostBack)
            {
                txtRate.Text = BookingPaymentBLL.USDRateGetByDate(DateTime.Now).Rate.ToString("#,##0.##");
            }
        }

        private void LoadData()
        {
            rptBooking.DataSource = ListBooking;
            rptBooking.DataBind();
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            var rate = Convert.ToDouble(txtRate.Text);
            var bookings = new List<Booking>(ListBooking);
            var payUsd = 0.0;
            var payVnd = 0.0;


            if (!string.IsNullOrWhiteSpace(txtPaidUSD.Text))
            {
                payUsd = Convert.ToDouble(txtPaidUSD.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtPaidVND.Text))
            {
                payVnd = Convert.ToDouble(txtPaidVND.Text);
            }
            var transactionGroup = new TransactionGroup()
            {
                CreatedDate = DateTime.Now,
                CreatedBy = UserIdentity,
                USDAmount = payUsd,
                VNDAmount = payVnd,
                Note = txtNote.Text
            };
            Module.SaveOrUpdate(transactionGroup);
            if (payUsd > 0)
            {
                var list = new List<Booking>(bookings.Where(x => x.IsTotalUsd).OrderBy(x => x.MoneyLeft));
                foreach (Booking booking in list)
                {
                    if (booking.IsTotalUsd && booking.MoneyLeft <= payUsd)
                    {
                        var money = booking.MoneyLeft;
                        PaymentBooking(transactionGroup, booking, money, 0, rate);
                        payUsd = payUsd - money;
                    }
                }
                // thua usd
                if (payUsd > 0)
                {
                    if (bookings.All(b => b.IsPaid))
                    {
                        // van con usd ma het booking thi cong thanh toan vao 1 booking gan nhat
                        phResult.Visible = true;
                        var booking = bookings.Where(b => b.IsTotalUsd).OrderByDescending(b => b.CreatedDate)
                            .First();
                        PaymentBooking(transactionGroup, booking, payUsd, 0, rate);
                        litResultUsd.Text = string.Format("Thừa ${0:#,##0.##} sẽ chuyển cho booking {1}", payUsd,
                            booking.BookingIdOS);
                    }
                    else
                    {
                        // thanh toán = usd
                        foreach (Booking booking in bookings)
                        {
                            var usd = booking.IsTotalUsd ? booking.MoneyLeft : booking.MoneyLeft / rate;
                            if (usd <= payUsd)
                            {
                                PaymentBooking(transactionGroup, booking, usd, 0, rate);
                                payUsd = payUsd - usd;
                            }
                        }
                        if (payUsd > 0 && bookings.Any(x => x.IsPaid == false))
                        {
                            var booking = bookings.OrderByDescending(b => b.CreatedDate).First();
                            PaymentBooking(transactionGroup, booking, payUsd, 0, rate);
                            litResultUsd.Text = string.Format("Thừa ${0:#,##0.##} sẽ chuyển cho booking {1}", payUsd,
                                booking.BookingIdOS);
                        }
                    }
                }

            }
            if (payVnd > 0)
            {
                var count = bookings.Count(b => b.IsPaid == false);

                foreach (Booking booking in bookings)
                {
                    if (!booking.IsPaid)
                    {
                        if (booking.IsTotalUsd)
                        {
                            var vnd = booking.MoneyLeft * rate;
                            if (vnd <= payVnd)
                            {
                                PaymentBooking(transactionGroup, booking, 0, vnd, rate);
                                payVnd = payVnd - vnd;
                                count--;
                            }
                        }
                        else
                        {
                            var vnd = booking.MoneyLeft;
                            if (vnd <= payVnd)
                            {
                                PaymentBooking(transactionGroup, booking, 0, vnd, rate);
                                payVnd = payVnd - vnd;
                                count--;
                            }
                        }
                    }
                }
                if (payVnd > 0)
                {
                    if (count > 0)
                    {
                        var booking = bookings.Where(b => b.IsPaid == false).OrderByDescending(b => b.MoneyLeft).FirstOrDefault();
                        if (booking != null)
                        {
                            PaymentBooking(transactionGroup, booking, 0, payVnd, rate);
                            phResult.Visible = true;
                            litResultVND.Text = string.Format("Số tiền {0:#,##0.##} không đủ thanh toán sẽ chuyển thanh toán cho booking {1}", payVnd, booking.BookingIdOS);
                        }
                    }
                    else
                    {
                        phResult.Visible = true;
                        var booking = bookings.Where(b => b.IsTotalUsd == false).OrderByDescending(b => b.CreatedDate).First();
                        PaymentBooking(transactionGroup, booking, 0, payVnd, rate);
                        litResultVND.Text = string.Format("Thừa {0:#,##0.##}đ sẽ chuyển cho booking {1}", payVnd, booking.BookingIdOS);
                    }
                }
            }
            LoadData();
            if (bookings.Any(b => b.IsPaid == false))
            {
                ShowSuccess(string.Format("Còn {0} booking chưa thanh toán", bookings.Count(b => b.IsPaid == false)));
            }
            else
            {
                ShowSuccess("Toàn bộ booking đã được thanh toán");
            }
            btnPayment.Enabled = false;
        }

        public void PaymentBooking(TransactionGroup transactionGroup, Booking bookingPay, double usd, double vnd, double rate)
        {
            double totalPaidUSD = 0;
            double totalPaidVND = 0;
            foreach (var t in bookingPay.Transactions)
            {
                if (t.TransactionType == Transaction.BOOKING)
                {
                    totalPaidUSD += t.USDAmount;
                    totalPaidVND += t.VNDAmount;
                }
            }

            var transaction = new Transaction()
            {
                Agency = bookingPay.Agency,
                Booking = bookingPay,
                CreatedBy = CurrentUser,
                CreatedDate = DateTime.Now,
                IsExpense = false,
                TransactionType = Transaction.BOOKING,
                USDAmount = usd,
                VNDAmount = vnd,
                TransactionGroup = transactionGroup
            };
            if (bookingPay.IsTotalUsd || usd > 0)
            {
                transaction.AppliedRate = rate;
            }
            BookingPaymentBLL.TransactionSaveOrUpdate(transaction);

            totalPaidUSD += transaction.USDAmount;
            totalPaidVND += transaction.VNDAmount;

            bookingPay.Paid = totalPaidUSD;
            bookingPay.PaidBase = totalPaidVND;
            if (bookingPay.IsTotalUsd || usd > 0)
            {
                bookingPay.CurrencyRate = rate;
            }
            if (bookingPay.MoneyLeft <= 0)
                bookingPay.IsPaid = true;

            bookingPay.PaidDate = DateTime.Now;

            bookingPay.AgencyConfirmed = false;
            BookingPaymentBLL.BookingSaveOrUpdate(bookingPay);
        }

        protected void btnPayment20_OnClick(object sender, EventArgs e)
        {
            var rate = Convert.ToDouble(txtRate.Text);
            var payUsd = 0.0;
            var payVnd = 0.0;
            if (!string.IsNullOrWhiteSpace(txtPaidUSD.Text))
            {
                payUsd = Convert.ToDouble(txtPaidUSD.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtPaidVND.Text))
            {
                payVnd = Convert.ToDouble(txtPaidVND.Text);
            }
            var transactionGroup = new TransactionGroup()
            {
                CreatedDate = DateTime.Now,
                CreatedBy = UserIdentity,
                USDAmount = payUsd,
                VNDAmount = payVnd,
                Note = txtNote.Text
            };
            Module.SaveOrUpdate(transactionGroup);
            var totalbooking = 0;
            int pageIndex = 0;

            PaymentListBooking20(ref pageIndex, ref payUsd, ref payVnd, transactionGroup, rate, ref totalbooking);

            if (payVnd > 0 || payUsd > 0)
            {
                litResultVND.Text = string.Format("Thừa {0:#,##0.##} usd, {1:#,##0.##}vnđ", payUsd, payVnd);
            }
            else
            {
                ShowSuccess(string.Format("Toàn bộ số tiền đã được thanh toán cho {0} booking", totalbooking));
            }

            btnPayment20.Enabled = false;
        }

        private void PaymentListBooking20(ref int pageIndex, ref double payUsd, ref double payVnd, TransactionGroup transactionGroup, double rate, ref int totalbooking)
        {
            var bookings = GetBooking20(pageIndex).Where(b => b.MoneyLeft > 0).ToList();
            pageIndex++;
            if (bookings.Count > 0)
            {
                if (payUsd > 0)
                {
                    double vnd = 0;
                    PaymentListBooking(ref payUsd, ref vnd, bookings, transactionGroup, rate, ref totalbooking);
                }
                if (payUsd > 0)
                {
                    PaymentListBooking20(ref pageIndex, ref payUsd, ref payVnd, transactionGroup, rate, ref totalbooking);
                }
                if (payVnd > 0)
                {
                    double usd = 0;
                    PaymentListBooking(ref usd, ref payVnd, bookings, transactionGroup, rate, ref totalbooking);
                }
                if (payVnd > 0)
                {
                    PaymentListBooking20(ref pageIndex, ref payUsd, ref payVnd, transactionGroup, rate, ref totalbooking);
                }
            }
        }

        private void PaymentListBooking(ref double payUsd, ref double payVnd, IList<Booking> bookings, TransactionGroup transactionGroup, double rate, ref int totalbooking)
        {
            if (payUsd > 0)
            {
                foreach (Booking booking in bookings)
                {
                    if (booking.IsTotalUsd)
                    {
                        var money = 0.0;
                        if (booking.MoneyLeft <= payUsd)
                            money = booking.MoneyLeft;
                        else money = payUsd;
                        if (money > 0)
                        {
                            PaymentBooking(transactionGroup, booking, money, 0, rate);
                            totalbooking += 1;
                            payUsd = payUsd - money;
                        }
                      
                    }
                    if (!booking.IsTotalUsd)
                    {
                        var money = Math.Round(booking.MoneyLeft / rate, 0) + 1;
                        if (payUsd < money)
                        {
                            money = payUsd;
                        }
                        if (money > 0)
                        {
                            PaymentBooking(transactionGroup, booking, money, 0, rate);
                            totalbooking += 1;
                            payUsd = payUsd - money;
                        }
                    }
                }
            }
            if (payVnd > 0)
            {

                foreach (Booking booking in bookings)
                {
                    if (!booking.IsPaid)
                    {
                        if (booking.IsTotalUsd)
                        {
                            var vnd = Math.Round(booking.MoneyLeft * rate, 0);
                            if (vnd > payVnd)
                            {
                                vnd = payVnd;
                            }
                            if (vnd > 0)
                            {
                                PaymentBooking(transactionGroup, booking, 0, vnd, rate);
                                totalbooking += 1;
                                payVnd = payVnd - vnd;
                            }

                        }
                        else
                        {
                            var vnd = booking.MoneyLeft;
                            if (vnd > payVnd)
                            {
                                vnd = payVnd;
                            }
                            if (vnd > 0)
                            {
                                PaymentBooking(transactionGroup, booking, 0, vnd, rate);
                                totalbooking += 1;
                                payVnd = payVnd - vnd;
                            }

                        }
                    }
                }

            }
        }

        private IList<Booking> GetBooking20(int pageIndex)
        {
            var to = DateTimeUtil.DateGetDefaultToDate();
            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                to = DateTime.ParseExact(Request.QueryString["t"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            var agencyId = -1;
            if (!string.IsNullOrEmpty(Request.QueryString["ai"]))
            {
                agencyId = Int32.Parse(Request.QueryString["ai"]);
            }
            return BookingPaymentBLL.GetBookingDebtReceivables(to, agencyId, 200, pageIndex).Where(b => b.MoneyLeft > 0).OrderBy(b => b.CreatedDate).ToList();
        }
    }
}