using AccountManagement.Library.API.http;
using AccountManagement.Library.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Library.API.Data
{
    public class ApplicationService
    {
        public async Task<List<Application>> GetApplicationsAsync()
        {
            List<Application> applications = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(URL.GetApplications);
            if (response.IsSuccessStatusCode)
            {
                applications = await response.Content.ReadAsAsync<List<Application>>();
            }
            return applications;
        }

        public async Task<string> AddApplicationAsync(Application application)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(URL.AddApplication, application);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> UpdateApplicationAsync(Application application)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PutAsJsonAsync(URL.UpdateApplication, application);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> DeleteApplicationAsync(Guid Id)
        {
            HttpResponseMessage response = await HttpClientSettings.client.DeleteAsync($"{URL.DeleteApplication}{Id}");
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
