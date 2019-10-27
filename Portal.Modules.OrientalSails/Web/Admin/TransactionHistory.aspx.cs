using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Utils;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class TransactionHistory : SailsAdminBasePage
    {
        private double _totalUSD;
        private double _totalVND;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillQueryString();
                GetTransactionHistory();
            }
        }

        private void GetTransactionHistory()
        {
            rptPaymentHistory.DataSource = Module.GetTransactionHistory(Request.QueryString);
            rptPaymentHistory.DataBind();
        }

        protected void rptPaymentHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Transaction)
            {
                var transaction = (Transaction)e.Item.DataItem;
                ValueBinder.BindLiteral(e.Item, "litTime", transaction.CreatedDate.ToString("dd/MM/yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litPayBy", transaction.AgencyName);
                ValueBinder.BindLiteral(e.Item, "litAmountUSD", transaction.USDAmount.ToString("#,0.#"));
                ValueBinder.BindLiteral(e.Item, "litAmountVND", transaction.VNDAmount.ToString("#,0.#"));
                ValueBinder.BindLiteral(e.Item, "litRate", transaction.AppliedRate > 0 ? transaction.AppliedRate.ToString("#,0.#") : "");
                ValueBinder.BindLiteral(e.Item, "litCreatedBy", transaction.CreatedBy.UserName);
                ValueBinder.BindLiteral(e.Item, "litNote", transaction.Note);
                var hplGroupCode = e.Item.FindControl("hplGroupCode") as HyperLink;
                if (hplGroupCode != null)
                {
                    if (transaction.TransactionGroup != null)
                    {
                        hplGroupCode.Text = transaction.TransactionGroup.Id.ToString();
                        hplGroupCode.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Path) +
                                                   QueryStringBuildByCriterion(transaction.Id.ToString());
                        hplGroupCode.Attributes.Add("data-placement", "bottom");
                        hplGroupCode.Attributes.Add("data-toggle", "tooltip");
                        hplGroupCode.Attributes.Add("title", string.Format("code {0} total usd: {1} , vnd: {2}. {3}",
                            transaction.TransactionGroup.Id, transaction.USDAmount.ToString("#,0.#"),
                            transaction.TransactionGroup.VNDAmount.ToString("#,0.#")
                            , transaction.TransactionGroup.Note));
                    }
                    else
                    {
                        hplGroupCode.Text = transaction.Booking.BookingIdOS;
                    }
                }
                _totalUSD += transaction.USDAmount;
                _totalVND += transaction.VNDAmount;
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ValueBinder.BindLiteral(e.Item, "litTotalUSD", _totalUSD.ToString("#,0.#"));
                ValueBinder.BindLiteral(e.Item, "litTotalVND", _totalVND.ToString("#,0.#"));
            }
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) + QueryStringBuildByCriterion(""));
        }
        public string QueryStringBuildByCriterion(string transactionId)
        {
            NameValueCollection nvcQueryString = new NameValueCollection();
            nvcQueryString.Add("NodeId", "1");
            nvcQueryString.Add("SectionId", "15");

            if (!string.IsNullOrEmpty(txtFrom.Text))
            {
                nvcQueryString.Add("f", txtFrom.Text);
            }

            if (!string.IsNullOrEmpty(txtTo.Text))
            {
                nvcQueryString.Add("t", txtTo.Text);
            }

            if (!string.IsNullOrEmpty(txtGroupCode.Text) && string.IsNullOrWhiteSpace(transactionId))
            {
                nvcQueryString.Add("gc", txtGroupCode.Text);
            }
            if (!string.IsNullOrWhiteSpace(transactionId))
            {
                nvcQueryString.Add("gc", transactionId);
            }
            var criterions = (from key in nvcQueryString.AllKeys
                              from value in nvcQueryString.GetValues(key)
                              select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            return "?" + string.Join("&", criterions);
        }

        private void FillQueryString()
        {

            txtFrom.Text = DateTimeUtil.DateGetDefaultFromDate().ToString("dd/MM/yyyy");
            if (Request.QueryString["f"] != null)
            {
                txtFrom.Text = Request.QueryString["f"];
            }

            txtTo.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("dd/MM/yyyy");
            if (Request.QueryString["t"] != null)
            {
                txtTo.Text = Request.QueryString["t"];
            }

            if (Request.QueryString["gc"] != null)
            {
                txtGroupCode.Text = Request.QueryString["gc"];
            }
        }
    }
}