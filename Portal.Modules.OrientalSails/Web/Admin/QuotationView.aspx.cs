using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums.Quotation;
using Portal.Modules.OrientalSails.Enums.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class QuotationView : System.Web.UI.Page
    {
        public QuotationViewBLL quotationViewBLL;
        public QuotationViewBLL QuotationViewBLL
        {
            get
            {
                if (quotationViewBLL == null)
                    quotationViewBLL = new QuotationViewBLL();
                return quotationViewBLL;
            }
        }
        public Quotation Quotation
        {
            get
            {
                Quotation quotation = null;
                try
                {
                    if (Request.QueryString["qi"] != null)
                        quotation = QuotationViewBLL.QuotationGetById(Convert.ToInt32(Request.QueryString["qi"]));
                }
                catch (Exception) { }
                return quotation;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtOs2d1nDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtOs2d1nSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtOs2d1nChildren6to11.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow);
            txtOs12d1nCharter.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow);
            txtOs22d1nCharter1to4passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to4passenger);
            txtOs22d1nCharter5to8passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._5to8passenger);
            txtOs22d1nCharter9to12passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._9to12passenger);
            txtOs22d1nCharter13to17passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._13to17passenger);
            txtOs3d2nDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtOs3d2nSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtOs3d2nChildren6to11.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow);
            txtOs13d2nCharter.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow);
            txtOs23d2nCharter1to4passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to4passenger);
            txtOs23d2nCharter5to8passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._5to8passenger);
            txtOs23d2nCharter9to12passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._9to12passenger);
            txtOs23d2nCharter13to17passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._13to17passenger);
            txtCls2d1nDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtCls2d1nSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtCls2d1nChildren6to11.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow);
            txtCls2d1nCharter.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow);
            txtCls3d2nDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtCls3d2nSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtCls3d2nChildren6to11.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow);
            txtCls3d2nCharter.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nDeluxeDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nDeluxeSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nDeluxeExtrabed.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nExecutiveDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nExecutiveSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nExecutiveExtrabed.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nSuiteDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nSuiteSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nSuiteExtrabed.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl2d1nCharter1to40passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to40passenger);
            txtStl2d1nCharter41to50passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._41to50passenger);
            txtStl2d1nCharter51to63passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._51to63passenger);
            txtStl3d2nDeluxeDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl3d2nDeluxeSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl3d2nDeluxeExtrabed.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl3d2nExecutiveDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl3d2nExecutiveSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl3d2nExecutiveExtrabed.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl3d2nSuiteDouble.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl3d2nSuiteSingle.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl3d2nSuiteExtrabed.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow);
            txtStl3d2nCharter1to40passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to40passenger);
            txtStl3d2nCharter41to50passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._41to50passenger);
            txtStl3d2nCharter51to63passenger.Text = GetCurrency() + GetPriceFormatted((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._51to63passenger);
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (quotationViewBLL != null)
            {
                quotationViewBLL.Dispose();
                quotationViewBLL = null;
            }
        }

        public string GetCurrency()
        {
            switch (Quotation.Currency)
            {
                case (int)CurrencyEnum.USD:
                    return "$";
                case (int)CurrencyEnum.VND:
                    return "₫";
                default:
                    return "";
            }
        }
        public string GetValidFrom()
        {
            var validFrom = "";
            if (Quotation.ValidFrom != null)
                validFrom = Quotation.ValidFrom.Value.ToString("dd/MM/yyyy");
            return validFrom;
        }
        public string GetValidTo()
        {
            var validTo = "";
            if (Quotation.ValidTo != null)
                validTo = Quotation.ValidTo.Value.ToString("dd/MM/yyyy");
            return validTo;
        }
        public double GetPrice(int cruiseId, int tripId, int roomClassId, int roomTypeId, bool isCharter, int numberOfPassenger)
        {
            var quotation = Quotation.ListQuotationPrice.Where(x => x.CruiseId == cruiseId
            && x.TripId == tripId
            && x.RoomClassId == roomClassId
            && x.RoomTypeId == roomTypeId
            && x.IsCharter == isCharter
            && x.NumberOfPassenger == numberOfPassenger).FirstOrDefault();

            if (quotation != null)
                return quotation.Price;
            else
                return 0.0;
        }

        public string GetPriceFormatted(int cruiseId, int tripId, int roomClassId, int roomTypeId, bool isCharter, int numberOfPassenger)
        {
            var price = GetPrice(cruiseId, tripId, roomClassId, roomTypeId, isCharter, numberOfPassenger);
            return String.Format("{0:#,##0.##}", price);
        }
    }
}