using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    /// <summary>
    /// Summary description for ContractCreateWebMethod
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ContractCreateWebMethod : System.Web.Services.WebService
    {
        private ContractCreateBLL contractCreateBLL;
        public ContractCreateBLL ContractCreateBLL
        {
            get
            {
                if (contractCreateBLL == null)
                    contractCreateBLL = new ContractCreateBLL();
                return contractCreateBLL;
            }
        }
        private UserBLL userBLL;
        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                    userBLL = new UserBLL();
                return userBLL;
            }
        }

        [WebMethod]
        public string CreateContract(List<ContractDTO> listContractDTO, string name, string currency)
        {
            var currencyInt = -1;
            try
            {
                currencyInt = Int32.Parse(currency);
            }
            catch { }
            var contract = new Contracts()
            {
                CreatedBy = UserBLL.UserGetCurrent(),
                CreatedDate = DateTime.Now,
                Name = name,
                Currency = currencyInt,
            };
            ContractCreateBLL.ContractSaveOrUpdate(contract);
            foreach (var contractDTO in listContractDTO)
            {
                DateTime? validFromDateTime = null;
                try
                {
                    validFromDateTime = DateTime.ParseExact(contractDTO.validFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch { }
                DateTime? validToDateTime = null;
                try
                {
                    validToDateTime = DateTime.ParseExact(contractDTO.validTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch { }
                var contractValid = new ContractValid()
                {
                    ValidFrom = validFromDateTime,
                    ValidTo = validToDateTime,
                    Contract = contract,
                };
                ContractCreateBLL.ContractValidSaveOrUpdate(contractValid);
                var priceOs2d1nDouble = GetPrice(contractDTO.txtOs2d1nDouble);
                var priceOs2d1nSingle = GetPrice(contractDTO.txtOs2d1nSingle);
                var priceOs2d1nChildren6to11 = GetPrice(contractDTO.txtOs2d1nChildren6to11);
                var priceOs12d1nCharter = GetPrice(contractDTO.txtOs12d1nCharter);
                var priceOs22d1nDouble = GetPrice(contractDTO.txtOs2d1nDouble);
                var priceOs22d1nSingle = GetPrice(contractDTO.txtOs2d1nSingle);
                var priceOs22d1nChildren6to11 = GetPrice(contractDTO.txtOs2d1nChildren6to11);
                var priceOs22d1nCharter1to4passenger = GetPrice(contractDTO.txtOs22d1nCharter1to4passenger);
                var priceOs22d1nCharter5to8passenger = GetPrice(contractDTO.txtOs22d1nCharter5to8passenger);
                var priceOs22d1nCharter9to12passenger = GetPrice(contractDTO.txtOs22d1nCharter9to12passenger);
                var priceOs22d1nCharter13to17passenger = GetPrice(contractDTO.txtOs22d1nCharter13to16passenger);
                var priceOs3d2nDouble = GetPrice(contractDTO.txtOs3d2nDouble);
                var priceOs3d2nSingle = GetPrice(contractDTO.txtOs3d2nSingle);
                var priceOs3d2nChildren6to11 = GetPrice(contractDTO.txtOs3d2nChildren6to11);
                var priceOs13d2nCharter = GetPrice(contractDTO.txtOs13d2nCharter);
                var priceOs23d2nDouble = GetPrice(contractDTO.txtOs3d2nDouble);
                var priceOs23d2nSingle = GetPrice(contractDTO.txtOs3d2nSingle);
                var priceOs23d2nChildren6to11 = GetPrice(contractDTO.txtOs3d2nChildren6to11);
                var priceOs23d2nCharter1to4passenger = GetPrice(contractDTO.txtOs23d2nCharter1to4passenger);
                var priceOs23d2nCharter5to8passenger = GetPrice(contractDTO.txtOs23d2nCharter5to8passenger);
                var priceOs23d2nCharter9to12passenger = GetPrice(contractDTO.txtOs23d2nCharter9to12passenger);
                var priceOs23d2nCharter13to16passenger = GetPrice(contractDTO.txtOs23d2nCharter13to16passenger);
                var priceCls2d1nDouble = GetPrice(contractDTO.txtCls2d1nDouble);
                var priceCls2d1nSingle = GetPrice(contractDTO.txtCls2d1nSingle);
                var priceCls2d1nChildren6to11 = GetPrice(contractDTO.txtCls2d1nChildren6to11);
                var priceCls2d1nCharter = GetPrice(contractDTO.txtCls2d1nCharter);
                var priceCls3d2nDouble = GetPrice(contractDTO.txtCls3d2nDouble);
                var priceCls3d2nSingle = GetPrice(contractDTO.txtCls3d2nSingle);
                var priceCls3d2nChildren6to11 = GetPrice(contractDTO.txtCls3d2nChildren6to11);
                var priceCls3d2nCharter = GetPrice(contractDTO.txtCls3d2nCharter);
                var priceStl2d1nDeluxeDouble = GetPrice(contractDTO.txtStl2d1nDeluxeDouble);
                var priceStl2d1nDeluxeSingle = GetPrice(contractDTO.txtStl2d1nDeluxeSingle);
                var priceStl2d1nDeluxeExtrabed = GetPrice(contractDTO.txtStl2d1nDeluxeExtrabed);
                var priceStl2d1nExecutiveDouble = GetPrice(contractDTO.txtStl2d1nExecutiveDouble);
                var priceStl2d1nExecutiveSingle = GetPrice(contractDTO.txtStl2d1nExecutiveSingle);
                var priceStl2d1nExecutiveExtrabed = GetPrice(contractDTO.txtStl2d1nExecutiveExtrabed);
                var priceStl2d1nSuiteDouble = GetPrice(contractDTO.txtStl2d1nExecutiveDouble);
                var priceStl2d1nSuiteSingle = GetPrice(contractDTO.txtStl2d1nExecutiveSingle);
                var priceStl2d1nSuiteExtrabed = GetPrice(contractDTO.txtStl2d1nExecutiveExtrabed);
                var priceStl2d1nCharter1to30passenger = GetPrice(contractDTO.txtStl2d1nCharter1to30passenger);
                var priceStl2d1nCharter31to40passenger = GetPrice(contractDTO.txtStl2d1nCharter31to40passenger);
                var priceStl2d1nCharter41to50passenger = GetPrice(contractDTO.txtStl2d1nCharter41to50passenger);
                var priceStl2d1nCharter51to64passenger = GetPrice(contractDTO.txtStl2d1nCharter51to64passenger);
                var priceStl3d2nDeluxeDouble = GetPrice(contractDTO.txtStl3d2nDeluxeDouble);
                var priceStl3d2nDeluxeSingle = GetPrice(contractDTO.txtStl3d2nDeluxeSingle);
                var priceStl3d2nDeluxeExtrabed = GetPrice(contractDTO.txtStl3d2nDeluxeExtrabed);
                var priceStl3d2nExecutiveDouble = GetPrice(contractDTO.txtStl3d2nExecutiveDouble);
                var priceStl3d2nExecutiveSingle = GetPrice(contractDTO.txtStl3d2nExecutiveSingle);
                var priceStl3d2nExecutiveExtrabed = GetPrice(contractDTO.txtStl3d2nExecutiveExtrabed);
                var priceStl3d2nSuiteDouble = GetPrice(contractDTO.txtStl3d2nExecutiveDouble);
                var priceStl3d2nSuiteSingle = GetPrice(contractDTO.txtStl3d2nExecutiveSingle);
                var priceStl3d2nSuiteExtrabed = GetPrice(contractDTO.txtStl3d2nExecutiveExtrabed);
                var priceStl3d2nCharter1to30passenger = GetPrice(contractDTO.txtStl3d2nCharter1to30passenger);
                var priceStl3d2nCharter31to40passenger = GetPrice(contractDTO.txtStl3d2nCharter31to40passenger);
                var priceStl3d2nCharter41to50passenger = GetPrice(contractDTO.txtStl3d2nCharter41to50passenger);
                var priceStl3d2nCharter51to64passenger = GetPrice(contractDTO.txtStl3d2nCharter51to64passenger);
                CreateContractPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nChildren6to11, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow, priceOs12d1nCharter, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceOs2d1nChildren6to11, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to4passenger, priceOs22d1nCharter1to4passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._5to8passenger, priceOs22d1nCharter5to8passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._9to12passenger, priceOs22d1nCharter9to12passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._13to16passenger, priceOs22d1nCharter13to17passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceOs3d2nDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceOs3d2nSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceOs3d2nChildren6to11, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails1, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow, priceOs13d2nCharter, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceOs23d2nDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceOs23d2nSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceOs23d2nChildren6to11, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to4passenger, priceOs23d2nCharter1to4passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._5to8passenger, priceOs23d2nCharter5to8passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._9to12passenger, priceOs23d2nCharter9to12passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.OrientalSails2, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._13to16passenger, priceOs23d2nCharter13to16passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceCls2d1nDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceCls2d1nSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceCls2d1nChildren6to11, contractValid);
                CreateContractPrice((int)CruiseEnum.Calypso, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow, priceCls2d1nCharter, contractValid);
                CreateContractPrice((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceCls3d2nDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceCls3d2nSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Children6to11, false, (int)NumberOfPassengerEnum.Unknow, priceCls3d2nChildren6to11, contractValid);
                CreateContractPrice((int)CruiseEnum.Calypso, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum.Unknow, priceCls3d2nCharter, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nDeluxeDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nDeluxeSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nDeluxeExtrabed, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nExecutiveDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nExecutiveSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nExecutiveExtrabed, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nSuiteDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nSuiteSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl2d1nSuiteExtrabed, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to30passenger, priceStl2d1nCharter1to30passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._31to40passenger, priceStl2d1nCharter31to40passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._41to50passenger, priceStl2d1nCharter41to50passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._2Day1Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._51to64passenger, priceStl2d1nCharter51to64passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nDeluxeDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nDeluxeSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Deluxe, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nDeluxeExtrabed, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nExecutiveDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nExecutiveSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Executive, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nExecutiveExtrabed, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Double, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nSuiteDouble, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Single, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nSuiteSingle, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Suite, (int)RoomTypeEnum.Extrabed, false, (int)NumberOfPassengerEnum.Unknow, priceStl3d2nSuiteExtrabed, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._1to30passenger, priceStl3d2nCharter1to30passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._31to40passenger, priceStl3d2nCharter31to40passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._41to50passenger, priceStl3d2nCharter41to50passenger, contractValid);
                CreateContractPrice((int)CruiseEnum.Starlight, (int)TripEnum._3Day2Night, (int)RoomClassEnum.Unknow, (int)RoomTypeEnum.Unknow, true, (int)NumberOfPassengerEnum._51to64passenger, priceStl3d2nCharter51to64passenger, contractValid);
            }
            Dispose();
            return null;
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
        public void CreateContractPrice(int cruiseId, int tripId, int roomClassId, int RoomTypeId, bool isCharter, int numberOfPassenger, double price, ContractValid contractValid)
        {
            var contractPrice = new ContractPrice()
            {
                CruiseId = cruiseId,
                TripId = tripId,
                RoomClassId = roomClassId,
                RoomTypeId = RoomTypeId,
                Price = price,
                ContractValid = contractValid,
                IsCharter = isCharter,
                NumberOfPassenger = numberOfPassenger,
            };
            ContractCreateBLL.ContractPriceSaveOrUpdate(contractPrice);
        }
        public void Dispose()
        {
            if (ContractCreateBLL != null)
            {
                contractCreateBLL.Dispose();
                contractCreateBLL = null;
            }
        }
    }
}
