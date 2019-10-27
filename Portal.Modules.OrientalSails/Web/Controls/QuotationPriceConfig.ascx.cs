using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Controls
{
    public partial class QuotationPriceConfig : System.Web.UI.UserControl
    {
        private SailsModule _module;
        private QQuotation _qQuotation;
        private int _trip;
        private QCruiseGroup _group;
        private string _agentLvCode;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void NewQuotationPriceConfig(SailsModule module, string agentLvCode, int trip, QCruiseGroup group, QQuotation quotation)
        {
            _module = module;
            _trip = trip;
            _group = group;
            _qQuotation = quotation;
            _agentLvCode = agentLvCode;
            hidTrip.Value = trip.ToString();
            rptRoomPrice.DataSource = module.GetGroupRoomPrice(group.Id, agentLvCode, trip, quotation);
            rptRoomPrice.DataBind();

            // load cruise charter price config
            rptCruise.DataSource = module.CruiseGetAllByGroup(group.Id);
            rptCruise.DataBind();
        }


        protected void rptRoomPrice_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var roomPrice = e.Item.DataItem as QGroupRomPrice;
            if (roomPrice != null)
            {
                TextBox txtPriceDoubleUsd = e.Item.FindControl("txtPriceDoubleUsd") as TextBox;
                TextBox txtPriceDoubleVnd = e.Item.FindControl("txtPriceDoubleVnd") as TextBox;
                TextBox txtPriceTwinUsd = e.Item.FindControl("txtPriceTwinUsd") as TextBox;
                TextBox txtPriceTwinVnd = e.Item.FindControl("txtPriceTwinVnd") as TextBox;
                TextBox txtPriceExtraUsd = e.Item.FindControl("txtPriceExtraUsd") as TextBox;
                TextBox txtPriceExtraVnd = e.Item.FindControl("txtPriceExtraVnd") as TextBox;
                TextBox txtPriceChildUsd = e.Item.FindControl("txtPriceChildUsd") as TextBox;
                TextBox txtPriceChildVnd = e.Item.FindControl("txtPriceChildVnd") as TextBox;
                CheckBox checkIsDelete = e.Item.FindControl("checkIsDelete") as CheckBox;

                if (roomPrice.PriceDoubleUsd != null)
                {
                    if (txtPriceDoubleUsd != null) txtPriceDoubleUsd.Text = roomPrice.PriceDoubleUsd.ToString();
                }
                if (roomPrice.PriceDoubleVnd != null)
                {
                    if (txtPriceDoubleVnd != null) txtPriceDoubleVnd.Text = roomPrice.PriceDoubleVnd.ToString();
                }

                if (roomPrice.PriceTwinUsd != null)
                {
                    if (txtPriceTwinUsd != null) txtPriceTwinUsd.Text = roomPrice.PriceTwinUsd.ToString();
                }
                if (roomPrice.PriceTwinVnd != null)
                {
                    txtPriceTwinVnd.Text = roomPrice.PriceTwinVnd.ToString();
                }

                if (roomPrice.PriceExtraUsd != null)
                {
                    txtPriceExtraUsd.Text = roomPrice.PriceExtraUsd.ToString();
                }
                if (roomPrice.PriceExtraVnd != null)
                {
                    txtPriceExtraVnd.Text = roomPrice.PriceExtraVnd.ToString();
                }

                if (roomPrice.PriceChildUsd != null)
                {
                    txtPriceChildUsd.Text = roomPrice.PriceChildUsd.ToString();
                }
                if (roomPrice.PriceChildVnd != null)
                {
                    txtPriceChildVnd.Text = roomPrice.PriceChildVnd.ToString();
                }
                checkIsDelete.Checked = roomPrice.IsDeleted;
            }
        }
        protected void rptCruseCharter_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cruiseCharterRange = e.Item.DataItem as QCruiseCharterRange;
            if (cruiseCharterRange != null)
            {
                var litCruiseName = e.Item.FindControl("litCruiseName") as Literal;
                if (litCruiseName != null)
                    litCruiseName.Text = string.Format("{0} {1} cabins", cruiseCharterRange.Cruise.Name, cruiseCharterRange.Cruise.Rooms.Count);
                var listRange = _module.GetCruiseCharterRange(cruiseCharterRange.Group).Where(c => c.Cruise == cruiseCharterRange.Cruise);
                var rptCharterRangerHeader = e.Item.FindControl("rptCharterRangerHeader") as Repeater;
                if (rptCharterRangerHeader != null && cruiseCharterRange.CharterRangeConfig != null)
                {
                    rptCharterRangerHeader.DataSource = listRange;
                    rptCharterRangerHeader.DataBind();
                }
                var rptCharterRanger = e.Item.FindControl("rptCharterRanger") as Repeater;
                if (rptCharterRanger != null)
                {
                    rptCharterRanger.DataSource = listRange;
                    rptCharterRanger.DataBind();
                }
            }
        }

        protected void rptCharterRanger_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var qCruiseCharterRange = e.Item.DataItem as QCruiseCharterRange;
            if (qCruiseCharterRange != null)
            {
                if (_qQuotation != null)
                {
                    var roomPrice = _module.GetCharterRangePrice(_qQuotation, qCruiseCharterRange, _trip);
                    if (roomPrice != null)
                    {
                        var hidCharterRangePriceId = e.Item.FindControl("hidCharterRangePriceId") as HiddenField;
                        if (hidCharterRangePriceId != null)
                        {
                            hidCharterRangePriceId.Value = roomPrice.Id.ToString();
                        }
                        var txtCharterRangePriceUsd = e.Item.FindControl("txtCharterRangePriceUSD") as TextBox;
                        if (txtCharterRangePriceUsd != null)
                        {
                            txtCharterRangePriceUsd.Text = roomPrice.Priceusd.ToString();
                        }
                        var txtCharterRangePriceVnd = e.Item.FindControl("txtCharterRangePriceVND") as TextBox;
                        if (txtCharterRangePriceVnd != null)
                        {
                            txtCharterRangePriceVnd.Text = roomPrice.Pricevnd.ToString();
                        }
                    }
                }
            }
        }

        public void SaveQuotationPriceConfig(SailsModule module, string agentLvCode, int trip, int groupId, QQuotation quotation)
        {


            QCruiseGroup group = module.GetById<QCruiseGroup>(groupId);
            // save room price
            foreach (RepeaterItem item in rptRoomPrice.Items)
            {

                var roomPrice = new QGroupRomPrice();
                HiddenField hidRoomPriceId = item.FindControl("hidRoomPriceId") as HiddenField;
                TextBox txtRoomType = item.FindControl("txtRoomType") as TextBox;
                TextBox txtPriceDoubleUsd = item.FindControl("txtPriceDoubleUsd") as TextBox;
                TextBox txtPriceDoubleVnd = item.FindControl("txtPriceDoubleVnd") as TextBox;
                TextBox txtPriceTwinUsd = item.FindControl("txtPriceTwinUsd") as TextBox;
                TextBox txtPriceTwinVnd = item.FindControl("txtPriceTwinVnd") as TextBox;
                TextBox txtPriceExtraUsd = item.FindControl("txtPriceExtraUsd") as TextBox;
                TextBox txtPriceExtraVnd = item.FindControl("txtPriceExtraVnd") as TextBox;
                TextBox txtPriceChildUsd = item.FindControl("txtPriceChildUsd") as TextBox;
                TextBox txtPriceChildVnd = item.FindControl("txtPriceChildVnd") as TextBox;
                CheckBox checkIsDelete = item.FindControl("checkIsDelete") as CheckBox;


                if (!string.IsNullOrEmpty(hidRoomPriceId.Value) && hidRoomPriceId.Value != "0")
                {
                    roomPrice = module.GetById<QGroupRomPrice>(Convert.ToInt32(hidRoomPriceId.Value));
                }
                if (!string.IsNullOrEmpty(txtRoomType.Text))
                {
                    roomPrice.RoomType = txtRoomType.Text;
                }
                if (!string.IsNullOrEmpty(txtPriceDoubleUsd.Text))
                {
                    roomPrice.PriceDoubleUsd = Convert.ToDecimal(txtPriceDoubleUsd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceDoubleVnd.Text))
                {
                    roomPrice.PriceDoubleVnd = Convert.ToDecimal(txtPriceDoubleVnd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceTwinUsd.Text))
                {
                    roomPrice.PriceTwinUsd = Convert.ToDecimal(txtPriceTwinUsd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceTwinVnd.Text))
                {
                    roomPrice.PriceTwinVnd = Convert.ToDecimal(txtPriceTwinVnd.Text);
                }

                if (!string.IsNullOrEmpty(txtPriceExtraUsd.Text))
                {
                    roomPrice.PriceExtraUsd = Convert.ToDecimal(txtPriceExtraUsd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceExtraVnd.Text))
                {
                    roomPrice.PriceExtraVnd = Convert.ToDecimal(txtPriceExtraVnd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceChildUsd.Text))
                {
                    roomPrice.PriceChildUsd = Convert.ToDecimal(txtPriceChildUsd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceChildVnd.Text))
                {
                    roomPrice.PriceChildVnd = Convert.ToDecimal(txtPriceChildVnd.Text);
                }
                roomPrice.GroupCruise = group;
                roomPrice.AgentLevelCode = agentLvCode;
                roomPrice.QQuotation = quotation;
                roomPrice.Trip = trip;
                if (checkIsDelete.Checked)
                    module.Delete(roomPrice);
                else
                {
                    module.SaveOrUpdate(roomPrice);
                }


            }
            //save charter range

            foreach (RepeaterItem cruiseItem in rptCruise.Items)
            {
                var hidCruise = cruiseItem.FindControl("hidCruise") as HiddenField;
                var cruiseprice = cruiseItem.FindControl("cruiseprice") as CruiseCharterConfigPrice;
                if (cruiseprice != null)
                {
                    if (hidCruise != null && !string.IsNullOrWhiteSpace(hidCruise.Value))
                    {
                        var cruise = module.GetById<Cruise>(Convert.ToInt32(hidCruise.Value));
                        cruiseprice.SaveDataConfig(module, cruise, group, agentLvCode, trip, quotation);
                    }
                }
            }




        }
        protected void btnAddRoomType_OnClick(object sender, EventArgs e)
        {
            IList data = new List<QGroupRomPrice>();
            foreach (RepeaterItem item in rptRoomPrice.Items)
            {
                var roomPrice = new QGroupRomPrice();
                HiddenField hidRoomPriceId = item.FindControl("hidRoomPriceId") as HiddenField;

                TextBox txtRoomType = item.FindControl("txtRoomType") as TextBox;
                TextBox txtPriceDoubleUsd = item.FindControl("txtPriceDoubleUsd") as TextBox;
                TextBox txtPriceDoubleVnd = item.FindControl("txtPriceDoubleVnd") as TextBox;
                TextBox txtPriceTwinUsd = item.FindControl("txtPriceTwinUsd") as TextBox;
                TextBox txtPriceTwinVnd = item.FindControl("txtPriceTwinVnd") as TextBox;
                TextBox txtPriceExtraUsd = item.FindControl("txtPriceExtraUsd") as TextBox;
                TextBox txtPriceExtraVnd = item.FindControl("txtPriceExtraVnd") as TextBox;
                TextBox txtPriceChildUsd = item.FindControl("txtPriceChildUsd") as TextBox;
                TextBox txtPriceChildVnd = item.FindControl("txtPriceChildVnd") as TextBox;
                CheckBox checkIsDelete = item.FindControl("checkIsDelete") as CheckBox;


                if (!string.IsNullOrEmpty(hidRoomPriceId.Value) && hidRoomPriceId.Value != "0")
                {
                    roomPrice.Id = Convert.ToInt32(hidRoomPriceId.Value);
                }
                if (!string.IsNullOrEmpty(txtRoomType.Text))
                {
                    roomPrice.RoomType = txtRoomType.Text;
                }
                if (!string.IsNullOrEmpty(txtPriceDoubleUsd.Text))
                {
                    roomPrice.PriceDoubleUsd = Convert.ToDecimal(txtPriceDoubleUsd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceDoubleVnd.Text))
                {
                    roomPrice.PriceDoubleVnd = Convert.ToDecimal(txtPriceDoubleVnd.Text);
                }

                if (!string.IsNullOrEmpty(txtPriceTwinUsd.Text))
                {
                    roomPrice.PriceTwinUsd = Convert.ToDecimal(txtPriceTwinUsd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceTwinVnd.Text))
                {
                    roomPrice.PriceTwinVnd = Convert.ToDecimal(txtPriceTwinVnd.Text);
                }

                if (!string.IsNullOrEmpty(txtPriceExtraUsd.Text))
                {
                    roomPrice.PriceExtraUsd = Convert.ToDecimal(txtPriceExtraUsd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceExtraVnd.Text))
                {
                    roomPrice.PriceExtraVnd = Convert.ToDecimal(txtPriceExtraVnd.Text);
                }

                if (!string.IsNullOrEmpty(txtPriceChildUsd.Text))
                {
                    roomPrice.PriceChildUsd = Convert.ToDecimal(txtPriceChildUsd.Text);
                }
                if (!string.IsNullOrEmpty(txtPriceChildVnd.Text))
                {
                    roomPrice.PriceChildVnd = Convert.ToDecimal(txtPriceChildVnd.Text);
                }

                roomPrice.IsDeleted = checkIsDelete.Checked;
                data.Add(roomPrice);
            }

            data.Add(new QGroupRomPrice());
            rptRoomPrice.DataSource = data;
            rptRoomPrice.DataBind();
        }

        protected void rptCruise_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cruise = e.Item.DataItem as Cruise;
            if (cruise != null)
            {
                var charterConfig = e.Item.FindControl("cruiseprice") as CruiseCharterConfigPrice;
                if (charterConfig != null)
                    charterConfig.DisplayDataConfig(_module, cruise, _group, _agentLvCode, _trip, _qQuotation);
            }
        }
    }
}