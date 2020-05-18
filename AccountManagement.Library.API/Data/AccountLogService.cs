using AccountManagement.Library.API.http;
using AccountManagement.Library.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Library.API.Data
{
    public class AccountLogService
    {
        public async Task<List<AccountLog>> GetAccountLogsAsync()
        {
            List<AccountLog> accountLogs = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(URL.GetAccountLogs);
            if (response.IsSuccessStatusCode)
            {
                accountLogs = await response.Content.ReadAsAsync<List<AccountLog>>();
            }
            return accountLogs;
        }

        public async Task<string> AddAccountLogAsync(AccountLog accountLog)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(URL.AddAccountLog, accountLog);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
