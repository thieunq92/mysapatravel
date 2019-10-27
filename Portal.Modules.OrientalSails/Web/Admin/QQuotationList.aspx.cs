using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using GemBox.Spreadsheet;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class QQuotationList : SailsAdminBasePage
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            pagerProduct.AllowCustomPaging = true;
            pagerProduct.PageSize = 20;
            if (!IsPostBack)
            {
                GetQuotation();
            }
        }

        private void GetQuotation()
        {
            int count = 0;
            rptQuotation.DataSource = Module.GetQQuotation(pagerProduct.PageSize, pagerProduct.CurrentPageIndex, out count);
            rptQuotation.DataBind();
            pagerProduct.VirtualItemCount = count;
        }

        protected void rptQuotation_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var quotationId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "export")
            {
                var quotation = Module.GetById<QQuotation>(quotationId);
                if (quotation != null)
                {
                    ExportQuotation(quotation);
                }
            }
        }

        private void ExportQuotation(QQuotation quotation)
        {
            ExcelFile excelFile = ExcelFile.Load(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/quotation.xlsx"));
            ExcelWorksheet sheet = excelFile.Worksheets[0];

            //sheet.Cells["A3"].Value = quotation.QAgentlevel.Name;
            sheet.Cells["B3"].Value = String.Format("FROM: {0}", quotation.Validfrom.ToString("dd/MM/yyyy"));
            sheet.Cells["C3"].Value = String.Format("TO: {0}", quotation.Validto.ToString("dd/MM/yyyy"));

            int rowQ = 6;
            WriteSheetByTrip("2 NGÀY / 1 ĐÊM", 2, quotation, ref rowQ, ref sheet);
            WriteSheetByTrip("3 NGÀY / 2 ĐÊM", 3, quotation, ref rowQ, ref sheet);
            //excelFile.Save(Response, string.Format("quotation_{0}_{1:dd-MM-yyyy}_{2:dd-MM-yyyy}.xlsx", quotation.QAgentlevel.Name, quotation.Fromdate, quotation.Todate));
        }

        private void WriteSheetByTrip(string tripName, int tripDay, QQuotation quotation, ref int rowQ, ref ExcelWorksheet sheet)
        {
            sheet.Cells[rowQ, 0].Value = tripName;
            rowQ += 2;
            var groups = Module.GetCruiseGroup();
            foreach (QCruiseGroup group in groups)
            {
                sheet.Cells[rowQ, 0].Value = group.Name;
                rowQ++;
                // display price room
                // display room type header
                var roomTypes = Module.GetRoomType(group);
                sheet.Cells[rowQ, 0].Value = "LOẠI PHÒNG";
                for (int i = 0; i < roomTypes.Count; i++)
                {
                    sheet.Cells[rowQ, i + 1].Value = roomTypes[i].Name;
                }
                // display rooom class
                rowQ++;
                var roomClasses = Module.GetRoomClass(group);
                foreach (QRoomClass roomClass in roomClasses)
                {
                    sheet.Cells[rowQ, 0].Value = roomClass.Roomclass;
                    var idextype = 1;
                    foreach (QRoomType roomType in roomTypes)
                    {
                        var roomPrice = Module.GetRoomPrice(quotation, roomType.Group, roomClass, roomType, tripDay);
                        if (roomPrice != null) sheet.Cells[rowQ, idextype].Value = string.Format("USD: {0:#,0.#} {2}VND: {1:#,0.#}", roomPrice.Priceusd, roomPrice.Pricevnd, Environment.NewLine);
                        idextype++;
                    }
                    rowQ++;
                }
                rowQ++;
                // display price charter
                sheet.Cells[rowQ, 1].Value = "THUÊ TRỌN TÀU";
                rowQ++;

                var cruiseCharterRanges = Module.GetCruiseCharterRange(group);
                var cruises = new List<QCruiseCharterRange>();
                foreach (var charterRange in cruiseCharterRanges)
                {
                    if (cruises.All(c => c.Cruise != charterRange.Cruise))
                    {
                        cruises.Add(charterRange);
                    }
                }

                foreach (QCruiseCharterRange cruseCharter in cruises)
                {
                    // write cruise name
                    sheet.Cells[rowQ, 0].Value = string.Format("{0} {1} cabins", cruseCharter.Cruise.Name, cruseCharter.Cruise.Rooms.Count);
                    var rowQ0 = rowQ;
                    var listRanges = Module.GetCruiseCharterRange(cruseCharter.Group).Where(c => c.Cruise == cruseCharter.Cruise).ToList();
                    // write header 
                    if (cruseCharter.CharterRangeConfig != null)
                    {
                        for (int j = 0; j < listRanges.Count; j++)
                        {
                            sheet.Cells[rowQ, j + 1].Value = listRanges[j].CharterRangeConfig.Name;
                        }
                        rowQ++;

                        var mergedRange = sheet.Cells.GetSubrange(string.Format("A{0}:A{1}", rowQ0 + 1, rowQ + 1));
                        mergedRange.Merged = true;
                        mergedRange.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
                        mergedRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

                    }
                    var idexRange = 1;
                    foreach (QCruiseCharterRange charterRange in listRanges)
                    {
                        var rangePrice = Module.GetCharterRangePrice(quotation, charterRange, tripDay);
                        if (rangePrice != null) sheet.Cells[rowQ, idexRange].Value = string.Format("USD: {0:#,0.#} {2}VND: {1:#,0.#}", rangePrice.Priceusd, rangePrice.Pricevnd, Environment.NewLine);
                        idexRange++;
                    }
                    rowQ++;
                }
                rowQ += 2;
            }
            rowQ += 2;
        }

        protected void rptQuotation_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is QQuotation)
            {
                QQuotation quotation = (QQuotation)e.Item.DataItem;
                HtmlAnchor aName = e.Item.FindControl("aName") as HtmlAnchor;
                if (aName != null)
                {
                    var name = string.Format("{0:dd/mm/yyyy}-{1:dd/mm/yyyy} {2}", quotation.Validfrom, quotation.Validto, quotation.GroupCruise.Name);
                    aName.InnerHtml = name;
                    aName.Attributes.CssStyle.Add("cursor", "pointer");

                    string script = string.Format("Done('{0}','{1}')", name.Replace("'", @"\'"), quotation.Id);
                    aName.Attributes.Add("onclick", script);
                }
            }
        }
    }
}