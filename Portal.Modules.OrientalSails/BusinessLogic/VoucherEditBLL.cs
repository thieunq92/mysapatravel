using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class VoucherEditBLL
    {
        public AgencyRepository AgencyRepository { get; set; }
        public VoucherEditBLL() {
            AgencyRepository = new AgencyRepository();
        }
        public void Dispose()
        {
            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }
        }

        public Agency AgencyGetById(int agencyId)
        {
            return AgencyRepository.AgencyGetById(agencyId);
        }
    }
}