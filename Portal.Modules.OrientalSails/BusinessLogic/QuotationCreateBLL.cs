using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class QuotationCreateBLL
    {
        public QuotationRepository QuotationRepository { get; set; }
        public QuotationPriceRepository QuotationPriceRepository { get; set; }
        public QuotationCreateBLL()
        {
            QuotationRepository = new QuotationRepository();
            QuotationPriceRepository = new QuotationPriceRepository();
        }
        public void Dispose()
        {
            if (QuotationRepository != null)
            {
                QuotationRepository.Dispose();
                QuotationRepository = null;
            }
            if (QuotationPriceRepository != null)
            {
                QuotationPriceRepository.Dispose();
                QuotationPriceRepository = null;
            }
        }
        public void QuotationSaveOrUpdate(Quotation quotation)
        {
            QuotationRepository.SaveOrUpdate(quotation);
        }
        public void QuotationPriceSaveOrUpdate(QuotationPrice quotationPrice)
        {
            QuotationPriceRepository.SaveOrUpdate(quotationPrice);
        }
    }
}