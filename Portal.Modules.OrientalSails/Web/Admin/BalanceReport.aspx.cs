using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.BusinessLogic;
using System.Globalization;
using System.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BalanceReport : Page
    {
        private SailsModule module = SailsModule.GetInstance();
        private BalanceReportBLL balanceReportBLL = new BalanceReportBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            var fromDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            try
            {
                fromDate = DateTime.ParseExact(Request.QueryString["fd"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            try
            {
                toDate = DateTime.ParseExact(Request.QueryString["td"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            if (!IsPostBack)
            {
                txtTuNgay.Text = fromDate.ToString("dd/MM/yyyy");
                txtDenNgay.Text = toDate.ToString("dd/MM/yyyy");
            }
            var bookings = this.balanceReportBLL.BookingGetAllFromDateToDate(fromDate,toDate);
            rptBooking.DataSource = bookings;
            rptBooking.DataBind();
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (balanceReportBLL != null)
            {
                balanceReportBLL.Dispose();
                balanceReportBLL = null;
            }
        }

        protected void btnHienThi_Click(object sender, EventArgs e)
        {
            Response.Redirect("BalanceReport.aspx?fd=" + txtTuNgay.Text + "&td=" + txtDenNgay.Text);
        }
    }
}