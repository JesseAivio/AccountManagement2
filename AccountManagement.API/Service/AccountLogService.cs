using AccountManagement.API.Data.Context;
using AccountManagement.API.Data.Enteties;
using AccountManagement.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Service
{
    public interface IAccountLogService
    {
        IQueryable<AccountLogEntity> GetAccountLogs();
        void AddAccountLog(AccountLogEntity accountLogEntity);
    }
    public class AccountLogService : IAccountLogService
    {
        readonly AccountManagementContext _accountManagementContext;
        public AccountLogService(AccountManagementContext accountManagementContext)
        {
            _accountManagementContext = accountManagementContext;
        }
        public IQueryable<AccountLogEntity> GetAccountLogs()
        {
            List<AccountLogEntity> accountLogEntities = new List<AccountLogEntity>();
            foreach(var accountlog in _accountManagementContext.AccountLogs.ToList())
            {
                Account account = _accountManagementContext.Accounts.FirstOrDefault(account => account.Id == accountlog.Id);
                Application application = _accountManagementContext.Applications.FirstOrDefault(app => app.Id == accountlog.Id);
                AccountLogEntity accountLogEntity = new AccountLogEntity
                {
                    HWID = accountlog.HWID,
                    IpAddress = accountlog.IpAddress,
                    wasSuccessful = accountlog.wasSuccessful,
                    Date = accountlog.Date,
                    Account = account.Username,
                    Application = application.Name,
                    Id = accountlog.Id
                };
                accountLogEntities.Add(accountLogEntity);
            }
            return accountLogEntities.AsQueryable();
        }

        public void AddAccountLog(AccountLogEntity accountLogEntity)
        {
            Account account = _accountManagementContext.Accounts.FirstOrDefault(account => account.Username == accountLogEntity.Account);
            Application application = _accountManagementContext.Applications.FirstOrDefault(app => app.Name == accountLogEntity.Application);
            AccountLog accountLog = new AccountLog
            {
                HWID = accountLogEntity.HWID,
                IpAddress = accountLogEntity.IpAddress,
                wasSuccessful = accountLogEntity.wasSuccessful,
                Date = accountLogEntity.Date,
                Account = account.Id,
                Application = application.Id
            };

            _accountManagementContext.AccountLogs.Add(accountLog);
            _accountManagementContext.SaveChanges();
        }
    }
}
