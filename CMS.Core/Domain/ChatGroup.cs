using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Domain
{
    public class ChatGroup
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual User User { get; set; }
        public virtual IList Users
        {
            get;
            set;
        }

        public virtual IList<ChatGroupUser> ChatGroupUsers { get; set; }
//        public virtual IList ChatMessages
//        {
//            get;
//            set;
//        }
    }
}
