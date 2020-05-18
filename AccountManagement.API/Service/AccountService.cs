using AccountManagement.API.Data.Context;
using AccountManagement.API.Data.Enteties;
using AccountManagement.API.Data.Models;
using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.API.Service
{
    public interface IAccountService
    {
        IQueryable<AccountEntity> GetAccounts();
        void AddAccount(AccountEntity accountEntety);
        void UpdateAccount(AccountEntity accountEntety);
        void DeleteAccount(Guid Id);
    }
    public class AccountService : IAccountService
    {
        readonly AccountManagementContext _accountManagementContext;
        readonly IEmailService _emailService;
        public AccountService(AccountManagementContext accountManagementContext, IEmailService emailService)
        {
            _accountManagementContext = accountManagementContext;
            _emailService = emailService;
        }

        public IQueryable<AccountEntity> GetAccounts()
        {
            List<AccountEntity> accountEntities = new List<AccountEntity>();
            foreach (var account in _accountManagementContext.Accounts.ToList())
            {
                Organization organization = _accountManagementContext.Organizations.FirstOrDefault(org => org.Id == account.Organization);
                AccountEntity accountEntety = new AccountEntity()
                {

                    Id = account.Id,
                    Username = account.Username,
                    Email = account.Email,
                    Password = account.Password,
                    Salt = account.Salt,
                    PhoneNumber = account.PhoneNumber,
                    Role = account.Role,
                    Organization = organization.Name

                };
                accountEntities.Add(accountEntety);
            }
            return accountEntities.AsQueryable();
        }

        public void AddAccount(AccountEntity accountEntety)
        {
            byte[] salt = CreateSalt();
            Organization organization = _accountManagementContext.Organizations.FirstOrDefault(org => org.Name == accountEntety.Organization);
            Account account = new Account
            {
                Email = accountEntety.Email,
                Organization = organization.Id,
                Username = accountEntety.Username,
                PhoneNumber = accountEntety.PhoneNumber,
                Role = accountEntety.Role,
                Password = Convert.ToBase64String(HashPassword(accountEntety.Password, salt)),
                Salt = Convert.ToBase64String(salt)
            };
            _accountManagementContext.Accounts.Add(account);
            _accountManagementContext.SaveChanges();
            _emailService.SendEmail("accountRegister", account.Email, organization.Name, account.Username);
        }

        public void UpdateAccount(AccountEntity accountEntety)
        {
            Account account = _accountManagementContext.Accounts.FirstOrDefault(account => account.Id == accountEntety.Id);

            account.Username = accountEntety.Username;
            account.Email = accountEntety.Email;
            account.PhoneNumber = account.PhoneNumber;
            account.Role = account.Role;
            if (!string.IsNullOrEmpty(accountEntety.Password))
            {
                byte[] salt = CreateSalt();
                account.Password = Convert.ToBase64String(HashPassword(accountEntety.Password, salt));
                account.Salt = Convert.ToBase64String(salt);
            }

            Organization organization = _accountManagementContext.Organizations.FirstOrDefault(organization => organization.Name == accountEntety.Organization);
            account.Organization = organization.Id;

            _accountManagementContext.SaveChanges();
        }

        public void DeleteAccount(Guid Id)
        {
            Account account = _accountManagementContext.Accounts.FirstOrDefault(account => account.Id == Id);
            _accountManagementContext.Accounts.Remove(account);
            _accountManagementContext.SaveChanges();
        }

        #region Argon2
        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }
        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
            }
            return buffer;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8, // four cores
                Iterations = 4,
                MemorySize = 100 * 100 // 1 GB
            };
            return argon2.GetBytes(16);
        }
        #endregion
    }
}
