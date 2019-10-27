using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// báo cáo phiếu xuất
    /// </summary>
    public partial class IvExportReport : SailsAdminBase
    {
        #region --- PAGE EVENT ---

        private int sumQuantity;

        private double sumTotal;
        private double sumPay;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Báo cáo doanh thu";

            if (!IsPostBack)
            {
                LoadCruise();
                LoadYears();

                LoadinfoSearch();
                BindrptProductList();

            }
        }

        private void LoadCruise()
        {
            ddlCruise.DataSource = Module.CruiseGetByUser(UserIdentity);
            ddlCruise.DataTextField = "Name";
            ddlCruise.DataValueField = "Id";
            ddlCruise.DataBind();
            ddlCruise.Items.Insert(0, new ListItem("-- Chọn tàu --", ""));
        }

        private void LoadYears()
        {
            drpYear.Items.Clear();
            drpMonthOfYear.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                drpYear.Items.Insert(i,
                    new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
                drpMonthOfYear.Items.Insert(i,
                    new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            drpYear.Items.Insert(0, new ListItem("", ""));
            drpMonthOfYear.Items.Insert(0, new ListItem("", ""));

        }

        #endregion

        #region --- CONTROL EVENT ---

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string path = string.Format("IvExportReport.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);

            string keyword = string.Empty;

            if (!string.IsNullOrEmpty(drpYear.SelectedValue))
            {
                DateTime firstTime = new DateTime(Convert.ToInt32(drpYear.SelectedValue), 1, 1);
                double time = firstTime.ToOADate();
                keyword = "&year=" + time;
            }


            if (!string.IsNullOrEmpty(drpMonth.SelectedValue) || !string.IsNullOrEmpty(drpMonthOfYear.SelectedValue))
            {
                if (!string.IsNullOrEmpty(drpMonth.SelectedValue) &&
                    !string.IsNullOrEmpty(drpMonthOfYear.SelectedValue))
                {
                    DateTime firstTime = new DateTime(Convert.ToInt32(drpMonthOfYear.SelectedValue),
                        Convert.ToInt32(drpMonth.SelectedValue), 1);
                    double time = firstTime.ToOADate();
                    keyword = "&month=" + time;
                }
                else
                {
                    ShowError("Phải chọn đủ tháng và năm");
                    return;
                }

            }
            if (!string.IsNullOrEmpty(txtFromDay.Text) || !string.IsNullOrEmpty(txtToDay.Text))
            {
                if (!string.IsNullOrEmpty(txtFromDay.Text) && !string.IsNullOrEmpty(txtToDay.Text))
                {
                    DateTime date = DateTime.ParseExact(txtFromDay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    double time = date.ToOADate();
                    keyword = "&day=1&fromday=" + time;

                    DateTime todate = DateTime.ParseExact(txtToDay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    double todatetime = todate.ToOADate();
                    keyword += "&today=" + todatetime;
                }
                else
                {
                    ShowError("Phải chọn từ ngày, đến ngày");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(ddlCruise.SelectedValue))
            {
                keyword += "&cruiseId=" + ddlCruise.SelectedValue;
            }
            if (!string.IsNullOrEmpty(ddlDebt.SelectedValue))
            {
                keyword += "&debt=" + ddlDebt.SelectedValue;
            }
            PageRedirect(path + keyword);
        }

        protected void rptProductList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is IvExportReportDate)
            {
                var ivReportDate = (IvExportReportDate)e.Item.DataItem;

                var hplDate = e.Item.FindControl("hplDate") as HyperLink;
                if (hplDate != null)
                {
                    var from = DateTime.Now;
                    var to = DateTime.Now;
                    if (!string.IsNullOrEmpty(Request.QueryString["day"]))
                    {
                        hplDate.Text = ivReportDate.ExportDate.ToString("dd/MM/yyyy");
                        from = ivReportDate.ExportDate;
                        to = ivReportDate.ExportDate;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["month"]))
                    {
                        hplDate.Text = ivReportDate.ExportDate.ToString("dd/MM/yyyy");
                        from = ivReportDate.ExportDate;
                        to = ivReportDate.ExportDate;
                    }
                    else if (!string.IsNullOrEmpty(Request.QueryString["year"]))
                    {
                        hplDate.Text = ivReportDate.ExportDate.ToString("MM/yyyy");
                        from = new DateTime(ivReportDate.ExportDate.Year, ivReportDate.ExportDate.Month, 1);
                        to = new DateTime(ivReportDate.ExportDate.Year, ivReportDate.ExportDate.Month, 1).AddMonths(1)
                            .AddDays(-1);
                    }
                    else
                    {
                        hplDate.Text = ivReportDate.ExportDate.ToString("dd/MM/yyyy");
                    }
                    var link = "/Modules/Sails/Admin/IvProductExportList.aspx?NodeId=1&SectionId=15";
                    link += "&fromDate=" + from.ToOADate();
                    link += "&toDate=" + to.ToOADate();
                    hplDate.NavigateUrl = link;
                    Label lblTotalPrice = e.Item.FindControl("lblTotal") as Label;
                    if (lblTotalPrice != null)
                    {
                        lblTotalPrice.Text = ivReportDate.Total.ToString("#,0.#");
                    }
                    Label lblPay = e.Item.FindControl("lblPay") as Label;
                    if (lblPay != null)
                    {
                        lblPay.Text = ivReportDate.Pay.ToString("#,0.#");
                    }
                    Label lblAverage = e.Item.FindControl("lblAverage") as Label;
                    if (lblAverage != null)
                    {
                        if (ivReportDate.TotalCustomer > 0 && ivReportDate.Pay > 0)
                        {
                            lblAverage.Text = (ivReportDate.Pay / ivReportDate.TotalCustomer).ToString("#,0.#");
                        }
                    }
                    Label lblCustomer = e.Item.FindControl("lblCustomer") as Label;
                    if (lblCustomer != null)
                    {
                        lblCustomer.Text = ivReportDate.TotalCustomer.ToString();
                    }
                    sumTotal += ivReportDate.Total;
                    sumPay += ivReportDate.Pay;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblSumTotal = e.Item.FindControl("lblSumTotal") as Label;
                if (lblSumTotal != null)
                {
                    lblSumTotal.Text = sumTotal.ToString("#,0.#");
                }
                Label lblSumPay = e.Item.FindControl("lblSumPay") as Label;
                if (lblSumPay != null)
                {
                    lblSumPay.Text = sumPay.ToString("#,0.#");
                }
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            double z = 0;


        }

        #endregion

        #region --- PRIVATE METHOD ---

        private void BindrptProductList()
        {
            int count;
            var list = Module.GetIvExportReport(Request.QueryString);
            rptProductList.DataSource = list;
            rptProductList.DataBind();
        }

        private void LoadinfoSearch()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["day"]))
            {
                double time = Convert.ToDouble(Request.QueryString["fromday"]);
                DateTime timeConvert = DateTime.FromOADate(time);
                txtFromDay.Text = timeConvert.ToString("dd/MM/yyyy");

                double today = Convert.ToDouble(Request.QueryString["today"]);
                DateTime timetodayConvert = DateTime.FromOADate(today);
                txtToDay.Text = timetodayConvert.ToString("dd/MM/yyyy");
            }

            else if (!string.IsNullOrEmpty(Request.QueryString["month"]))
            {
                double time = Convert.ToDouble(Request.QueryString["month"]);
                DateTime timeConvert = DateTime.FromOADate(time);

                drpMonth.SelectedValue = timeConvert.Month.ToString();
                drpMonthOfYear.SelectedValue = timeConvert.Year.ToString();
            }

            else if (!string.IsNullOrEmpty(Request.QueryString["year"]))
            {
                double time = Convert.ToDouble(Request.QueryString["year"]);
                DateTime timeConvert = DateTime.FromOADate(time);

                drpYear.SelectedValue = timeConvert.Year.ToString();
            }
            else
            {
                txtFromDay.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                txtToDay.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            }
            if (!string.IsNullOrEmpty(Request["cruiseId"]))
            {
                ddlCruise.SelectedValue = Request["cruiseId"];
            }
            if (!string.IsNullOrEmpty(Request["debt"]))
            {
                ddlDebt.SelectedValue = Request["debt"];
            }
        }

        #endregion
    }
}