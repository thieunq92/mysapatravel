using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class ContractCreateBLL
    {
        public ContractRepository ContractRepository { get; set; }
        public ContractValidRepository ContractValidRepository { get; set; }
        public ContractPriceRepository ContractPriceRepository { get; set; }
        public ContractCreateBLL()
        {
            ContractRepository = new ContractRepository();
            ContractValidRepository = new ContractValidRepository();
            ContractPriceRepository = new ContractPriceRepository();
        }
        public void Dispose()
        {
            if (ContractRepository != null)
            {
                ContractRepository.Dispose();
                ContractRepository = null;
            }

            if (ContractValidRepository != null) {
                ContractValidRepository.Dispose();
                ContractValidRepository = null;
            }

            if(ContractPriceRepository != null){
                ContractPriceRepository.Dispose();
                ContractPriceRepository = null;
            }
        }
        public void ContractSaveOrUpdate(Contracts contract)
        {
            ContractRepository.SaveOrUpdate(contract);
        }

        public void ContractValidSaveOrUpdate(ContractValid contractValid)
        {
            ContractValidRepository.SaveOrUpdate(contractValid);
        }

        public void ContractPriceSaveOrUpdate(ContractPrice contractPrice)
        {
            ContractPriceRepository.SaveOrUpdate(contractPrice);
        }
    }
}