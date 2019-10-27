using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Domain
{
    public class ChatGroupUser
    {
        public virtual int Id { get; set; }
        public virtual ChatGroup ChatGroup { get; set; }
        public virtual User User { get; set; }
    }
}
