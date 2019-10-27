using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace CMS.ServerControls
{
    public class NumberValidator : RegularExpressionValidator
    {
        private NumberType _numberType = NumberType.Integer;
        public NumberType NumberType
        {
            get { return _numberType; }
            set { _numberType = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Display = ValidatorDisplay.Dynamic;
            switch (_numberType)
            {
                case NumberType.Integer:
                    ValidationExpression = @"^(\+|-)?\d+$";
                    ErrorMessage = "Phải là số nguyên";
                    break;
                case NumberType.PositiveInteger:
                    ValidationExpression = @"^\d+$";
                    ErrorMessage = "Phải là số nguyên dương";
                    break;
                case NumberType.NegativeInteger:
                    ValidationExpression = @"-^\d+$";
                    ErrorMessage = "Phải là số nguyên âm";
                    break;
                case NumberType.Date:
                    ValidationExpression =
                        @"^\d{1,2}\/\d{1,2}\/\d{4}$";
                    ErrorMessage = "Ngày tháng không hợp lệ";
                    break;
            }
        }
    }

    public enum NumberType
    {
        Integer,
        PositiveInteger,
        NegativeInteger,
        Date
    }
}
