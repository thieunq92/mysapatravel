using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class AgencyViewBLL
    {
        public AgencyRepository AgencyRepository { get; set; }
        public AgencyContractRepository AgencyContractRepository { get; set; }
        public ContractRepository ContractRepository { get; set; }
        public QuotationRepository QuotationRepository { get; set; }
        public AgencyContactRepository AgencyContactRepository { get; set; }
        public CruiseRepository CruiseRepository { get; set; }
        public ActivityRepository ActivityRepository { get; set; }
        public AgencyNotesRepository AgencyNotesRepository { get; set; }
        public RoleRepository RoleRepository { get; set; }
        public AgencyViewBLL()
        {
            AgencyRepository = new AgencyRepository();
            AgencyContractRepository = new AgencyContractRepository();
            ContractRepository = new ContractRepository();
            QuotationRepository = new QuotationRepository();
            AgencyContactRepository = new AgencyContactRepository();
            CruiseRepository = new CruiseRepository();
            ActivityRepository = new ActivityRepository();
            AgencyNotesRepository = new AgencyNotesRepository();
            RoleRepository = new RoleRepository();
        }

        public void Dispose()
        {
            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }
            if (AgencyContractRepository != null)
            {
                AgencyContractRepository.Dispose();
                AgencyContractRepository = null;
            }
            if (ContractRepository != null)
            {
                ContractRepository.Dispose();
                ContractRepository = null;
            }
            if (QuotationRepository != null)
            {
                QuotationRepository.Dispose();
                QuotationRepository = null;
            }
            if (AgencyContactRepository != null)
            {
                AgencyContactRepository.Dispose();
                AgencyContactRepository = null;
            }
            if (CruiseRepository != null)
            {
                CruiseRepository.Dispose();
                CruiseRepository = null;
            }
            if (ActivityRepository != null)
            {
                ActivityRepository.Dispose();
                ActivityRepository = null;
            }
            if (AgencyNotesRepository != null)
            {
                AgencyNotesRepository.Dispose();
                AgencyNotesRepository = null;
            }
            if (RoleRepository != null)
            {
                RoleRepository.Dispose();
                RoleRepository = null;
            }
        }

        public Agency AgencyGetById(int agencyId)
        {
            return AgencyRepository.AgencyGetById(agencyId);
        }
        public void AgencyContractSaveOrUpdate(AgencyContract agencyContract)
        {
            AgencyContractRepository.SaveOrUpdate(agencyContract);
        }
        public IList<AgencyContract> AgencyContractGetAllByAgency(int agencyId)
        {
            return AgencyContractRepository.AgencyContractGetAllByAgency(agencyId);
        }
        public AgencyContract AgencyContractGetById(int agencyContractId)
        {
            return AgencyContractRepository.AgencyContractGetById(agencyContractId);
        }
        public IList<Contracts> ContractGetAll()
        {
            return ContractRepository.ContractGetAll();
        }
        public Contracts ContractGetById(int contractId)
        {
            return ContractRepository.ContractGetById(contractId);
        }
        public IList<Quotation> QuotationGetAll()
        {
            return QuotationRepository.QuotationGetAll();
        }

        public Quotation QuotationGetById(int quotationId)
        {
            return QuotationRepository.QuotationGetById(quotationId);
        }

        public AgencyContact AgencyContactGetById(int agencyContactId)
        {
            return AgencyContactRepository.AgencyContactGetById(agencyContactId);
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return CruiseRepository.CruiseGetById(cruiseId);
        }

        public Activity ActivityGetById(int activityId)
        {
            return ActivityRepository.GetById(activityId);
        }

        public void ActivitySaveOrUpdate(Activity activity)
        {
            ActivityRepository.SaveOrUpdate(activity);
        }

        public IEnumerable<Cruise> CruiseGetAll()
        {
            return CruiseRepository.CruiseGetAll();
        }

        public IEnumerable<AgencyNotes> AgencyNotesGetAllByAgency(Agency agency)
        {
            return AgencyNotesRepository.AgencyNotesGetAllByAgency(agency);
        }

        public IEnumerable<Role> RolesGetAll()
        {
            return RoleRepository.RoleGetAll();
        }

        public Role RoleGetById(int roleId)
        {
            return RoleRepository.RoleGetById(roleId);
        }

        public void AgencyNotesSaveOrUpdate(AgencyNotes agencyNotes)
        {
            AgencyNotesRepository.SaveOrUpdate(agencyNotes);
        }

        public AgencyNotes AgencyNotesGetById(int agencyNotesId)
        {
            return AgencyNotesRepository.AgencyNotesGetById(agencyNotesId);
        }

        public void DeleteAgencyNotes(AgencyNotes agencyNotes)
        {
            AgencyNotesRepository.Delete(agencyNotes);
        }
    }
}