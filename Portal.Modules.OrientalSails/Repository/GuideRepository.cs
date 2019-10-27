using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class GuideRepository : RepositoryBase<Guide>
    {
        public GuideRepository() { }
        public GuideRepository(ISession session) : base(session) { }
    }
}