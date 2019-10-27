using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class ContractViewBLL
    {
        public ContractRepository ContractRepository { get; set; }
        public ContractViewBLL()
        {
            ContractRepository = new ContractRepository();
        }
        public void Dispose()
        {
            if (ContractRepository != null)
            {
                ContractRepository.Dispose();
                ContractRepository = null;
            }
        }

        public Contracts ContractGetById(int contractId)
        {
            return ContractRepository.ContractGetById(contractId);
        }
    }
}