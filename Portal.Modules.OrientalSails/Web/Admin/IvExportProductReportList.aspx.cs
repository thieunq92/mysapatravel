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
    /// BÁO CÁO SẢN PHẨM XUẤT THEO THỜI GIAN
    /// </summary>
    public partial class IvExportProductReportList : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "BÁO CÁO SẢN PHẨM XUẤT THEO THỜI GIAN";

            if (!IsPostBack)
            {
                LoadCruise();
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

        #region --- CONTROL EVENT ---

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string path = string.Format("IvExportProductReportList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);

            string keyword = string.Empty;


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
            PageRedirect(path + keyword);
        }

        protected void rptProductList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.DataItem is IvExportReportDate)
            //{
            //    var ivReportDate = (IvExportReportDate)e.Item.DataItem;

            //    Label lblDate = e.Item.FindControl("lblDate") as Label;
            //    if (lblDate != null)
            //    {
            //        if (!string.IsNullOrEmpty(Request.QueryString["day"]))
            //        {
            //            lblDate.Text = ivReportDate.ExportDate.ToString("dd/MM/yyyy");
            //        }
            //        else if (!string.IsNullOrEmpty(Request.QueryString["month"]))
            //        {
            //            lblDate.Text = ivReportDate.ExportDate.ToString("dd/MM/yyyy");
            //        }
            //        else if (!string.IsNullOrEmpty(Request.QueryString["year"]))
            //        {
            //            lblDate.Text = ivReportDate.ExportDate.ToString("MM/yyyy");
            //        }

            //        Label lblTotalPrice = e.Item.FindControl("lblTotal") as Label;
            //        if (lblTotalPrice != null)
            //        {
            //            lblTotalPrice.Text = ivReportDate.Total.ToString("#,0.#");
            //        }
            //    }
            //}
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
            var list = Module.GetExportProductReportDates(Request.QueryString);
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
            else
            {
                txtFromDay.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                txtToDay.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            }
            if (!string.IsNullOrEmpty(Request["cruiseId"]))
            {
                ddlCruise.SelectedValue = Request["cruiseId"];
            }
        }

        #endregion
    }
}