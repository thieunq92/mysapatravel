using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class QuotationRepository : RepositoryBase<Quotation>
    {
        public QuotationRepository() : base() { }
        public QuotationRepository(ISession session)
            : base(session)
        {
        }

        public List<Quotation> QuotationGetAll()
        {
            return _session.QueryOver<Quotation>().Future().ToList();
        }

        public Quotation QuotationGetById(int quotationId)
        {
            return _session.QueryOver<Quotation>().Where(x => x.Id == quotationId).FutureValue().Value;
        }
    }
}