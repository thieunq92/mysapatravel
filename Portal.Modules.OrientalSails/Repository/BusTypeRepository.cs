using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class BusTypeRepository : RepositoryBase<BusType>
    {
        public BusTypeRepository() : base() { }
        public BusTypeRepository(ISession session) : base(session) { }
        public IQueryOver<BusType, BusType> BusTypeGetAll()
        {
            return _session.QueryOver<BusType>();
        }

        public BusType BusTypeGetById(int busTypeId)
        {
            return _session.QueryOver<BusType>().Where(x => x.Id == busTypeId).FutureValue().Value;
        }

        public IQueryOver<BusType, BusType> BusTypeGetAllById(int busTypeId)
        {
            var query = _session.QueryOver<BusType>();
            if (busTypeId > -1)
            {
                query = query.Where(x => x.Id == busTypeId);
            }
            return query;
        }
    }
}