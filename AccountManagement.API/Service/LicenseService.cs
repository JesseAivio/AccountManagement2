using AccountManagement.API.Data.Context;
using AccountManagement.API.Data.Enteties;
using AccountManagement.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Service
{
    public interface ILicenseService
    {
        IQueryable<LicenseEntity> GetLicenses();
        void AddLicenses(int amount);
        void AddLicensesForApplication(int amount, string application);
        void UpdateLicense(LicenseEntity licenseEntity);
        void DeleteLicense(Guid Id);
    }
    public class LicenseService : ILicenseService
    {
        readonly AccountManagementContext _accountManagementContext;
        public LicenseService(AccountManagementContext accountManagementContext)
        {
            _accountManagementContext = accountManagementContext;
        }

        public IQueryable<LicenseEntity> GetLicenses()
        {
            List<LicenseEntity> licenseEntities = new List<LicenseEntity>();
            foreach(var license in _accountManagementContext.Licenses.ToList())
            {
                Application application = _accountManagementContext.Applications.FirstOrDefault(app => app.Id == license.Application);
                LicenseEntity licenseEntity = new LicenseEntity
                {
                    Id = license.Id,
                    Key = license.Key,
                    Application = application.Name,
                    isFree = license.isFree,
                    isLocked = license.isLocked,
                    ReleaseDate = license.ReleaseDate,
                    RenewDate = license.RenewDate
                };
                licenseEntities.Add(licenseEntity);
            }
            return licenseEntities.AsQueryable();
        }

        public void AddLicenses(int amount)
        {
            foreach(var app in _accountManagementContext.Applications.ToList())
            {
                for (int i = 0; i < amount; i++)
                {
                    LicenseEntity licenseEntity = new LicenseEntity
                    {
                        Application = app.Name,
                        isFree = true,
                        isLocked = false,
                        ReleaseDate = DateTime.MinValue,
                        RenewDate = DateTime.MinValue,
                        Key = ""
                    };
                    AddLicense(licenseEntity);
                }
            }
        }

        public void AddLicensesForApplication(int amount, string application)
        {
            for (int i = 0; i < amount; i++)
            {
                LicenseEntity licenseEntity = new LicenseEntity
                {
                    Application = application,
                    isFree = true,
                    isLocked = false,
                    ReleaseDate = DateTime.MinValue,
                    RenewDate = DateTime.MinValue,
                    Key = ""
                };
                AddLicense(licenseEntity);
            }
        }
        public void UpdateLicense(LicenseEntity licenseEntity)
        {
            License license = _accountManagementContext.Licenses.FirstOrDefault(license => license.Id == licenseEntity.Id);
            Application application = _accountManagementContext.Applications.FirstOrDefault(app => app.Name == licenseEntity.Application);
            license.Key = licenseEntity.Key;
            license.Application = application.Id;
            license.isFree = licenseEntity.isFree;
            license.isLocked = licenseEntity.isLocked;
            license.ReleaseDate = licenseEntity.ReleaseDate;
            license.RenewDate = licenseEntity.RenewDate;

            _accountManagementContext.SaveChanges();
        }

        public void DeleteLicense(Guid Id)
        {
            License license = _accountManagementContext.Licenses.FirstOrDefault(license => license.Id == Id);

            _accountManagementContext.Licenses.Remove(license);
            _accountManagementContext.SaveChanges();
        }

        private void AddLicense(LicenseEntity licenseEntity)
        {
            Application application = _accountManagementContext.Applications.FirstOrDefault(app => app.Name == licenseEntity.Application);
            License license = new License
            {
                Id = licenseEntity.Id,
                Key = GenerateKey(30),
                Application = application.Id,
                isFree = licenseEntity.isFree,
                isLocked = licenseEntity.isLocked,
                ReleaseDate = licenseEntity.ReleaseDate,
                RenewDate = licenseEntity.RenewDate
            };
            _accountManagementContext.Licenses.Add(license);
            _accountManagementContext.SaveChanges();
        }

        private string GenerateKey(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
