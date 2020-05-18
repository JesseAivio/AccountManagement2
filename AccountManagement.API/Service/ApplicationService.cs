using AccountManagement.API.Data.Context;
using AccountManagement.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Service
{
    public interface IApplicationService
    {
        IQueryable<Application> GetApplications();
        void AddApplication(Application application);
        void UpdateApplication(Application application);
        void DeleteApplication(Guid Id);
    }
    public class ApplicationService : IApplicationService
    {
        readonly AccountManagementContext _accountManagementContext;
        public ApplicationService(AccountManagementContext accountManagementContext)
        {
            _accountManagementContext = accountManagementContext;
        }

        public IQueryable<Application> GetApplications()
        {
            return _accountManagementContext.Applications.ToList().AsQueryable();
        }

        public void AddApplication(Application application)
        {
            _accountManagementContext.Applications.Add(application);
            _accountManagementContext.SaveChanges();
        }

        public void UpdateApplication(Application application)
        {
            Application applicationDb = _accountManagementContext.Applications.FirstOrDefault(org => org.Id == application.Id);

            applicationDb.Name = application.Name;

            _accountManagementContext.SaveChanges();
        }

        public void DeleteApplication(Guid Id)
        {
            Application application = _accountManagementContext.Applications.FirstOrDefault(app => app.Id == Id);
            DeleteLicenses(Id);
            _accountManagementContext.Applications.Remove(application);
            _accountManagementContext.SaveChanges();
        }

        private void DeleteLicenses(Guid Id)
        {
            List<License> licenses = _accountManagementContext.Licenses.Where(license => license.Application == Id).ToList();
            _accountManagementContext.Licenses.RemoveRange(licenses);
            _accountManagementContext.SaveChanges();
        }
    }
}
