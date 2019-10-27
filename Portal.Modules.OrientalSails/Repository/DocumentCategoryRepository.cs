using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class DocumentCategoryRepository : RepositoryBase<DocumentCategory>
    {
        public DocumentCategoryRepository() : base() { }

        public DocumentCategoryRepository(ISession session) : base(session) { }

        public IList<DocumentCategory> DocumentGetAll()
        {
            return _session.QueryOver<DocumentCategory>().Future().ToList();
        }
    }
}