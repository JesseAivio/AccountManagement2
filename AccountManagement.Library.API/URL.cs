using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Library.API
{
    public class URL
    {
        public const string BaseAddress = "https://accountmanagement-api.azurewebsites.net/";

        public const string GetAccounts = "Account/Accounts";

        public const string GetOrganizations = "Organization/Organizations";

        public const string AddAccount = "Account/AddAccount";

        public const string AddOrganization = "Organization/AddOrganization";

        public const string UpdateAccount = "Account/UpdateAccount";

        public const string UpdateOrganization = "Organization/UpdateOrganization";

        public const string GetOrganizationSettings = "Organization/OrganizationSettings";

        public const string UpdateOrganizationSettings = "Organization/ChangeSettings";

        public const string DeleteAccount = "Account/DeleteAccount/";

        public const string DeleteOrganization = "Organization/DeleteOrganization/";

        public const string GetApplications = "Application/Applications";

        public const string AddApplication = "Application/AddApplication";

        public const string UpdateApplication = "Application/UpdateApplication";

        public const string DeleteApplication = "Application/DeleteApplication/";

        public const string GetLicenses = "License/Licenses";

        public const string AddLicense = "License/AddLicense";

        public const string UpdateLicense = "License/UpdateLicense";

        public const string DeleteLicense = "License/DeleteLicense/";

        public const string GetApplicationOrganizations = "ApplicationOrganizations/ApplicationOrganizations";

        public const string AddApplicationOrganization = "ApplicationOrganizations/AddApplicationOrganization";

        public const string GetAccountLogs = "AccountLog/AccountLogs";

        public const string AddAccountLog = "AccountLog/AddAccountLog";
    }
}
