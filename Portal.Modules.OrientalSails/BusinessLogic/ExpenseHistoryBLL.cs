using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class ExpenseHistoryBLL
    {
        public ExpenseRepository ExpenseRepository { get; set; }
        public ExpenseHistoryRepository ExpenseHistoryRepository { get; set; }
        public AgencyRepository AgencyRepository { get; set; }
        public ExpenseHistoryBLL()
        {
            ExpenseRepository = new ExpenseRepository();
            ExpenseHistoryRepository = new ExpenseHistoryRepository();
            AgencyRepository = new AgencyRepository();
        }
        public void Dispose()
        {
            if (ExpenseRepository != null)
            {
                ExpenseRepository.Dispose();
                ExpenseRepository = null;
            }
            if (ExpenseHistoryRepository != null)
            {
                ExpenseHistoryRepository.Dispose();
                ExpenseHistoryRepository = null;
            }
            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }
        }

        public Expense ExpenseGetById(int expenseId)
        {
            return ExpenseRepository.ExpenseGetById(expenseId);
        }
        public IQueryOver<ExpenseHistory, ExpenseHistory> ExpenseHistoryGetAllByCriterion(int expenseId)
        {
            return ExpenseHistoryRepository.ExpenseHistoryGetAllByCriterion(expenseId);
        }

        public Agency AgencyGetById(int guideId)
        {
            return AgencyRepository.AgencyGetById(guideId);
        }
    }
}