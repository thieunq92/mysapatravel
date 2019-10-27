using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums.Contract;
using Portal.Modules.OrientalSails.Enums.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class ContractView : System.Web.UI.Page
    {
        public ContractViewBLL contractViewBLL;
        public ContractViewBLL ContractViewBLL
        {
            get
            {
                if (contractViewBLL == null)
                {
                    contractViewBLL = new ContractViewBLL();
                }
                return contractViewBLL;
            }
        }
        public Contracts Contract
        {
            get
            {
                Contracts contract = null;
                try
                {
                    if (Request.QueryString["ci"] != null)
                        contract = ContractViewBLL.ContractGetById(Convert.ToInt32(Request.QueryString["ci"]));
                }
                catch (Exception) { }
                return contract;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rptValidTimeOs2d1n.DataSource = Contract.ListContractValid;
            rptValidTimeOs2d1n.DataBind();
            rptOs2d1nHeader.DataSource = Contract.ListContractValid;
            rptOs2d1nHeader.DataBind();
            rptPriceOs2d1n.DataSource = Contract.ListContractValid;
            rptPriceOs2d1n.DataBind();
            rptValidTimeOs2d1nCharter.DataSource = Contract.ListContractValid;
            rptValidTimeOs2d1nCharter.DataBind();
            rptOs22d1nCharterHeader.DataSource = Contract.ListContractValid;
            rptOs22d1nCharterHeader.DataBind();
            rptPriceOs12d1nCharter.DataSource = Contract.ListContractValid;
            rptPriceOs12d1nCharter.DataBind();
            rptPriceOs22d1nCharter.DataSource = Contract.ListContractValid;
            rptPriceOs22d1nCharter.DataBind();
            rptValidTimeOs3d2n.DataSource = Contract.ListContractValid;
            rptValidTimeOs3d2n.DataBind();
            rptOs3d2nHeader.DataSource = Contract.ListContractValid;
            rptOs3d2nHeader.DataBind();
            rptPriceOs3d2n.DataSource = Contract.ListContractValid;
            rptPriceOs3d2n.DataBind();
            rptValidTimeOs3d2nCharter.DataSource = Contract.ListContractValid;
            rptValidTimeOs3d2nCharter.DataBind();
            rptPriceOs13d2nCharter.DataSource = Contract.ListContractValid;
            rptPriceOs13d2nCharter.DataBind();
            rptOs23d2nCharterHeader.DataSource = Contract.ListContractValid;
            rptOs23d2nCharterHeader.DataBind();
            rptPriceOs23d2nCharter.DataSource = Contract.ListContractValid;
            rptPriceOs23d2nCharter.DataBind();
            rptValidTimeCls2d1n.DataSource = Contract.ListContractValid;
            rptValidTimeCls2d1n.DataBind();
            rptCls2d1nHeader.DataSource = Contract.ListContractValid;
            rptCls2d1nHeader.DataBind();
            rptPriceCls2d1n.DataSource = Contract.ListContractValid;
            rptPriceCls2d1n.DataBind();
            rptValidTimeCls2d1nCharter.DataSource = Contract.ListContractValid;
            rptValidTimeCls2d1nCharter.DataBind();
            rptPriceCls2d1nCharter.DataSource = Contract.ListContractValid;
            rptPriceCls2d1nCharter.DataBind();
            rptValidTimeCls3d2n.DataSource = Contract.ListContractValid;
            rptValidTimeCls3d2n.DataBind();
            rptCls3d2nHeader.DataSource = Contract.ListContractValid;
            rptCls3d2nHeader.DataBind();
            rptPriceCls3d2n.DataSource = Contract.ListContractValid;
            rptPriceCls3d2n.DataBind();
            rptValidTimeCls3d2nCharter.DataSource = Contract.ListContractValid;
            rptValidTimeCls3d2nCharter.DataBind();
            rptPriceCls3d2nCharter.DataSource = Contract.ListContractValid;
            rptPriceCls3d2nCharter.DataBind();
            rptValidTimeStl2d1n.DataSource = Contract.ListContractValid;
            rptValidTimeStl2d1n.DataBind();
            rptStl2d1nHeader.DataSource = Contract.ListContractValid;
            rptStl2d1nHeader.DataBind();
            rptPriceStl2d1nDeluxe.DataSource = Contract.ListContractValid;
            rptPriceStl2d1nDeluxe.DataBind();
            rptPriceStl2d1nExecutive.DataSource = Contract.ListContractValid;
            rptPriceStl2d1nExecutive.DataBind();
            rptPriceStl2d1nSuite.DataSource = Contract.ListContractValid;
            rptPriceStl2d1nSuite.DataBind();
            rptPriceStl2d1nCharter.DataSource = Contract.ListContractValid;
            rptPriceStl2d1nCharter.DataBind();
            rptValidTimeStl3d2n.DataSource = Contract.ListContractValid;
            rptValidTimeStl3d2n.DataBind();
            rptStl3d2nHeader.DataSource = Contract.ListContractValid;
            rptStl3d2nHeader.DataBind();
            rptPriceStl3d2nDeluxe.DataSource = Contract.ListContractValid;
            rptPriceStl3d2nDeluxe.DataBind();
            rptPriceStl3d2nExecutive.DataSource = Contract.ListContractValid;
            rptPriceStl3d2nExecutive.DataBind();
            rptPriceStl3d2nSuite.DataSource = Contract.ListContractValid;
            rptPriceStl3d2nSuite.DataBind();
            rptPriceStl3d2nCharter.DataSource = Contract.ListContractValid;
            rptPriceStl3d2nCharter.DataBind();
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (contractViewBLL != null)
            {
                contractViewBLL.Dispose();
                contractViewBLL = null;
            }
        }
        protected void rptPriceOs2d1n_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtOs2d1nDouble = e.Item.FindControl("txtOs2d1nDouble") as Literal;
            var txtOs2d1nSingle = e.Item.FindControl("txtOs2d1nSingle") as Literal;
            var txtOs2d1nChildren6to11 = e.Item.FindControl("txtOs2d1nChildren6to11") as Literal;
            txtOs2d1nDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow);
            txtOs2d1nSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow);
            txtOs2d1nChildren6to11.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow);
        }
        public string GetCurrency()
        {
            switch (Contract.Currency)
            {
                case (int)CurrencyEnum.USD:
                    return "$";
                case (int)CurrencyEnum.VND:
                    return "₫";
                default:
                    return "";
            }
        }
        public double GetPrice(ContractValid contractValid, int cruiseId, int tripId, int roomClassId, int roomTypeId, bool isCharter, int numberOfPassenger)
        {
            var quotation = contractValid.ListContractPrice.Where(x => x.CruiseId == cruiseId
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
        public string GetPriceFormatted(ContractValid contractValid, int cruiseId, int tripId, int roomClassId, int roomTypeId, bool isCharter, int numberOfPassenger)
        {
            var price = GetPrice(contractValid, cruiseId, tripId, roomClassId, roomTypeId, isCharter, numberOfPassenger);
            return String.Format("{0:#,##0.##}", price);
        }
        protected void rptPriceOs12d1nCharter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtOs12d1nCharter = e.Item.FindControl("txtOs12d1nCharter") as Literal;
            txtOs12d1nCharter.Text = GetCurrency() + GetPrice(contractValid, (int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow).ToString();
        }
        protected void rptPriceOs22d1nCharter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtOs22d1nCharter1to4passenger = e.Item.FindControl("txtOs22d1nCharter1to4passenger") as Literal;
            var txtOs22d1nCharter5to8passenger = e.Item.FindControl("txtOs22d1nCharter5to8passenger") as Literal;
            var txtOs22d1nCharter9to12passenger = e.Item.FindControl("txtOs22d1nCharter9to12passenger") as Literal;
            var txtOs22d1nCharter13to16passenger = e.Item.FindControl("txtOs22d1nCharter13to16passenger") as Literal;
            txtOs22d1nCharter1to4passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to4passenger);
            txtOs22d1nCharter5to8passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._5to8passenger);
            txtOs22d1nCharter9to12passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._9to12passenger);
            txtOs22d1nCharter13to16passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._13to16passenger);
        }
        protected void rptPriceOs3d2n_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtOs3d2nDouble = e.Item.FindControl("txtOs3d2nDouble") as Literal;
            var txtOs3d2nSingle = e.Item.FindControl("txtOs3d2nSingle") as Literal;
            var txtOs3d2nChildren6to11 = e.Item.FindControl("txtOs3d2nChildren6to11") as Literal;
            txtOs3d2nDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtOs3d2nSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtOs3d2nChildren6to11.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow).ToString();
        }
        protected void rptPriceOs13d2nCharter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtOs13d2nCharter = e.Item.FindControl("txtOs13d2nCharter") as Literal;
            txtOs13d2nCharter.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow).ToString();
        }
        protected void rptPriceOs23d2nCharter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtOs23d2nCharter1to4passenger = e.Item.FindControl("txtOs23d2nCharter1to4passenger") as Literal;
            var txtOs23d2nCharter5to8passenger = e.Item.FindControl("txtOs23d2nCharter5to8passenger") as Literal;
            var txtOs23d2nCharter9to12passenger = e.Item.FindControl("txtOs23d2nCharter9to12passenger") as Literal;
            var txtOs23d2nCharter13to16passenger = e.Item.FindControl("txtOs23d2nCharter13to16passenger") as Literal;
            txtOs23d2nCharter1to4passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to4passenger).ToString();
            txtOs23d2nCharter5to8passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._5to8passenger).ToString();
            txtOs23d2nCharter9to12passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._9to12passenger).ToString();
            txtOs23d2nCharter13to16passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._13to16passenger).ToString();
        }

        protected void rptPriceCls2d1n_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtCls2d1nDouble = e.Item.FindControl("txtCls2d1nDouble") as Literal;
            var txtCls2d1nSingle = e.Item.FindControl("txtCls2d1nSingle") as Literal;
            var txtCls2d1nChildren6to11 = e.Item.FindControl("txtCls2d1nChildren6to11") as Literal;
            txtCls2d1nDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtCls2d1nSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtCls2d1nChildren6to11.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow).ToString();
        }

        protected void rptPriceCls2d1nCharter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtCls2d1nCharter = e.Item.FindControl("txtCls2d1nCharter") as Literal;
            txtCls2d1nCharter.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow).ToString();
        }
        protected void rptPriceCls3d2n_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtCls3d2nDouble = e.Item.FindControl("txtCls3d2nDouble") as Literal;
            var txtCls3d2nSingle = e.Item.FindControl("txtCls3d2nSingle") as Literal;
            var txtCls3d2nChildren6to11 = e.Item.FindControl("txtCls3d2nChildren6to11") as Literal;
            txtCls3d2nDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtCls3d2nSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtCls3d2nChildren6to11.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow).ToString();
        }

        protected void rptPriceCls3d2nCharter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtCls3d2nCharter = e.Item.FindControl("txtCls3d2nCharter") as Literal;
            txtCls3d2nCharter.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow).ToString();
        }

        protected void rptPriceStl2d1nDeluxe_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtStl2d1nDeluxeDouble = e.Item.FindControl("txtStl2d1nDeluxeDouble") as Literal;
            var txtStl2d1nDeluxeSingle = e.Item.FindControl("txtStl2d1nDeluxeSingle") as Literal;
            var txtStl2d1nDeluxeExtrabed = e.Item.FindControl("txtStl2d1nDeluxeExtrabed") as Literal;
            txtStl2d1nDeluxeDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl2d1nDeluxeSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl2d1nDeluxeExtrabed.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow).ToString();
        }

        protected void rptPriceStl2d1nExecutive_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtStl2d1nExecutiveDouble = e.Item.FindControl("txtStl2d1nExecutiveDouble") as Literal;
            var txtStl2d1nExecutiveSingle = e.Item.FindControl("txtStl2d1nExecutiveSingle") as Literal;
            var txtStl2d1nExecutiveExtrabed = e.Item.FindControl("txtStl2d1nExecutiveExtrabed") as Literal;
            txtStl2d1nExecutiveDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl2d1nExecutiveSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl2d1nExecutiveExtrabed.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow).ToString();
        }

        protected void rptPriceStl2d1nSuite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtStl2d1nSuiteDouble = e.Item.FindControl("txtStl2d1nSuiteDouble") as Literal;
            var txtStl2d1nSuiteSingle = e.Item.FindControl("txtStl2d1nSuiteSingle") as Literal;
            var txtStl2d1nSuiteExtrabed = e.Item.FindControl("txtStl2d1nSuiteExtrabed") as Literal;
            txtStl2d1nSuiteDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl2d1nSuiteSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl2d1nSuiteExtrabed.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow).ToString();
        }

        protected void rptPriceStl2d1nCharter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtStl2d1nCharter1to30passenger = e.Item.FindControl("txtStl2d1nCharter1to30passenger") as Literal;
            var txtStl2d1nCharter31to40passenger = e.Item.FindControl("txtStl2d1nCharter31to40passenger") as Literal;
            var txtStl2d1nCharter41to50passenger = e.Item.FindControl("txtStl2d1nCharter41to50passenger") as Literal;
            var txtStl2d1nCharter51to64passenger = e.Item.FindControl("txtStl2d1nCharter51to64passenger") as Literal;
            txtStl2d1nCharter1to30passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to30passenger).ToString();
            txtStl2d1nCharter31to40passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._31to40passenger).ToString();
            txtStl2d1nCharter41to50passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._41to50passenger).ToString();
            txtStl2d1nCharter51to64passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._51to64passenger).ToString();
        }

        protected void rptPriceStl3d2nDeluxe_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtStl3d2nDeluxeDouble = e.Item.FindControl("txtStl3d2nDeluxeDouble") as Literal;
            var txtStl3d2nDeluxeSingle = e.Item.FindControl("txtStl3d2nDeluxeSingle") as Literal;
            var txtStl3d2nDeluxeExtrabed = e.Item.FindControl("txtStl3d2nDeluxeExtrabed") as Literal;
            txtStl3d2nDeluxeDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl3d2nDeluxeSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl3d2nDeluxeExtrabed.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow).ToString();
        }

        protected void rptPriceStl3d2nExecutive_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtStl3d2nExecutiveDouble = e.Item.FindControl("txtStl3d2nExecutiveDouble") as Literal;
            var txtStl3d2nExecutiveSingle = e.Item.FindControl("txtStl3d2nExecutiveSingle") as Literal;
            var txtStl3d2nExecutiveExtrabed = e.Item.FindControl("txtStl3d2nExecutiveExtrabed") as Literal;
            txtStl3d2nExecutiveDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl3d2nExecutiveSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl3d2nExecutiveExtrabed.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow).ToString();
        }

        protected void rptPriceStl3d2nSuite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtStl3d2nSuiteDouble = e.Item.FindControl("txtStl3d2nSuiteDouble") as Literal;
            var txtStl3d2nSuiteSingle = e.Item.FindControl("txtStl3d2nSuiteSingle") as Literal;
            var txtStl3d2nSuiteExtrabed = e.Item.FindControl("txtStl3d2nSuiteExtrabed") as Literal;
            txtStl3d2nSuiteDouble.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl3d2nSuiteSingle.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow).ToString();
            txtStl3d2nSuiteExtrabed.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow).ToString();
        }

        protected void rptPriceStl3d2nCharter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var contractValid = e.Item.DataItem as ContractValid;
            var txtStl3d2nCharter1to30passenger = e.Item.FindControl("txtStl3d2nCharter1to30passenger") as Literal;
            var txtStl3d2nCharter31to40passenger = e.Item.FindControl("txtStl3d2nCharter31to40passenger") as Literal;
            var txtStl3d2nCharter41to50passenger = e.Item.FindControl("txtStl3d2nCharter41to50passenger") as Literal;
            var txtStl3d2nCharter51to64passenger = e.Item.FindControl("txtStl3d2nCharter51to64passenger") as Literal;
            txtStl3d2nCharter1to30passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to30passenger).ToString();
            txtStl3d2nCharter31to40passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._31to40passenger).ToString();
            txtStl3d2nCharter41to50passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._41to50passenger).ToString();
            txtStl3d2nCharter51to64passenger.Text = GetCurrency() + GetPriceFormatted(contractValid, (int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._51to64passenger).ToString();
        }
    }
}