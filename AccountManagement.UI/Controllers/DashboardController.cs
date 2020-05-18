using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AccountManagement.Library.API.http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace AccountManagement.UI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public DashboardController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IActionResult Index()
        {
            GetYourTokenWithClientCredentialsFlow();
            return View();
        }

        private async void GetYourTokenWithClientCredentialsFlow()
        {
            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create(Configuration.GetValue<string>("ClientId"))
                .WithClientSecret(Configuration.GetValue<string>("ClientSecret"))
                .WithAuthority(new Uri(Configuration.GetValue<string>("Instance")))
                .Build();

            string[] ResourceIds = new string[] { Configuration.GetValue<string>("ResourceId") };
            try
            {
                AuthenticationResult result = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Token acquired \n");
                Console.WriteLine(result.AccessToken);
                Console.ResetColor();
                HttpClientSettings.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            }
            catch (MsalClientException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}