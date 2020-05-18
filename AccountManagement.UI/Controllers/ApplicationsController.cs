using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.Library.API.Data;
using AccountManagement.Library.API.Models;
using AccountManagement.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountManagement.UI.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        readonly ILogger _logger;
        List<Application> Applications = new List<Application>();
        List<License> Licenses = new List<License>();
        readonly ApplicationService applicationService = new ApplicationService();
        readonly LicenseService licenseService = new LicenseService();

        public ApplicationsController(ILogger<ApplicationsController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index(int? page, string? message, string? messageType, int pageSize)
        {
            try
            {
                Applications = await applicationService.GetApplicationsAsync();
                Licenses = await licenseService.GetLicensesAsync();
                if (pageSize == 0)
                {
                    pageSize = 10;
                }

                var pager = new Pager(Applications.Count(), page, pageSize);

                var viewModel = new ApplicationViewModel
                {
                    Applications = Applications.Skip((pager.CurrentPage - 1) * (int)pager.PageSize).Take((int)pager.PageSize),
                    Licenses = Licenses,
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

        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                string messageType = "";
                string message = "";
                Guid ApplicationId = Guid.Parse(Id);

                string result = await applicationService.DeleteApplicationAsync(ApplicationId);
                if (result == "{\"message\":\"Application deleted\"}")
                {
                    messageType = "Success";
                    message = "Application deleted";
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

        public async Task<IActionResult> ApplicationAction(string actionbtn, string name, string Id)
        {
            try
            {
                string messageType = "";
                string message = "";
                Application application = new Application
                {
                    Name = name
                };
                if (actionbtn == "Add")
                {
                    string result = await applicationService.AddApplicationAsync(application);
                    if (result == "{\"message\":\"Application added\"}")
                    {
                        message = $"Application {application.Name} Added!";
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
                    application.Id = Guid.Parse(Id);
                    string result = await applicationService.UpdateApplicationAsync(application);
                    if (result == "{\"message\":\"Application updated\"}")
                    {
                        message = $"Application updated!";
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


        public async Task<IActionResult> AddLicenses(string App)
        {
            try
            {
                string messageType = "";
                string message = "";
                const int amount = 10;
                if (await licenseService.AddLicensesForApplicationAsync(amount, App) == "{\"message\":\"Licenses added\"}")
                {
                    messageType = "Success";
                    message = $"10 licenses added for {App}";
                    return RedirectToAction("Index", new { page = 0, message, messageType, pageSize = 0 });
                }
                else
                {
                    messageType = "Error";
                    message = $"Cannot add licenses to {App} please try again";
                    return RedirectToAction("Index", new { page = 0, message, messageType, pageSize = 0 });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Message {ex.Message}, StackTrace {ex.StackTrace} Inner {ex.InnerException}");
                return RedirectToAction("Index", "Dashboard", null);
            }
        }
    }
}