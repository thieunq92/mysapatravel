using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Modules.OrientalSails.HangFire.Domain
{
    public class AgencyContact
    {
        public virtual int Id { get; set; }

        public virtual DateTime? Birthday { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }
    }
}
