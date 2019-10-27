using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// báo cáo phiếu nhập
    /// </summary>
    public partial class IvImportReport : SailsAdminBase
    {
        #region --- PAGE EVENT ---

        private int sumQuantity;

        private double sumTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Quản lý báo cáo nhập";

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
            //ddlCruises.DataSource = Module.GetCruiseByUser(UserIdentity);
            //ddlCruises.DataTextField = "Name";
            //ddlCruises.DataValueField = "Id";
            //ddlCruises.DataBind();
            //ddlCruises.Items.Insert(0, new ListItem("-- Chọn tàu --", ""));
        }
        private void LoadYears()
        {
            drpYear.Items.Clear();
            drpMonthOfYear.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                drpYear.Items.Insert(i, new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
                drpMonthOfYear.Items.Insert(i, new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            drpYear.Items.Insert(0, new ListItem("", ""));
            drpMonthOfYear.Items.Insert(0, new ListItem("", ""));

        }
        #endregion

        #region --- CONTROL EVENT ---

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string path = string.Format("IvImportReport.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);

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

            //if (!string.IsNullOrEmpty(ddlCruises.SelectedValue))
            //{
            //    keyword += "&cruiseId=" + ddlCruises.SelectedValue;
            //}
            PageRedirect(path + keyword);
        }

        protected void rptProductList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is IvImportReportDate)
            {
                var ivReportDate = (IvImportReportDate)e.Item.DataItem;

                Label lblDate = e.Item.FindControl("lblDate") as Label;
                if (lblDate != null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["day"]))
                    {
                        lblDate.Text = ivReportDate.ImportDate.ToString("dd/MM/yyyy");
                    }
                    else if (!string.IsNullOrEmpty(Request.QueryString["month"]))
                    {
                        lblDate.Text = ivReportDate.ImportDate.ToString("dd/MM/yyyy");
                    }
                    else if (!string.IsNullOrEmpty(Request.QueryString["year"]))
                    {
                        lblDate.Text = ivReportDate.ImportDate.ToString("MM/yyyy");
                    }
                    else
                    {
                        lblDate.Text = ivReportDate.ImportDate.ToString("dd/MM/yyyy");
                    }
                    Label lblTotalPrice = e.Item.FindControl("lblTotal") as Label;
                    if (lblTotalPrice != null)
                    {
                        lblTotalPrice.Text = ivReportDate.Total.ToString("#,0.#");
                    }
                    sumTotal += ivReportDate.Total;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblSumTotal = e.Item.FindControl("lblSumTotal") as Label;
                if (lblSumTotal != null)
                {
                    lblSumTotal.Text = sumTotal.ToString("#,0.#");
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
            var list = Module.GetIvImportReport(Request.QueryString);
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
                drpMonth.SelectedValue = DateTime.Now.Month.ToString();
                drpMonthOfYear.SelectedValue = DateTime.Now.Year.ToString();
            }
            //if (!string.IsNullOrEmpty(Request["cruiseId"]))
            //{
            //    ddlCruises.SelectedValue = Request["cruiseId"];
            //}
        }

        #endregion
    }
}