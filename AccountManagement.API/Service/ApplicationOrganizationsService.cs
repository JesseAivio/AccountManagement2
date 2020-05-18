using AccountManagement.API.Data.Context;
using AccountManagement.API.Data.Enteties;
using AccountManagement.API.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Service
{
    public interface IApplicationOrganizationsService
    {
        IQueryable<ApplicationOrganizationsEntity> GetApplicationOrganizations();
        void AddApplicationOrganization(ApplicationOrganizationsEntity applicationOrganizationsEntity);
    }
    public class ApplicationOrganizationsService : IApplicationOrganizationsService
    {
        readonly AccountManagementContext _accountManagementContext;
        readonly IEmailService _emailService;
        readonly ILicenseService _licenseService;
        ILogger _logger;
        public ApplicationOrganizationsService(AccountManagementContext accountManagementContext, IEmailService emailService, ILicenseService licenseService, ILogger<ApplicationOrganizationsService> logger)
        {
            _accountManagementContext = accountManagementContext;
            _emailService = emailService;
            _licenseService = licenseService;
            _logger = logger;
        }

        public IQueryable<ApplicationOrganizationsEntity> GetApplicationOrganizations()
        {
            List<ApplicationOrganizationsEntity> applicationOrganizationsEntities = new List<ApplicationOrganizationsEntity>();
            foreach(var apporganization in _accountManagementContext.ApplicationOrganizations.ToList())
            {
                Application application = _accountManagementContext.Applications.FirstOrDefault(app => app.Id == apporganization.Application);
                Organization organization = _accountManagementContext.Organizations.FirstOrDefault(organization => organization.Id == apporganization.Organization);
                License license = _accountManagementContext.Licenses.FirstOrDefault(license => license.Id == apporganization.License);
                ApplicationOrganizationsEntity applicationOrganizationsEntity = new ApplicationOrganizationsEntity
                {
                    Application = application.Name,
                    Organization = organization.Name,
                    License = license.Key
                };
                applicationOrganizationsEntities.Add(applicationOrganizationsEntity);
            }
            return applicationOrganizationsEntities.AsQueryable();
        }

        public void AddApplicationOrganization(ApplicationOrganizationsEntity applicationOrganizationsEntity)
        {
            Application application = _accountManagementContext.Applications.FirstOrDefault(app => app.Name == applicationOrganizationsEntity.Application);
            Organization organization = _accountManagementContext.Organizations.FirstOrDefault(organization => organization.Name == applicationOrganizationsEntity.Organization);
            License license = _accountManagementContext.Licenses.FirstOrDefault(license => license.Application == application.Id && license.isFree);
            ApplicationOrganizations applicationOrganizations = new ApplicationOrganizations
            {
                Application = application.Id,
                Organization = organization.Id,
                License = license.Id
            };
            _logger.LogInformation($"applicationOrganizations: {applicationOrganizations}");
            license.isFree = false;
            license.ReleaseDate = DateTime.Now.AddDays(30);
            license.RenewDate = DateTime.Now.AddDays(60);
            _accountManagementContext.ApplicationOrganizations.Add(applicationOrganizations);
            _accountManagementContext.SaveChanges();
            _emailService.SendEmail("AppRegister", organization.Email, organization.Name, "");
        }
    }
}
