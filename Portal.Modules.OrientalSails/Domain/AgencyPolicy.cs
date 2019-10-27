using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{

    #region AgencyPolicy

    /// <summary>
    /// AgencyPolicy object for NHibernate mapped table 'tmh_AgencyPolicy'.
    /// </summary>
    public class AgencyPolicy
    {
        #region Member Variables

        protected double _costFrom;
        protected double? _costTo;
        protected int _id;
        protected double _percentage;
        protected Role _role;
        protected bool _isPercentage;
        protected string _moduleType;

        #endregion

        #region Constructors

        public AgencyPolicy()
        {
            _id = -1;
        }

        public AgencyPolicy(Role role, double costFrom, double? costTo, double percentage)
        {
            _isPercentage = true;
            _role = role;
            _costFrom = costFrom;
            _costTo = costTo;
            _percentage = percentage;
        }

        #endregion

        #region Public Properties

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual Role Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public virtual double CostFrom
        {
            get { return _costFrom; }
            set { _costFrom = value; }
        }

        public virtual double? CostTo
        {
            get { return _costTo; }
            set { _costTo = value; }
        }

        public virtual double Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }

        public virtual bool IsPercentage
        {
            get { return _isPercentage; }
            set { _isPercentage = value; }
        }

        public virtual string ModuleType
        {
            get { return _moduleType; }
            set { _moduleType = value; }
        }

        #endregion
    }

    #endregion
}