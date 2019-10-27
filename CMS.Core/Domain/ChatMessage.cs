using System;
using System.Collections;
using System.Security.Principal;
using CMS.Core.Util;

namespace CMS.Core.Domain
{
    /// <summary>
    /// Summary description for User.
    /// </summary>
    public class ChatMessage
    {
        public virtual int Id { get; set; }
        public virtual ChatGroup ChatGroup { get; set; }
        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
        public virtual string Message { get; set; }
        public virtual DateTime TimeStamp { get; set; }
        public virtual ChatStatusType Status { get; set; }
    }
}