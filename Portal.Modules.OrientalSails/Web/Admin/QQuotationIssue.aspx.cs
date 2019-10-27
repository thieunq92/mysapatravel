using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GemBox.Spreadsheet;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class QQuotationIssue : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(quotationSelector.Value))
            {
                ShowError("Select quotation !");
            }
            else
            {
                var quotation = Module.GetById<QQuotation>(Convert.ToInt32(quotationSelector.Value));
                ExcelFile excelFile = ExcelFile.Load(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/quotation.xlsx"));
                ExcelWorksheet sheet = excelFile.Worksheets[0];

                sheet.Cells["B1"].Value = quotation.GroupCruise.Name;
                sheet.Cells["A3"].Value = ddlAgentLevel.SelectedItem.Text;
                sheet.Cells["B3"].Value = String.Format("FROM: {0}", quotation.Validfrom.ToString("dd/MM/yyyy"));
                sheet.Cells["C3"].Value = String.Format("TO: {0}", quotation.Validto.ToString("dd/MM/yyyy"));

                int rowQ = 6;
                WriteSheetByTrip("2 NGÀY / 1 ĐÊM", 2, quotation, ref rowQ, ref sheet);
                WriteSheetByTrip("3 NGÀY / 2 ĐÊM", 3, quotation, ref rowQ, ref sheet);

                excelFile.Save(Response, string.Format("quotation_{0:dd-MM-yyyy}_{1:dd-MM-yyyy}_{2}_{3}.xlsx", quotation.Validfrom, quotation.Validto, ddlAgentLevel.SelectedItem.Text, quotation.GroupCruise.Name));

            }
        }
        private void WriteSheetByTrip(string tripName, int tripDay, QQuotation quotation, ref int rowQ, ref ExcelWorksheet sheet)
        {
            sheet.Cells[rowQ, 0].Value = tripName;
            rowQ += 2;
            //sheet.Cells[rowQ, 0].Value = quotation.GroupCruise.Name;
            //rowQ++;
            // display price room
            // display room type header
            var roomPrices = Module.GetGroupRoomPrice(quotation.GroupCruise.Id, ddlAgentLevel.SelectedValue, tripDay, quotation);
            sheet.Cells[rowQ, 0].Value = "LOẠI PHÒNG";
            sheet.Cells[rowQ, 1].Value = "Phòng đôi";
            sheet.Cells[rowQ, 2].Value = "Phòng đơn";
            sheet.Cells[rowQ, 3].Value = "Giường/Đệm phụ";
            sheet.Cells[rowQ, 4].Value = "Trẻ em";

            // display rooom class
            rowQ++;
            foreach (QGroupRomPrice roomPrice in roomPrices)
            {
                sheet.Cells[rowQ, 0].Value = roomPrice.RoomType;
                sheet.Cells[rowQ, 1].Value = string.Format("USD: {0:#,0.#} {2}VND: {1:#,0.#}", roomPrice.PriceDoubleUsd, roomPrice.PriceDoubleVnd, Environment.NewLine);
                sheet.Cells[rowQ, 2].Value = string.Format("USD: {0:#,0.#} {2}VND: {1:#,0.#}", roomPrice.PriceTwinUsd, roomPrice.PriceTwinVnd, Environment.NewLine);
                sheet.Cells[rowQ, 3].Value = string.Format("USD: {0:#,0.#} {2}VND: {1:#,0.#}", roomPrice.PriceExtraUsd, roomPrice.PriceExtraVnd, Environment.NewLine);
                sheet.Cells[rowQ, 4].Value = string.Format("USD: {0:#,0.#} {2}VND: {1:#,0.#}", roomPrice.PriceChildUsd, roomPrice.PriceChildVnd, Environment.NewLine);
                rowQ++;
            }
            rowQ++;
            // display price charter
            sheet.Cells[rowQ, 1].Value = "THUÊ TRỌN TÀU";
            rowQ++;

            var cruises = Module.CruiseGetAllByGroup(quotation.GroupCruise.Id);


            foreach (Cruise cruise in cruises)
            {
                // write cruise name
                //sheet.Cells[rowQ, 0].Value = string.Format("{0} {1} cabins", cruise.Name, cruise.Rooms.Count);
                var charterPrices = Module.GetCruiseCharterPrice(quotation.GroupCruise.Id, cruise, ddlAgentLevel.SelectedValue, tripDay, quotation);
                if (charterPrices.Count > 0)
                {
                    rowQ++;
                    var mergedRange = sheet.Cells.GetSubrange(string.Format("A{0}:A{1}", rowQ, rowQ + 1));
                    mergedRange.Merged = true;
                    mergedRange.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
                    mergedRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                    mergedRange.Value = string.Format("{0} {1} cabins", cruise.Name, cruise.Rooms.Count);
                    var idexRange = 1;
                    foreach (QCharterPrice charterPrice in charterPrices)
                    {
                        sheet.Cells[rowQ - 1, idexRange].Value = string.Format("{0}-{1} khách", charterPrice.Validfrom, charterPrice.Validto);
                        sheet.Cells[rowQ, idexRange].Value = string.Format("USD: {0:#,0.#} {2}VND: {1:#,0.#}", charterPrice.Priceusd, charterPrice.Priceusd, Environment.NewLine);
                    }
                    rowQ++;
                }
            }
            rowQ += 2;
        }

    }
}