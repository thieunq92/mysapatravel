using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.ReportEngine;
using Portal.Modules.OrientalSails.Web.Controls;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BookingHistories : SailsAdminBasePage
    {
        private BookingHistory _prev;

        protected void Page_Load(object sender, EventArgs e)
        {
            var booking = Module.BookingGetById(Convert.ToInt32(Request.QueryString["bookingid"]));
            var histories = Module.BookingGetHistory(booking);

            _prev = null;
            rptAgencies.DataSource = histories;
            rptAgencies.DataBind();

            _prev = null;
            rptDates.DataSource = histories;
            rptDates.DataBind();

            _prev = null;
            rptTotals.DataSource = histories;
            rptTotals.DataBind();

            _prev = null;
            rptStatus.DataSource = histories;
            rptStatus.DataBind();

            _prev = null;
            rptTrips.DataSource = histories;
            rptTrips.DataBind();

            _prev = null;
            rptCabins.DataSource = histories;
            rptCabins.DataBind();

            _prev = null;
            rptCustomers.DataSource = histories;
            rptCustomers.DataBind();

            _prev = null;
            rptSpecialRequest.DataSource = histories;
            rptSpecialRequest.DataBind();

            _prev = null;
            rptRooms.DataSource = histories;
            rptRooms.DataBind();

            _prev = null;
            rptPickupAddress.DataSource = histories;
            rptPickupAddress.DataBind();
        }

        protected void rptDates_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.StartDate == history.StartDate)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.StartDate.ToString("dd/MM/yyyy"));
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.StartDate.ToString("dd/MM/yyyy"));
                }
                _prev = history;
            }
        }

        protected void rptStatus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.Status == history.Status)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.Status.ToString());
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.Status.ToString());
                }
                _prev = history;
            }
        }

        protected void rptTrips_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.Trip.Id == history.Trip.Id)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.Trip.Name);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.Trip.Name);
                }
                _prev = history;
            }
        }

        protected void rptAgencies_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.Agency.Id == history.Agency.Id)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.Agency.Name);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.Agency.Name);
                }
                _prev = history;
            }
        }

        protected void rptTotals_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.Total == history.Total && _prev.TotalCurrency == history.TotalCurrency)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.Total + history.TotalCurrency);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.Total + _prev.TotalCurrency);
                }
                _prev = history;
            }
        }

        protected void rptCabins_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.CabinNumber == history.CabinNumber)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.CabinNumber);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.CabinNumber);
                }
                _prev = history;
            }
        }

        protected void rptCustomers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.CustomerNumber == history.CustomerNumber)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.CustomerNumber);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.CustomerNumber);
                }
                _prev = history;
            }
        }

        protected void rptSpecialRequest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.SpecialRequest == history.SpecialRequest)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.SpecialRequest);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.SpecialRequest);
                }
                _prev = history;
            }
        }

        protected void rptRooms_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (history.NewRoom != null)
                {

                    if (history.OldRoom == history.NewRoom)
                    {
                        e.Item.Visible = false;
                        return;
                    }


                    ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                    ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                    ValueBinder.BindLiteral(e.Item, "litFrom", history.OldRoom != null ? history.OldRoom.Name : "");
                    ValueBinder.BindLiteral(e.Item, "litTo", history.NewRoom.Name);
                }
                else e.Item.Visible = false;
            }
        }

        protected void rptPickupAddress_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.PickupAddress == history.PickupAddress)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.PickupAddress);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.PickupAddress);
                }
                _prev = history;
            }
        }
    }
}