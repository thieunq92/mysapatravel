using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class ViewMeetingsBLL
    {
        public ActivityRepository ActivityRepository { get; set; }
        public AgencyRepository AgencyRepository { get; set; }
        public AgencyContactRepository AgencyContactRepository { get; set; }
        public CruiseRepository CruiseRepository { get; set; }

        public ViewMeetingsBLL()
        {
            ActivityRepository = new ActivityRepository();
            AgencyRepository = new AgencyRepository();
            AgencyContactRepository = new AgencyContactRepository();
            CruiseRepository = new CruiseRepository();
        }

        public void Dispose()
        {
            if (ActivityRepository != null)
            {
                ActivityRepository.Dispose();
                ActivityRepository = null;
            }
            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }
            if (AgencyContactRepository != null)
            {
                AgencyContactRepository.Dispose();
                AgencyContactRepository = null;
            }
            if (CruiseRepository != null)
            {
                CruiseRepository.Dispose();
                CruiseRepository = null;
            }
        }

        public Agency AgencyGetById(int agencyId)
        {
            return AgencyRepository.AgencyGetById(agencyId);
        }

        public AgencyContact AgencyContactGetById(int agencyContactId)
        {
            return AgencyContactRepository.AgencyContactGetById(agencyContactId);
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return CruiseRepository.CruiseGetById(cruiseId);
        }

        public Activity ActivityGetById(int activityId)
        {
            return ActivityRepository.GetById(activityId);
        }

        public void ActivitySaveOrUpdate(Activity activity)
        {
            ActivityRepository.SaveOrUpdate(activity);
        }

        public IEnumerable<Cruise> CruiseGetAll()
        {
            return CruiseRepository.CruiseGetAll();
        }
    }
}