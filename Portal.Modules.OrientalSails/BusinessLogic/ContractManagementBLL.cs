using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class ContractManagementBLL
    {
        public ContractRepository ContractRepository { get; set; }

        public ContractManagementBLL()
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

        public IList<Contracts> ContractGetAll()
        {
            return ContractRepository.ContractGetAll();
        }
    }
}