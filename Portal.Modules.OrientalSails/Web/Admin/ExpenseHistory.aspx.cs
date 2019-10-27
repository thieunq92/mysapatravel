using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class ExpenseHistory : System.Web.UI.Page
    {
        private ExpenseHistoryBLL expenseHistoryBLL;
        public ExpenseHistoryBLL ExpenseHistoryBLL
        {
            get
            {
                if (expenseHistoryBLL == null)
                {
                    expenseHistoryBLL = new ExpenseHistoryBLL();
                }
                return expenseHistoryBLL;
            }
        }

        public Expense Expense
        {
            get
            {
                var expenseId = -1;
                try
                {
                    expenseId = Int32.Parse(Request.QueryString["ei"]);
                }
                catch { }
                Expense expense = null;
                try
                {
                    expense = ExpenseHistoryBLL.ExpenseGetById(expenseId);
                }
                catch
                {
                }
                return expense;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var listExpenseHistory = new List<Domain.ExpenseHistory>();
            if (Expense != null)
            {
                listExpenseHistory = ExpenseHistoryBLL.ExpenseHistoryGetAllByCriterion(Expense.Id).Future().ToList();     
            }
            
            if (!IsPostBack)
            {
                rptGuideHistory.DataSource = listExpenseHistory.Where(x => x.ColumnName == "GuideId").ToList();
                rptGuideHistory.DataBind();
                rptNameHistory.DataSource = listExpenseHistory.Where(x => x.ColumnName == "Name").ToList();
                rptNameHistory.DataBind();
                rptPhoneHistory.DataSource = listExpenseHistory.Where(x => x.ColumnName == "Phone").ToList();
                rptPhoneHistory.DataBind();
                rptCostHistory.DataSource = listExpenseHistory.Where(x => x.ColumnName == "Cost").ToList();
                rptCostHistory.DataBind();
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (expenseHistoryBLL != null)
            {
                expenseHistoryBLL.Dispose();
                expenseHistoryBLL = null;
            }
        }

        public string GetGuide(string guideId)
        {
            var output = "";
            var intGuideId = -1;
            try
            {
                intGuideId = Int32.Parse(guideId);
            }
            catch { }
            var guide = ExpenseHistoryBLL.AgencyGetById(intGuideId);
            if (guide != null && guide.Id > 0)
            {
                output += guide.Name + " - M: <span>" + NumberUtil.FormatPhoneNumber(guide.Phone) +"</span>";
            }
            return output;
        }

        public string GetCost(string cost)
        {
            var output = "";
            var doubleCost = 0.0;
            try
            {
                doubleCost = Double.Parse(cost);
            }
            catch { }
            output += doubleCost.ToString("#,##0.##") + "₫";
            return output;
        }
    }
}