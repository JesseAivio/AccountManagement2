using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
using GemBox.Spreadsheet;
using System.IO;
using AccountManagement.Library.API.Data;
using AccountManagement.Library.API.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace AccountManagement.UI.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        readonly ILogger _logger;
        List<Account> accounts = new List<Account>();
        List<Organization> organizations = new List<Organization>();
        List<AccountLog> accountLogs = new List<AccountLog>();
        readonly AccountsService accountsService = new AccountsService();
        readonly OrganizationsService organizationsService = new OrganizationsService();
        readonly AccountLogService accountLogService = new AccountLogService();

        public AccountsController(ILogger<AccountsController> logger)
        {
            _logger = logger;
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public async Task<IActionResult> Index(int? page, string? message, string? messageType, int pageSize)
        {
            try
            {
                accounts = await accountsService.GetAccountsAsync();
                organizations = await organizationsService.GetOrganizationsAsync();
                accountLogs = await accountLogService.GetAccountLogsAsync();
                if (pageSize == 0)
                {
                    pageSize = 10;
                }

                var pager = new Pager(accounts.Count(), page, pageSize);

                var viewModel = new AccountViewModel
                {
                    Accounts = accounts.Skip((pager.CurrentPage - 1) * (int)pager.PageSize).Take((int)pager.PageSize),
                    Organizations = organizations,
                    AccountLogs = accountLogs,
                    Pager = pager
                };
                ViewData["Message"] = message;
                ViewData["messageType"] = messageType;
                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Message {ex.Message}, StackTrace {ex.StackTrace} Inner {ex.InnerException}");
                return RedirectToAction("Index", "Dashboard", null);
            }
            
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadAccounts()
        {
            try
            {
                accounts = await accountsService.GetAccountsAsync();
                var book = new ExcelFile();
                var sheet = book.Worksheets.Add("Accounts");

                CellStyle style = sheet.Rows[0].Style;
                style.Font.Weight = ExcelFont.BoldWeight;
                style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                sheet.Columns[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

                sheet.Columns[0].SetWidth(50, LengthUnit.Pixel);
                sheet.Columns[1].SetWidth(150, LengthUnit.Pixel);
                sheet.Columns[2].SetWidth(150, LengthUnit.Pixel);

                sheet.Cells["A1"].Value = "Username";
                sheet.Cells["B1"].Value = "Email";
                sheet.Cells["C1"].Value = "PhoneNumber";
                sheet.Cells["D1"].Value = "Role";
                sheet.Cells["E1"].Value = "Organization";

                for (int r = 1; r <= accounts.Count; r++)
                {
                    Account account = accounts[r - 1];
                    sheet.Cells[r, 0].Value = account.Username;
                    sheet.Cells[r, 1].Value = account.Email;
                    sheet.Cells[r, 2].Value = account.PhoneNumber;
                    sheet.Cells[r, 3].Value = account.Role;
                    sheet.Cells[r, 4].Value = account.Organization;
                }

                using (var stream = new MemoryStream())
                {
                    book.Save(stream, SaveOptions.XlsxDefault);
                    return File(stream.ToArray(), SaveOptions.XlsxDefault.ContentType, "Accounts.xlsx");
                }
            }
            catch(Exception ex)
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
                Guid AccountId = Guid.Parse(Id);

                string result = await accountsService.DeleteAccountAsync(AccountId);
                if (result == "{\"message\":\"Account deleted\"}")
                {
                    messageType = "Success";
                    message = "Account deleted";
                }
                else
                {
                    messageType = "Error";
                    message = result;
                }
                return RedirectToAction("Index", new { page = 0, message, messageType, pageSize = 0 });
            }
            catch(Exception ex)
            {
                _logger.LogError($"Message {ex.Message}, StackTrace {ex.StackTrace} Inner {ex.InnerException}");
                return RedirectToAction("Index", "Dashboard", null);
            }
        }

        public async Task<IActionResult> AccountAction(string actionbtn, string username, string email, string password, string phonenumber, string role, string organization, string Id)
        {
            try
            {
                string messageType = "";
                string message = "";
                Account account = new Account
                {
                    Username = username,
                    Email = email,
                    Password = password,
                    PhoneNumber = phonenumber,
                    Role = role,
                    Organization = organization
                };
                if (actionbtn == "Add")
                {
                    string result = await accountsService.AddAccountAsync(account);
                    if (result == "{\"message\":\"Account added\"}")
                    {
                        message = $"Account {account.Username} Added!";
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
                    account.Id = Guid.Parse(Id);
                    string result = await accountsService.UpdateAccountAsync(account);
                    if (result == "{\"message\":\"Account updated\"}")
                    {
                        message = $"Account updated!";
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
    }
}