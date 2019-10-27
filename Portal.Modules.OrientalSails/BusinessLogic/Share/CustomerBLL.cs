using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic.Share
{
    public class CustomerBLL
    {
        public CustomerRepository CustomerRepository { get; set; }

        public CustomerBLL()
        {
            CustomerRepository = new CustomerRepository();
        }

        public void Dispose()
        {
            if (CustomerRepository != null)
            {
                CustomerRepository.Dispose();
                CustomerRepository = null;
            }
        }
    }
}