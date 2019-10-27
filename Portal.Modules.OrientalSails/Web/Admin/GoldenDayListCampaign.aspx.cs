using Aspose.Words;
using Aspose.Words.Tables;
using OfficeOpenXml;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Admin.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class GoldenDayListCampaign : System.Web.UI.Page
    {
        private GoldenDayListCampaignBLL goldenDayListCampaignBLL;
        public GoldenDayListCampaignBLL GoldenDayListCampaignBLL
        {
            get
            {
                if (goldenDayListCampaignBLL == null)
                {
                    goldenDayListCampaignBLL = new GoldenDayListCampaignBLL();
                }
                return goldenDayListCampaignBLL;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int count = 0;
                rptCampaign.DataSource = GoldenDayListCampaignBLL.CampaignGetAllPaged(pagerCampaign.PageSize,
                    pagerCampaign.CurrentPageIndex, out count);
                pagerCampaign.AllowCustomPaging = true;
                pagerCampaign.VirtualItemCount = count;
                rptCampaign.DataBind();

            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (goldenDayListCampaignBLL != null)
            {
                goldenDayListCampaignBLL.Dispose();
                goldenDayListCampaignBLL = null;
            }
        }
        public string GetDateApplied(Campaign campaign)
        {
            return String.Join("<br/>", campaign.GoldenDays.OrderBy(x => x.Date).Select(gd =>
                "<a href='' data-toggle='tooltip' title ='" + gd.Policy + "'>" + gd.Date.ToString("dd/MM/yyyy") + "</a>"));
        }
        public IEnumerable<Booking> GetNewBookings(Campaign campaign)
        {
            return GoldenDayListCampaignBLL.BookingGetAllNewBookingsByCampaign(campaign);
        }
        public string GetNoOfNewBooking(Campaign campaign)
        {
            var bookings = GoldenDayListCampaignBLL.BookingGetAllNewBookingsByCampaign(campaign);
            return bookings.Count().ToString();
        }

        protected void rptCampaign_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Export")
            {
                var campaignId = Convert.ToInt32(e.CommandArgument);
                var campaign = GoldenDayListCampaignBLL.CampaginGetById(campaignId);
                MemoryStream mem = new MemoryStream();
                var document = new Aspose.Words.Document(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/Golden_Days_Form.doc"));
                DocumentBuilder builder = new DocumentBuilder(document);
                document.Range.Replace("{Month}", campaign.Month.ToString("0#"), false, false);
                document.Range.Replace("{Year}", campaign.Year.ToString(), false, false);
                NodeCollection tables = document.GetChildNodes(NodeType.Table, true);
                var table = tables[0] as Aspose.Words.Tables.Table;
                for (var i = table.Rows.Count - 1; i > 1; i--)
                {
                    table.Rows.RemoveAt(i); 
                }
                
                foreach (var goldenDay in campaign.GoldenDays)
                {
                    var cloneOfRow = (Row)table.Rows[1].Clone(true);
                    cloneOfRow.Cells[0].RemoveAllChildren();
                    cloneOfRow.Cells[0].AppendChild(new Paragraph(document));
                    cloneOfRow.Cells[0].FirstParagraph.AppendChild(new Run(document, goldenDay.Date.ToString("dd MMM")));
                    cloneOfRow.Cells[1].RemoveAllChildren();
                    cloneOfRow.Cells[1].AppendChild(new Paragraph(document));
                    cloneOfRow.Cells[1].FirstParagraph.AppendChild(new Run(document, goldenDay.Policy != null ? goldenDay.Policy.Replace("\n", ControlChar.LineBreak) : ""));
                    table.Rows.Insert(table.Rows.Count, cloneOfRow);
                }
                table.Rows.RemoveAt(1);
                document.Save(mem, Aspose.Words.SaveFormat.Docx);
                Response.Clear();
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                var fileName = string.Format("\"{0}.docx\"", campaign.Name);
                Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
                mem.Position = 0;
                byte[] buffer = mem.ToArray();
                Response.BinaryWrite(buffer);
                Response.End();
            }
            if (e.CommandName == "Delete")
            {
                var campaignId = 0;
                try
                {
                    campaignId = Convert.ToInt32(e.CommandArgument);
                }
                catch { }
                var campaign = GoldenDayListCampaignBLL.CampaginGetById(campaignId);
                GoldenDayListCampaignBLL.CampaignDelete(campaign);
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void rptBooking_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           
        }

        protected void rptCampaign_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var campaign = (Campaign)e.Item.DataItem;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lbtView = (LinkButton)e.Item.FindControl("lbtView");
                lbtView.OnClientClick = "$('#" + hidCampaignId.ClientID + "').val(" + campaign.Id + ");$get('" + btnTrigger.ClientID + "').click();return false;";
            }
        }

        protected void btnTrigger_Click(object sender, EventArgs e)
        {
            plhTableBooking.Visible = true;
            var campaignId = Int32.Parse(hidCampaignId.Value);
            var campaign = GoldenDayListCampaignBLL.CampaginGetById(campaignId);
            var bookings = new List<Booking>();
            bookings = GetNewBookings(campaign).ToList();
            rptBooking.DataSource = bookings;
            rptBooking.DataBind();
        }
    }
}