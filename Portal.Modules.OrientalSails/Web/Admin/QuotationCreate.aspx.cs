using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums.Quotation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class QuotationCreate : System.Web.UI.Page
    {
        private UserBLL userBLL;
        private QuotationCreateBLL quotationCreateBLL;
        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                    userBLL = new UserBLL();
                return userBLL;
            }
        }
        public QuotationCreateBLL QuotationCreateBLL
        {
            get
            {
                if (quotationCreateBLL == null)
                    quotationCreateBLL = new QuotationCreateBLL();
                return quotationCreateBLL;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            if (userBLL != null)
            {
                userBLL.Dispose();
                userBLL = null;
            }
            if (quotationCreateBLL != null)
            {
                quotationCreateBLL.Dispose();
                quotationCreateBLL = null;
            }
        }
        protected void btnCreateQuotation_Click(object sender, EventArgs e)
        {
            DateTime? validFrom = null;
            try
            {
                validFrom = DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            DateTime? validTo = null;
            try
            {
                validTo = DateTime.ParseExact(txtValidTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var quotation = new Quotation()
            {
                ValidFrom = validFrom,
                ValidTo = validTo,
                CreatedDate = DateTime.Now,
                CreatedBy = UserBLL.UserGetCurrent(),
                Name = txtName.Text,
                Currency = Int32.Parse(ddlCurrency.SelectedValue),
            };
            QuotationCreateBLL.QuotationSaveOrUpdate(quotation);
            var priceOs2d1nDouble = GetPrice(txtOs2d1nDouble.Text);
            var priceOs2d1nSingle = GetPrice(txtOs2d1nSingle.Text);
            var priceOs2d1nChildren6to11 = GetPrice(txtOs2d1nChildren6to11.Text);
            var priceOs12d1nCharter = GetPrice(txtOs12d1nCharter.Text);
            var priceOs22d1nDouble = GetPrice(txtOs2d1nDouble.Text);
            var priceOs22d1nSingle = GetPrice(txtOs2d1nSingle.Text);
            var priceOs22d1nChildren6to11 = GetPrice(txtOs2d1nChildren6to11.Text);
            var priceOs22d1nCharter1to4passenger = GetPrice(txtOs22d1nCharter1to4passenger.Text);
            var priceOs22d1nCharter5to8passenger = GetPrice(txtOs22d1nCharter5to8passenger.Text);
            var priceOs22d1nCharter9to12passenger = GetPrice(txtOs22d1nCharter9to12passenger.Text);
            var priceOs22d1nCharter13to17passenger = GetPrice(txtOs22d1nCharter13to17passenger.Text);
            var priceOs3d2nDouble = GetPrice(txtOs3d2nDouble.Text);
            var priceOs3d2nSingle = GetPrice(txtOs3d2nSingle.Text);
            var priceOs3d2nChildren6to11 = GetPrice(txtOs3d2nChildren6to11.Text);
            var priceOs13d2nCharter = GetPrice(txtOs13d2nCharter.Text);
            var priceOs23d2nDouble = GetPrice(txtOs3d2nDouble.Text);
            var priceOs23d2nSingle = GetPrice(txtOs3d2nSingle.Text);
            var priceOs23d2nChildren6to11 = GetPrice(txtOs3d2nChildren6to11.Text);
            var priceOs23d2nCharter1to4passenger = GetPrice(txtOs23d2nCharter1to4passenger.Text);
            var priceOs23d2nCharter5to8passenger = GetPrice(txtOs23d2nCharter5to8passenger.Text);
            var priceOs23d2nCharter9to12passenger = GetPrice(txtOs23d2nCharter9to12passenger.Text);
            var priceOs23d2nCharter13to17passenger = GetPrice(txtOs23d2nCharter13to17passenger.Text);
            var priceCls2d1nDouble = GetPrice(txtCls2d1nDouble.Text);
            var priceCls2d1nSingle = GetPrice(txtCls2d1nSingle.Text);
            var priceCls2d1nChildren6to11 = GetPrice(txtCls2d1nChildren6to11.Text);
            var priceCls2d1nCharter = GetPrice(txtCls2d1nCharter.Text);
            var priceCls3d2nDouble = GetPrice(txtCls3d2nDouble.Text);
            var priceCls3d2nSingle = GetPrice(txtCls3d2nSingle.Text);
            var priceCls3d2nChildren6to11 = GetPrice(txtCls3d2nChildren6to11.Text);
            var priceCls3d2nCharter = GetPrice(txtCls3d2nCharter.Text);
            var priceStl2d1nDeluxeDouble = GetPrice(txtStl2d1nDeluxeDouble.Text);
            var priceStl2d1nDeluxeSingle = GetPrice(txtStl2d1nDeluxeSingle.Text);
            var priceStl2d1nDeluxeExtrabed = GetPrice(txtStl2d1nDeluxeExtrabed.Text);
            var priceStl2d1nExecutiveDouble = GetPrice(txtStl2d1nExecutiveDouble.Text);
            var priceStl2d1nExecutiveSingle = GetPrice(txtStl2d1nExecutiveSingle.Text);
            var priceStl2d1nExecutiveExtrabed = GetPrice(txtStl2d1nExecutiveExtrabed.Text);
            var priceStl2d1nSuiteDouble = GetPrice(txtStl2d1nExecutiveDouble.Text);
            var priceStl2d1nSuiteSingle = GetPrice(txtStl2d1nExecutiveSingle.Text);
            var priceStl2d1nSuiteExtrabed = GetPrice(txtStl2d1nExecutiveExtrabed.Text);
            var priceStl2d1nCharter1to40passenger = GetPrice(txtStl2d1nCharter1to40passenger.Text);
            var priceStl2d1nCharter41to50passenger = GetPrice(txtStl2d1nCharter41to50passenger.Text);
            var priceStl2d1nCharter51to63passenger = GetPrice(txtStl2d1nCharter51to63passenger.Text);
            var priceStl3d2nDeluxeDouble = GetPrice(txtStl3d2nDeluxeDouble.Text);
            var priceStl3d2nDeluxeSingle = GetPrice(txtStl3d2nDeluxeSingle.Text);
            var priceStl3d2nDeluxeExtrabed = GetPrice(txtStl3d2nDeluxeExtrabed.Text);
            var priceStl3d2nExecutiveDouble = GetPrice(txtStl3d2nExecutiveDouble.Text);
            var priceStl3d2nExecutiveSingle = GetPrice(txtStl3d2nExecutiveSingle.Text);
            var priceStl3d2nExecutiveExtrabed = GetPrice(txtStl3d2nExecutiveExtrabed.Text);
            var priceStl3d2nSuiteDouble = GetPrice(txtStl3d2nExecutiveDouble.Text);
            var priceStl3d2nSuiteSingle = GetPrice(txtStl3d2nExecutiveSingle.Text);
            var priceStl3d2nSuiteExtrabed = GetPrice(txtStl3d2nExecutiveExtrabed.Text);
            var priceStl3d2nCharter1to40passenger = GetPrice(txtStl3d2nCharter1to40passenger.Text);
            var priceStl3d2nCharter41to50passenger = GetPrice(txtStl3d2nCharter41to50passenger.Text);
            var priceStl3d2nCharter51to63passenger = GetPrice(txtStl3d2nCharter51to63passenger.Text);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nChildren6to11, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow, priceOs12d1nCharter, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nChildren6to11, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to4passenger, priceOs22d1nCharter1to4passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._5to8passenger, priceOs22d1nCharter5to8passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._9to12passenger, priceOs22d1nCharter9to12passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._13to17passenger, priceOs22d1nCharter13to17passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceOs3d2nDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceOs3d2nSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceOs3d2nChildren6to11, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow, priceOs13d2nCharter, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceOs23d2nDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceOs23d2nSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceOs23d2nChildren6to11, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to4passenger, priceOs23d2nCharter1to4passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._5to8passenger, priceOs23d2nCharter5to8passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._9to12passenger, priceOs23d2nCharter9to12passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._13to17passenger, priceOs23d2nCharter13to17passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceCls2d1nDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceCls2d1nSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceCls2d1nChildren6to11, quotation);
            CreateQuotationPrice((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow, priceCls2d1nCharter, quotation);
            CreateQuotationPrice((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceCls3d2nDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceCls3d2nSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceCls3d2nChildren6to11, quotation);
            CreateQuotationPrice((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow, priceCls3d2nCharter, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nDeluxeDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nDeluxeSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nDeluxeExtrabed, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nExecutiveDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nExecutiveSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nExecutiveExtrabed, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nSuiteDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nSuiteSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nSuiteExtrabed, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to40passenger, priceStl2d1nCharter1to40passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._41to50passenger, priceStl2d1nCharter41to50passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._51to63passenger, priceStl2d1nCharter51to63passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nDeluxeDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nDeluxeSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nDeluxeExtrabed, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nExecutiveDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nExecutiveSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nExecutiveExtrabed, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nSuiteDouble, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nSuiteSingle, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nSuiteExtrabed, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to40passenger, priceStl3d2nCharter1to40passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._41to50passenger, priceStl3d2nCharter41to50passenger, quotation);
            CreateQuotationPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._51to63passenger, priceStl3d2nCharter51to63passenger, quotation);
            Response.Redirect("QuotationManagement.aspx?NodeId=1&SectionId=15");
        }
        public double GetPrice(string priceStringType)
        {
            var price = 0.0;
            try
            {
                price = Double.Parse(priceStringType);
            }
            catch { }
            return price;
        }
        public void CreateQuotationPrice(int cruiseId, int tripId, int roomClassId, int RoomTypeId, bool isCharter, int numberOfPassenger, double price, Quotation quotation)
        {
            var quotationPrice = new QuotationPrice()
            {
                CruiseId = cruiseId,
                TripId = tripId,
                RoomClassId = roomClassId,
                RoomTypeId = RoomTypeId,
                Price = price,
                Quotation = quotation,
                IsCharter = isCharter,
                NumberOfPassenger = numberOfPassenger,
            };
            QuotationCreateBLL.QuotationPriceSaveOrUpdate(quotationPrice);
        }
    }
}