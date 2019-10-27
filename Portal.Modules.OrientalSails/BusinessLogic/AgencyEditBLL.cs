using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class AgencyEditBLL
    {
        public AgencyLocationRepository AgencyLocationRepository { get; set; }
        public RoleRepository RoleRepository { get; set; }
        public AgencyEditBLL()
        {
            AgencyLocationRepository = new AgencyLocationRepository();
            RoleRepository = new RoleRepository();
        }

        public IList<AgencyLocation> AgencyLocationGetAll()
        {
            return AgencyLocationRepository.AgencyLocationGetAll();
        }

        public IList<Role> RoleGetAll()
        {
            return RoleRepository.RoleGetAll();
        }

        public void Dispose()
        {
            if (AgencyLocationRepository != null)
            {
                AgencyLocationRepository.Dispose();
                AgencyLocationRepository = null;
            }

            if (RoleRepository != null)
            {
                RoleRepository.Dispose();
                RoleRepository = null;
            }
        }

        public Role RoleGetById(int roleId)
        {
            return RoleRepository.RoleGetById(roleId);
        }
    }
}