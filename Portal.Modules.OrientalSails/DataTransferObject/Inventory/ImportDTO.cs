using System;
using System.Collections.Generic;

namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class ImportDTO
    {
        public virtual int Id { get; set; }
        public virtual int ImportId { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual string Name { get; set; }

        public virtual int CreatedBy { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual int ModifiedBy { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }


        public virtual string Code { get; set; }

        public virtual double Total { get; set; }

        public virtual string Detail { get; set; }

        public virtual int AgencyId { get; set; }

        public virtual DateTime ImportDate { get; set; }

        public virtual string ImportedBy { get; set; }
        public virtual int StorageId { get; set; }
        public virtual int CruiseId { get; set; }
        public List<ImportProductDTO> ImportProducts { get; set; }

    }
}
