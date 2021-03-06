using System;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Nationality
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual int NaCode { set; get; }
        public virtual string NaNameViet { set; get; }
        public virtual string AbbreviationCode { get; set; }
    }
}