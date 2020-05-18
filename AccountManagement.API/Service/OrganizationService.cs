using AccountManagement.API.Data.Context;
using AccountManagement.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Service
{
    public interface IOrganizationService
    {
        IQueryable<Organization> GetOrganizations();
        void AddOrganization(Organization organization);
        void UpdateOrganization(Organization organization);
        void DeleteOrganization(Guid Id);
    }
    public class OrganizationService : IOrganizationService
    {
        readonly AccountManagementContext _accountManagementContext;
        readonly IEmailService _emailService;
        public OrganizationService(AccountManagementContext accountManagementContext, IEmailService emailService)
        {
            _accountManagementContext = accountManagementContext;
            _emailService = emailService;
        }

        public IQueryable<Organization> GetOrganizations()
        {
            return _accountManagementContext.Organizations.ToList().AsQueryable();
        }

        public void AddOrganization(Organization organization)
        {
            _accountManagementContext.Organizations.Add(organization);
            _accountManagementContext.SaveChanges();
            _emailService.SendEmail("organizationRegister", organization.Email, organization.Name, "");
        }

        public void UpdateOrganization(Organization organization)
        {
            Organization organizationDb = _accountManagementContext.Organizations.FirstOrDefault(org => org.Id == organization.Id);

            organizationDb.Name = organization.Name;
            organizationDb.BusinessId = organization.BusinessId;
            organizationDb.Info = organization.Info;
            organizationDb.Email = organization.Email;

            _accountManagementContext.SaveChanges();
        }

        public void DeleteOrganization(Guid Id)
        {
            DeleteApplications(Id);
            Organization organization = _accountManagementContext.Organizations.FirstOrDefault(organization => organization.Id == Id);
            _accountManagementContext.Organizations.Remove(organization);
            _accountManagementContext.SaveChanges();
        }

        private void DeleteApplications(Guid Id)
        {
            var applicationOrganizations = _accountManagementContext.ApplicationOrganizations.Where(app => app.Organization == Id);
            _accountManagementContext.ApplicationOrganizations.RemoveRange(applicationOrganizations);
            _accountManagementContext.SaveChanges();
        }
    }
}
