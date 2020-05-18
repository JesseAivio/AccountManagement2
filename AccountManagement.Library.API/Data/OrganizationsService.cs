using AccountManagement.Library.API.http;
using AccountManagement.Library.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Library.API.Data
{
    public class OrganizationsService
    {
        public async Task<List<Organization>> GetOrganizationsAsync()
        {
            List<Organization> organizations = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(URL.GetOrganizations);
            if (response.IsSuccessStatusCode)
            {
                organizations = await response.Content.ReadAsAsync<List<Organization>>();
            }
            return organizations;
        }

        public async Task<string> AddOrganizationAsync(Organization organization)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(URL.AddOrganization, organization);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> UpdateOrganizationAsync(Organization organization)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PutAsJsonAsync(URL.UpdateOrganization, organization);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> DeleteOrganizationAsync(Guid Id)
        {
            HttpResponseMessage response = await HttpClientSettings.client.DeleteAsync($"{URL.DeleteOrganization}{Id}");
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
