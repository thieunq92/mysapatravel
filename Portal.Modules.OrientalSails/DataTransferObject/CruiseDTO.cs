using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class CruiseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ExpenseDTO> ListGuideExpenseDTO { get; set; }
        public List<ExpenseDTO> ListOthersExpenseDTO { get; set; }
        public List<ExpenseDTO> ListDeletedGuideExpenseDTO { get; set; }
        public List<ExpenseDTO> ListDeletedOthersExpenseDTO { get; set; }
        public string ExpenseLockStatus
        {
            get
            {
                if (ListGuideExpenseDTO.Count <= 0 && ListOthersExpenseDTO.Count <= 0)
                    return "Locked";

                if (ListGuideExpenseDTO.Select(x => x.LockStatus).Contains("Unlocked")
                    || ListGuideExpenseDTO.Select(x => x.LockStatus).Contains("")
                    || ListGuideExpenseDTO.Select(x => x.LockStatus).Contains(null)
                    || ListOthersExpenseDTO.Select(x => x.LockStatus).Contains("Unlocked")
                    || ListOthersExpenseDTO.Select(x => x.LockStatus).Contains("")
                    || ListOthersExpenseDTO.Select(x => x.LockStatus).Contains(null)
                    )
                {
                    return "Unlocked";
                }
                else
                {
                    return "Locked";
                }
            }
        }
    }
}