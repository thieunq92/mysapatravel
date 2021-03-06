using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using GemBox.Spreadsheet;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class PayableList : SailsAdminBase
    {
        private double _total;
        private double _paid;
        private double _payable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSuppliers();

                BindData();


                DateTime from;
                DateTime to;

                if (Request.QueryString["from"] == null && Request.QueryString["mode"] == null)
                {
                    from = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
                    to = from.AddMonths(1).AddDays(-1);
                    PageRedirect(string.Format("PayableList.aspx?NodeId={0}&SectionId={1}&from={2}&to={3}", Node.Id, Section.Id, from.ToOADate(), to.ToOADate()));
                    return;
                }

                from = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["from"]));
                to = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["to"]));
                
                Agency agency = null;
                CostType type = null;
                IList expenseServices = null;

                if (Request.QueryString["supplierid"] != null)
                {
                    agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["supplierid"]));
                }

                if (Request.QueryString["mode"] != "all")
                {
                    expenseServices = Module.ExpenseServiceGet(null, from, to, agency, type);
                }
                else
                {
                    expenseServices = Module.ExpenseServiceGet(null, null, null, agency, type);
                }

                var costTypes = new List<CostType>();
                foreach (ExpenseService expenseService in expenseServices)
                {
                    costTypes.Add(expenseService.Type);
                }
                var uniqueCostTypes = new HashSet<CostType>(costTypes);
                var uniqueCostTypesList = new List<CostType>();
                foreach (CostType costType in uniqueCostTypes) {
                    if (costType.Name.ToLower() == "hai phong cruise")
                    {
                        uniqueCostTypesList.Insert(0, costType);
                        continue;
                    }

                    uniqueCostTypesList.Add(costType);
                }

                rptCostTypeTabHeader.DataSource = uniqueCostTypesList;
                rptCostTypeTabHeader.DataBind();

                rptPanel.DataSource = uniqueCostTypes;
                rptPanel.DataBind();

            }
        }

        protected void BindSuppliers()
        {
            ddlSupplier.DataSource = Module.SupplierGetAll();
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataValueField = "Id";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, "-- Select supplier --");
            foreach (Agency agency in Module.GuidesGetAll())
            {
                ddlSupplier.Items.Add(new ListItem(agency.Name, agency.Id.ToString()));
            }
        }

        protected void BindData()
        {
            txtFrom.Text = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["from"])).ToString("dd/MM/yyyy");
            txtTo.Text = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["to"])).ToString("dd/MM/yyyy");

            if (Request.QueryString["supplierid"] != null)
            {
                ddlSupplier.SelectedValue = Request.QueryString["supplierid"];
            }

        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            string query = string.Empty;
            query += string.Format("&from={0}&to={1}",
                                   DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).
                                       ToOADate(),
                                   DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToOADate());
            if (ddlSupplier.SelectedIndex > 0)
            {
                query += "&supplierid=" + ddlSupplier.SelectedValue;
            }

            PageRedirect(string.Format("PayableList.aspx?NodeId={0}&SectionId={1}{2}", Node.Id, Section.Id, query));
        }

        protected void rptPanel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DateTime from;
            DateTime to;

            if (Request.QueryString["from"] == null && Request.QueryString["mode"] == null)
            {
                from = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
                to = from.AddMonths(1).AddDays(-1);
                PageRedirect(string.Format("PayableList.aspx?NodeId={0}&SectionId={1}&from={2}&to={3}", Node.Id, Section.Id, from.ToOADate(), to.ToOADate()));
                return;
            }

            from = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["from"]));
            to = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["to"]));

            Agency agency = null;
            CostType type = e.Item.DataItem as CostType;
            IList expenseServices = null;

            if (Request.QueryString["supplierid"] != null)
            {
                agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["supplierid"]));
            }

            if (Request.QueryString["mode"] != "all")
            {
                expenseServices = Module.ExpenseServiceGet(null, from, to, agency, type);
            }
            else
            {
                expenseServices = Module.ExpenseServiceGet(null, null, null, agency, type);
            }

            var repeaterExpenseServices = e.Item.FindControl("rptExpenseServices") as Repeater;
            if (repeaterExpenseServices == null)
                return;
            repeaterExpenseServices.DataSource = expenseServices;
            repeaterExpenseServices.DataBind();
        }

        CostType costType = null;
        protected void rptExpenseServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
      
            if (e.Item.DataItem is ExpenseService)
            {
                ExpenseService service = (ExpenseService)e.Item.DataItem;
                costType = service.Type;
                if (service.Cost == 0 || service.Type.IsMonthly || service.Type.IsYearly)
                {
                    e.Item.Visible = false;
                    return;
                }
                HyperLink hplDate = e.Item.FindControl("hplDate") as HyperLink;
                if (hplDate != null)
                {
                    hplDate.Text = service.Expense.Date.ToString("dd/MM/yyyy");
                }

                HyperLink hplPartner = e.Item.FindControl("hplPartner") as HyperLink;
                if (hplPartner != null)
                {
                    if (service.Supplier != null)
                    {
                        hplPartner.Text = service.Supplier.Name;
                    }
                }

                HyperLink hplService = e.Item.FindControl("hplService") as HyperLink;
                if (hplService != null)
                {
                    hplService.Text = service.Type.Name;
                }

                Literal litTotal = e.Item.FindControl("litTotal") as Literal;
                if (litTotal != null)
                {
                    litTotal.Text = service.Cost.ToString("#,0.#");
                    _total += service.Cost;
                }

                Literal litPaid = e.Item.FindControl("litPaid") as Literal;
                if (litPaid != null)
                {
                    litPaid.Text = service.Paid.ToString("#,0.#");
                    _paid += service.Paid;
                }

                Literal litPayable = e.Item.FindControl("litPayable") as Literal;
                if (litPayable != null)
                {
                    litPayable.Text = (service.Cost - service.Paid).ToString("#,0.#");
                    _payable += service.Cost - service.Paid;
                }
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                var rptExpenseServices = (Repeater)sender;
                var expenseServices = (IList)rptExpenseServices.DataSource;
                var total = 0.0;
                var paid = 0.0;
                var payable = 0.0;
                foreach (ExpenseService expenseService in expenseServices)
                {
                    total += expenseService.Cost;
                    paid += expenseService.Paid;
                    payable += (expenseService.Cost - expenseService.Paid);
                }
                Literal litTotal = e.Item.FindControl("litTotal") as Literal;
                if (litTotal != null)
                {
                    litTotal.Text = total.ToString("#,0.#");
                }
                
                Literal litPaid = e.Item.FindControl("litPaid") as Literal;
                if (litPaid != null)
                {
                    litPaid.Text = paid.ToString("#,0.#");
                }

                Literal litPayable = e.Item.FindControl("litPayable") as Literal;
                if (litPayable != null)
                {
                    litPayable.Text = payable.ToString("#,0.#");
                }

                Button btnPayAll = e.Item.FindControl("btnPay") as Button;
                if (btnPayAll != null) {
                    btnPayAll.CommandArgument = costType.Id.ToString();
                }
            }
        }

        protected void rptExpenseServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Footer)
            {
                ExpenseService service = Module.ExpenseServiceGetById(Convert.ToInt32(e.CommandArgument));
                TextBox txtPay = (TextBox)e.Item.FindControl("txtPay");
                if (!string.IsNullOrEmpty(txtPay.Text))
                {
                    service.Paid = Convert.ToDouble(txtPay.Text);
                }
                else
                {
                    service.Paid = service.Cost;
                }
                Module.SaveOrUpdate(service);

                DateTime from;
                DateTime to;
                if (Request.QueryString["from"] == null && Request.QueryString["mode"] == null)
                {
                    from = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
                    to = from.AddMonths(1).AddDays(-1);
                    PageRedirect(string.Format("PayableList.aspx?NodeId={0}&SectionId={1}&from={2}&to={3}", Node.Id, Section.Id, from.ToOADate(), to.ToOADate()));
                    return;
                }

                from = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["from"]));
                to = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["to"]));

                Agency agency = null;
                CostType type = null;
                IList expenseServices = null;

                if (Request.QueryString["supplierid"] != null)
                {
                    agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["supplierid"]));
                }

                if (Request.QueryString["mode"] != "all")
                {
                    expenseServices = Module.ExpenseServiceGet(null, from, to, agency, type);
                }
                else
                {
                    expenseServices = Module.ExpenseServiceGet(null, null, null, agency, type);
                }

                var costTypes = new List<CostType>();
                foreach (ExpenseService expenseService in expenseServices)
                {
                    costTypes.Add(expenseService.Type);
                }
                 var uniqueCostTypes = new HashSet<CostType>(costTypes);
                rptPanel.DataSource = uniqueCostTypes;
                rptPanel.DataBind();
            }
            else
            {
                var type = Module.CostTypeGetById(Convert.ToInt32(e.CommandArgument));

                DateTime from;
                DateTime to;
                if (Request.QueryString["from"] == null && Request.QueryString["mode"] == null)
                {
                    from = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
                    to = from.AddMonths(1).AddDays(-1);
                    PageRedirect(string.Format("PayableList.aspx?NodeId={0}&SectionId={1}&from={2}&to={3}", Node.Id, Section.Id, from.ToOADate(), to.ToOADate()));
                    return;
                }

                from = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["from"]));
                to = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["to"]));

                Agency agency = null;
                IList expenseServices = null;

                if (Request.QueryString["supplierid"] != null)
                {
                    agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["supplierid"]));
                }

                if (Request.QueryString["mode"] != "all")
                {
                    expenseServices = Module.ExpenseServiceGet(null, from, to, agency, type);
                }
                else
                {
                    expenseServices = Module.ExpenseServiceGet(null, null, null, agency, type);
                }

                foreach (ExpenseService service in expenseServices)
                {
                    service.Paid = service.Cost;
                    Module.SaveOrUpdate(service);
                }

                var costTypes = new List<CostType>();
                foreach (ExpenseService expenseService in expenseServices)
                {
                    costTypes.Add(expenseService.Type);
                }
                var uniqueCostTypes = new HashSet<CostType>(costTypes);
                rptPanel.DataSource = uniqueCostTypes;
                rptPanel.DataBind();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExcelFile excelFile = new ExcelFile();
            excelFile.LoadXls(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/MultiService.xls"));

            ExcelWorksheet sheet;

            #region -- Get data --
            DateTime from = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["from"]));
            DateTime to = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["to"]));

            Agency agency = null;
            CostType type = null;

            if (Request.QueryString["supplierid"] != null)
            {
                agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["supplierid"]));
            }

            if (Request.QueryString["costtype"] != null)
            {
                type = Module.CostTypeGetById(Convert.ToInt32(Request.QueryString["costtype"]));
            }
            IList data = Module.ExpenseServiceGet(null, from, to, agency, type);
            #endregion

            #region -- Các thông tin chung --
            string time;
            if (from.AddMonths(1).AddDays(-1) == to)
            {
                time = from.ToString("MMM - yyyy");
            }
            else
            {
                time = string.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", from, to);
            }
            #endregion

            #region -- Get Supplier list --
            IList agencies = new ArrayList();
            if (agency != null)
            {
                sheet = excelFile.Worksheets[0];
                IList costTypes = new ArrayList();
                foreach (ExpenseService service in data)
                {
                    if (!costTypes.Contains(service.Type))
                    {
                        costTypes.Add(service);
                    }
                }
                ExportAgencyData(agency, data, sheet, time, costTypes);
            }
            else
            {
                foreach (ExpenseService service in data)
                {
                    if (!agencies.Contains(service.Supplier) && service.Supplier != null)
                    {
                        agencies.Add(service.Supplier);
                    }
                }

                foreach (Agency supplier in agencies)
                {
                    sheet = excelFile.Worksheets.AddCopy(supplier.Name, excelFile.Worksheets[0]);

                    // Tạo sheet mới, sao chép nguyên từ sheet cũ, số lượng sheet = số lượng agency

                    IList list = new ArrayList();

                    // Chỉ lấy các booking chưa trả hết nợ của agency này
                    foreach (ExpenseService service in data)
                    {
                        if (service.Supplier != supplier)
                        {
                            continue;
                        }
                        // Chỉ loại trừ khi nợ đúng bằng 0
                        if (service.Paid != service.Cost)
                        {
                            list.Add(service);
                        }
                    }

                    IList costTypes = new ArrayList();
                    foreach (ExpenseService service in list)
                    {
                        if (!costTypes.Contains(service.Type) && service.Cost > 0)
                        {
                            costTypes.Add(service.Type);
                        }
                    }

                    ExportAgencyData(supplier, list, sheet, time, costTypes);
                }
            }
            #endregion

            #region -- Trả dữ liệu về cho người dùng --

            // Xóa sheet mẫu
            if (excelFile.Worksheets.Count > 0)
            {
                excelFile.Worksheets[0].Delete();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + string.Format("Congno{0}", time));

            MemoryStream m = new MemoryStream();

            excelFile.SaveXls(m);

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();

            m.Close();
            Response.End();

            #endregion
        }

        private void ExportAgencyData(Agency agency, IList list, ExcelWorksheet sheet, string time, IList services)
        {
            #region -- Thông tin chung --

            sheet.Cells["E4"].Value = time;
            sheet.Cells["E17"].Value = UserIdentity.FullName;

            const int firstCol = 3;
            const int firstRow = 7;
            if (services.Count > 1)
            {
                for (int ii = 1; ii < 50; ii++)
                {
                    sheet.Columns[100].Delete();
                }
                sheet.Columns[firstCol].InsertCopy(services.Count - 1, sheet.Columns[firstCol]);
            }

            for (int ii = 0; ii < services.Count; ii++)
            {
                sheet.Cells[firstRow - 1, firstCol + ii].Value = ((CostType)services[ii]).Name;
            }

            List<DateTime> dates = new List<DateTime>();
            Dictionary<DateTime, Dictionary<CostType, double>> map =
                new Dictionary<DateTime, Dictionary<CostType, double>>();
            Dictionary<CostType, double> serviceSum = new Dictionary<CostType, double>();

            foreach (CostType type in services)
            {
                serviceSum.Add(type, 0);
            }

            foreach (ExpenseService service in list)
            {
                if (!dates.Contains(service.Expense.Date))
                {
                    dates.Add(service.Expense.Date);
                    map.Add(service.Expense.Date, new Dictionary<CostType, double>());
                    foreach (CostType type in services)
                    {
                        map[service.Expense.Date].Add(type, 0);
                    }
                }
                map[service.Expense.Date][service.Type] += service.Cost - service.Paid;
            }

            sheet.Rows[firstRow].InsertCopy(dates.Count - 1, sheet.Rows[firstRow]);
            int count = 0;
            foreach (DateTime date in dates)
            {
                int rowIndex = firstRow + count;
                sheet.Cells[rowIndex, firstCol - 3].Value = date;

                IList bookings = GetBooking(date);
                int adult = 0;
                int child = 0;
                foreach (Booking booking in bookings)
                {
                    adult += booking.Adult;
                    child += booking.Child;
                }

                sheet.Cells[rowIndex, firstCol - 2].Value = adult;
                sheet.Cells[rowIndex, firstCol - 1].Value = child;

                double sum = 0;
                for (int ii = 0; ii < services.Count; ii++)
                {
                    sheet.Cells[rowIndex, firstCol + ii].Value = map[date][(CostType)services[ii]];
                    serviceSum[(CostType)services[ii]] += map[date][(CostType)services[ii]];
                    sum += map[date][(CostType)services[ii]];
                }
                sheet.Cells[rowIndex, firstCol + services.Count].Value = sum;

                count++;
            }

            int totalRowIndex = firstRow + count;
            double total = 0;
            for (int ii = 0; ii < services.Count; ii++)
            {
                sheet.Cells[totalRowIndex, firstCol + ii].Value = serviceSum[(CostType)services[ii]];
                total += serviceSum[(CostType)services[ii]];
            }
            sheet.Cells[totalRowIndex, firstCol + services.Count].Value = total;
            #endregion
        }

        protected IList GetBooking(DateTime date)
        {
            ICriterion criterion = Expression.Eq("Deleted", false);
            criterion = Expression.And(criterion, Expression.Eq("Status", StatusType.Approved));
            criterion = Module.AddDateExpression(criterion, date);
            int count;
            return Module.BookingGetByCriterion(criterion, null, out count, 0, 0);
        }
    }
}
