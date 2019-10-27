using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Web.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class GoldenDayCreateCampaign : System.Web.UI.Page
    {
        private DashBoardBLL dashBoardBLL;
        private IEnumerable<RoomsAvaiableDTO> RoomsAvaiableDTO { get; set; }
        public DashBoardBLL DashBoardBLL
        {
            get
            {
                if (dashBoardBLL == null)
                {
                    dashBoardBLL = new DashBoardBLL();
                }
                return dashBoardBLL;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlMonth.Items.AddRange(Enumerable.Range(1, 12).Select(x => new ListItem() { Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(x), Value = x.ToString() }).ToArray());
                ddlYear.Items.AddRange(Enumerable.Range(DateTime.Now.Year, 100).Select(x => new ListItem() { Text = x.ToString(), Value = x.ToString() }).ToArray());
                ddlMonth.SelectedValue = (DateTime.Today.Month + 1).ToString();
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (dashBoardBLL != null)
            {
                dashBoardBLL.Dispose();
                dashBoardBLL = null;
            }
        }
        public void LoadCruiseAvaialability()
        {
            var month = Int32.Parse(Request.Params["month"]);
            var year = Int32.Parse(Request.Params["year"]);
            var from = new DateTime(year, month, 1);
            var to = from.AddMonths(1).AddDays(-1);
            var dateRange = new List<DateTime>();
            var current = from;
            while (current <= to)
            {
                dateRange.Add(current);
                current = current.AddDays(1);
            }
            RoomsAvaiableDTO = DashBoardBLL.CruiseGetRoomsAvaiableInDateRange(from, to);
            rptCruiseAvaibility.DataSource = dateRange;
            rptCruiseAvaibility.DataBind();
        }

        protected void rptCruiseAvaibility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cruises = DashBoardBLL.CruiseGetAll();

            if (e.Item.ItemType == ListItemType.Header)
            {
                var ltrHeader = (Literal)e.Item.FindControl("ltrHeader");
                var header = "";
                foreach (var cruise in cruises)
                {
                    header += "<th style='width:10%'>" + (cruise.Code != null ? cruise.Code.ToUpper() : "") + "</th>";
                }
                ltrHeader.Text = header;
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var date = (DateTime)e.Item.DataItem;
                var ltrRow = (Literal)e.Item.FindControl("ltrRow");
                var rowItems = "<td>" +
                                    "<a href=BookingReport.aspx?NodeId=1&SectionId=15&date=" + date.ToString("dd/MM/yyyy") + ">" + date.ToString("dd/MM/yyyy") +
                                    "</a>" +
                               "</td>";
                foreach (var cruise in cruises)
                {
                    var numberOfRoomAvaiable = RoomsAvaiableDTO.Where(x => DateTimeUtil.EqualsUpToSeconds(x.Date, date) && x.CruiseId == cruise.Id).First().NoRAvaiable;
                    var numberOfRoom = RoomsAvaiableDTO.Where(x => DateTimeUtil.EqualsUpToSeconds(x.Date, date) && x.CruiseId == cruise.Id).First().TotalRoom;
                    double percentOfRoomAvailable = (numberOfRoomAvaiable / numberOfRoom);
                    var className = "";
                    if (percentOfRoomAvailable == 1)
                    {
                        className = "td__no-room-using";
                    }
                    if (numberOfRoomAvaiable > 0)
                    {
                        rowItems += "<td class='" + className + "'>" + numberOfRoomAvaiable + "</td>";
                    }
                    else
                    {
                        rowItems += "<td class='td__not-avaiable'>" + numberOfRoomAvaiable + "</td>";
                    }
                };
                rowItems += "<td style='width:10%;border:none'>" +
                                "<button type='button' class='btn btn-primary' ng-click=\"add('" + date.ToString("dd/MM/yyyy") + "',null)\"" +
                                "ng-hide=\"isSelected('" + date.ToString("dd/MM/yyyy") + "')\">Select</button>" +
                            "</td>";
                var row = string.Format("<tr>{0}</tr>", rowItems);
                ltrRow.Text = row;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            LoadCruiseAvaialability();
        }
    }
}