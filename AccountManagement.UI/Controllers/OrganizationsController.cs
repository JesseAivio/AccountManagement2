using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.Library.API.Data;
using AccountManagement.Library.API.Models;
using AccountManagement.UI.Models;
using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountManagement.UI.Controllers
{
    [Authorize]
    public class OrganizationsController : Controller
    {
        readonly ILogger _logger;
        List<Organization> organizations = new List<Organization>();
        List<ApplicationOrganizations> applicationOrganizations = new List<ApplicationOrganizations>();
        List<Application> applications = new List<Application>();
        List<License> licenses = new List<License>();
        readonly OrganizationsService organizationsService = new OrganizationsService();
        readonly ApplicationOrganizationsService applicationOrganizationsService = new ApplicationOrganizationsService();
        readonly ApplicationService applicationService = new ApplicationService();
        readonly LicenseService licenseService = new LicenseService();

        public OrganizationsController(ILogger<OrganizationsController> logger)
        {
            _logger = logger;
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public async Task<IActionResult> Index(int? page, string? message, string? messageType, int pageSize)
        {
            try
            {
                organizations = await organizationsService.GetOrganizationsAsync();
                applicationOrganizations = await applicationOrganizationsService.GetApplicationOrganizationsAsync();
                applications = await applicationService.GetApplicationsAsync();
                licenses = await licenseService.GetLicensesAsync();
                if (pageSize == 0)
                {
                    pageSize = 10;
                }

                var pager = new Pager(organizations.Count(), page, pageSize);

                var viewModel = new OrganizationViewModel
                {
                    Organizations = organizations.Skip((pager.CurrentPage - 1) * (int)pager.PageSize).Take((int)pager.PageSize),
                    ApplicationOrganizations = applicationOrganizations,
                    Applications = applications,
                    Licenses = licenses,
                    Pager = pager
                };
                ViewData["Message"] = message;
                ViewData["messageType"] = messageType;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message {ex.Message}, StackTrace {ex.StackTrace} Inner {ex.InnerException}");
                return RedirectToAction("Index", "Dashboard", null);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DownloadOrganizations()
        {
            try
            {
                var book = new ExcelFile();
                var sheet = book.Worksheets.Add("Organizations");

                CellStyle style = sheet.Rows[0].Style;
                style.Font.Weight = ExcelFont.BoldWeight;
                style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                sheet.Columns[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

                sheet.Columns[0].SetWidth(50, LengthUnit.Pixel);
                sheet.Columns[1].SetWidth(150, LengthUnit.Pixel);
                sheet.Columns[2].SetWidth(150, LengthUnit.Pixel);

                sheet.Cells["A1"].Value = "Name";
                sheet.Cells["B1"].Value = "Business Id";
                sheet.Cells["C1"].Value = "Info";
                sheet.Cells["D1"].Value = "Email";

                for (int r = 1; r <= organizations.Count; r++)
                {
                    Organization organization = organizations[r - 1];
                    sheet.Cells[r, 0].Value = organization.Name;
                    sheet.Cells[r, 1].Value = organization.BusinessId;
                    sheet.Cells[r, 2].Value = organization.Info;
                    sheet.Cells[r, 3].Value = organization.Email;
                }

                using (var stream = new MemoryStream())
                {
                    book.Save(stream, SaveOptions.XlsxDefault);
                    return File(stream.ToArray(), SaveOptions.XlsxDefault.ContentType, "Organizations.xlsx");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message {ex.Message}, StackTrace {ex.StackTrace} Inner {ex.InnerException}");
                return RedirectToAction("Index", "Dashboard", null);
            }
        }
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                string messageType = "";
                string message = "";
                Guid OrganizationId = Guid.Parse(Id);

                string result = await organizationsService.DeleteOrganizationAsync(OrganizationId);
                if (result == "{\"message\":\"Organization deleted\"}")
                {
                    messageType = "Success";
                    message = "Organization deleted";
                }
                else
                {
                    messageType = "Error";
                    message = result;
                }
                return RedirectToAction("Index", new { page = 0, message, messageType, pageSize = 0 });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message {ex.Message}, StackTrace {ex.StackTrace} Inner {ex.InnerException}");
                return RedirectToAction("Index", "Dashboard", null);
            }
        }

        public async Task<IActionResult> OrganizationAction(string actionbtn, string name, string businessid, string info, string email, string Id)
        {
            try
            {
                string messageType = "";
                string message = "";
                Organization organization = new Organization
                {
                    Name = name,
                    BusinessId = businessid,
                    Info = info,
                    Email = email
                };
                if (actionbtn == "Add")
                {
                    string result = await organizationsService.AddOrganizationAsync(organization);
                    if (result == "{\"message\":\"Organization added\"}")
                    {
                        message = $"Organization {organization.Name} Added!";
                        messageType = "Success";
                    }
                    else
                    {
                        message = result;
                        messageType = "Error";
                    }
                }
                else
                {
                    organization.Id = Guid.Parse(Id);
                    string result = await organizationsService.UpdateOrganizationAsync(organization);
                    if (result == "{\"message\":\"Organization updated\"}")
                    {
                        message = $"Organization updated!";
                        messageType = "Success";
                    }
                    else
                    {
                        message = result;
                        messageType = "Error";
                    }
                }
                return RedirectToAction("Index", new { page = 0, message, messageType, pageSize = 0 });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message {ex.Message}, StackTrace {ex.StackTrace} Inner {ex.InnerException}");
                return RedirectToAction("Index", "Dashboard", null);
            }
        }

        public async Task<IActionResult> AddApplicationForOrganization(string application, string organization)
        {
            string message;
            string messageType;
            ApplicationOrganizations applicationOrganization = new ApplicationOrganizations
            {
                Application = application,
                License = "lol",
                Organization = organization
            };
            if(await applicationOrganizationsService.AddApplicationOrganizationAsync(applicationOrganization) == "{\"message\":\"ApplicationOrganizations added\"}")
            {
                message = $"{application} added for {organization}";
                messageType = "Success";
                return RedirectToAction("Index", new { page = 0, message, messageType, pageSize = 0 });
            }
            else
            {
                message = $"Cant add {application} for {organization}";
                messageType = "Error";
                return RedirectToAction("Index", new { page = 0, message, messageType, pageSize = 0 });
            }
            
        }
    }
}