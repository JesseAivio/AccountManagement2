using AccountManagement.Library.API.http;
using AccountManagement.Library.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Library.API.Data
{
    public class LicenseService
    {
        public async Task<List<License>> GetLicensesAsync()
        {
            List<License> licenses = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(URL.GetLicenses);
            if (response.IsSuccessStatusCode)
            {
                licenses = await response.Content.ReadAsAsync<List<License>>();
            }
            return licenses;
        }

        public async Task<string> AddLicensesAsync(int amount)
        {
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync($"{URL.AddLicense}/{amount}");
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> AddLicensesForApplicationAsync(int amount, string application)
        {
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync($"{URL.AddLicense}/{amount}/{application}");
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> UpdateLicenseAsync(License license)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PutAsJsonAsync(URL.UpdateLicense, license);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> DeleteLicenseAsync(Guid Id)
        {
            HttpResponseMessage response = await HttpClientSettings.client.DeleteAsync($"{URL.DeleteLicense}{Id}");
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
