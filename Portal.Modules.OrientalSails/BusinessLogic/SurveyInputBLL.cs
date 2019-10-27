using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class SurveyInputBLL
    {
        public ExpenseRepository ExpenseRepository { get; set; }
        public SurveyInputBLL()
        {
            ExpenseRepository = new ExpenseRepository();
        }
        public void Dispose()
        {
            if (ExpenseRepository != null)
            {
                ExpenseRepository.Dispose();
                ExpenseRepository = null;
            }
        }
        public IQueryOver<Expense, Expense> ExpenseGetAllByCriterion(int cruiseId, DateTime? date)
        {
            return ExpenseRepository.ExpenseGetAllByCriterion(cruiseId, date);
        }
    }
}