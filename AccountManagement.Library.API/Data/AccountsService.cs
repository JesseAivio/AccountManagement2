using AccountManagement.Library.API.http;
using AccountManagement.Library.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Library.API.Data
{
    public class AccountsService
    {
        public async Task<List<Account>> GetAccountsAsync()
        {
            List<Account> accounts = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(URL.GetAccounts);
            if (response.IsSuccessStatusCode)
            {
                accounts = await response.Content.ReadAsAsync<List<Account>>();
            }
            return accounts;
        }

        public async Task<string> AddAccountAsync(Account account)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(URL.AddAccount, account);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> UpdateAccountAsync(Account account)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PutAsJsonAsync(URL.UpdateAccount, account);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> DeleteAccountAsync(Guid Id)
        {
            HttpResponseMessage response = await HttpClientSettings.client.DeleteAsync($"{URL.DeleteAccount}{Id}");
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
