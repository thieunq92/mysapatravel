using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class ExpenseHistoryRepository : RepositoryBase<ExpenseHistory>
    {
        public ExpenseHistoryRepository() : base() { }
        public ExpenseHistoryRepository(ISession session) : base(session) { }

        public IQueryOver<ExpenseHistory, ExpenseHistory> ExpenseHistoryGetAllByCriterion(int expenseId)
        {
            var query = _session.QueryOver<ExpenseHistory>();
            Expense expenseAlias = null;
            query = query.JoinAlias(x=>x.Expense, ()=>expenseAlias);
            if(expenseId != -1){
                query = query.Where(x=>x.Expense.Id == expenseId);
            }
            return query;
        }
    }
}