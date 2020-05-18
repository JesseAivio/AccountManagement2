using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using AccountManagement.Library.API.Data;
using AccountManagement.Library.API.http;
using AccountManagement.Library.API.Models;
using Microsoft.Identity.Client;
using Xunit;

namespace AccountManagement.Library.API.Tests
{
    public class AccountLogServiceTests
    {
        AccountLogService AccountLogService = new AccountLogService();
        [Fact]
        public async void AddAccountLogAsyncTest()
        {
            GetYourTokenWithClientCredentialsFlow();
            // Arrange
            string Expected = "AccountLog added";

            // Act
            AccountLog accountLog = new AccountLog
            {
                Account = "",
                Application = "",
                wasSuccessful = true,
                Date = DateTime.Now,
                HWID = "",
                IpAddress = "1.3.3.7"
            };
            string Actual = await AccountLogService.AddAccountLogAsync(accountLog);

            // Assert
            Assert.Equal(Expected, Actual);
        }

        private async void GetYourTokenWithClientCredentialsFlow()
        {
            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create("fa5c888b-794a-4d8e-9e17-040c6e2d1756")
                .WithClientSecret("_38uzSMoe72mTbbuqcOaL?vwFkt@lvC_")
                .WithAuthority(new Uri("https://login.microsoftonline.com/ad83df42-aaaa-4586-a1bc-14a3324f2b1c"))
                .Build();

            string[] ResourceIds = new string[] { "api://13984e32-cf0c-44de-8f25-cf4cc775e718/.default" };
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
