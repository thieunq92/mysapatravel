using CMS.Core.Domain;
using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class AgencyNotesRepository : RepositoryBase<AgencyNotes>
    {
        public AgencyNotesRepository() { }
        public AgencyNotesRepository(ISession session) : base(session) { }

        public IEnumerable<AgencyNotes> AgencyNotesGetAllByAgency(Agency agency)
        {
            var query = _session.QueryOver<AgencyNotes>().Where(an => an.Agency == agency);
            return query.Future().ToList();
        }

        public AgencyNotes AgencyNotesGetById(int agencyNotesId)
        {
            var query = _session.QueryOver<AgencyNotes>().Where(an => an.Id == agencyNotesId);
            return query.FutureValue().Value;
        }

        public IEnumerable<AgencyNotes> AgencyNotesGetAllByAgencyAndRole(Agency agency, Role role)
        {
            var query = _session.QueryOver<AgencyNotes>().Where(an => an.Agency == agency && an.Role == role);
            return query.Future().ToList();
        }
    }
}