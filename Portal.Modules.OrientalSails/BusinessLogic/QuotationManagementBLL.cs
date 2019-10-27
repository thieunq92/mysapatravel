using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class QuotationManagementBLL
    {
        public QuotationRepository QuotationRepository { get; set; }
        public QuotationManagementBLL()
        {
            QuotationRepository = new QuotationRepository();
        }
        public void Dispose()
        {
            if (QuotationRepository != null)
            {
                QuotationRepository.Dispose();
                QuotationRepository = null;
            }
        }
        public List<Quotation> QuotationGetAll()
        {
            return QuotationRepository.QuotationGetAll();
        }
    }
}