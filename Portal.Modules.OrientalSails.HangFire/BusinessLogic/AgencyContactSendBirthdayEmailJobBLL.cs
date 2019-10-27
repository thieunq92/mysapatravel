using Portal.Modules.OrientalSails.HangFire.Domain;
using Portal.Modules.OrientalSails.HangFire.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Modules.OrientalSails.HangFire.BusinessLogic
{
    public class AgencyContactSendBirthdayEmailJobBLL
    {
        public AgencyContactRepository AgencyContactRepository { get; set; }

        public AgencyContactSendBirthdayEmailJobBLL()
        {
            AgencyContactRepository = new AgencyContactRepository();
        }

        public void Dispose()
        {
            if (AgencyContactRepository != null)
            {
                AgencyContactRepository.Dispose();
                AgencyContactRepository = null;
            }
        }


        public IList<AgencyContact> AgencyContactGetByBirthday()
        {
            return AgencyContactRepository.AgencyContactGetAllByBirthday();
        }
    }
}
