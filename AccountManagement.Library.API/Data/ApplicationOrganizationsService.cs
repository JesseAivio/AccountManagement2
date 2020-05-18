using AccountManagement.Library.API.http;
using AccountManagement.Library.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Library.API.Data
{
    public class ApplicationOrganizationsService
    {
        public async Task<List<ApplicationOrganizations>> GetApplicationOrganizationsAsync()
        {
            List<ApplicationOrganizations> applicationOrganizations = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(URL.GetApplicationOrganizations);
            if (response.IsSuccessStatusCode)
            {
                applicationOrganizations = await response.Content.ReadAsAsync<List<ApplicationOrganizations>>();
            }
            return applicationOrganizations;
        }

        public async Task<string> AddApplicationOrganizationAsync(ApplicationOrganizations applicationOrganizations)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(URL.AddApplicationOrganization, applicationOrganizations);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
