using System;
using AccountManagement.Library.API.Data;
using AccountManagement.Library.API.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AccountManagement.Azure.Functions
{
    public static class AddLicense
    {
        [FunctionName("AddLicense")]
        public static async void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            LicenseService licenseService = new LicenseService();
            await licenseService.AddLicensesAsync(10);
        }
    }
}
