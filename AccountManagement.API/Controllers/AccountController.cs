using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.API.Data.Enteties;
using AccountManagement.API.Data.Models;
using AccountManagement.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountManagement.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IAccountService _accountService;
        readonly ILogger _logger;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet("Accounts")]
        public IActionResult GetAccount()
        {
            try
            {
                return Ok(_accountService.GetAccounts());
            }
            catch(Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new {error = "Error" });
            }
        }    

        [HttpPost("AddAccount")]
        public IActionResult AddAccount([FromBody] AccountEntity accountEntety)
        {
            try
            {
                if (accountEntety == null)
                    return BadRequest(new { error = "Input was null"});

                _accountService.AddAccount(accountEntety);
                return Ok(new {message = "Account added" });
            }
            catch(Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }       

        [HttpPut("UpdateAccount")]
        public IActionResult UpdateAccount([FromBody] AccountEntity accountEntety)
        {
            try
            {
                if (accountEntety == null)
                    return BadRequest(new { error = "Input was null" });

                _accountService.UpdateAccount(accountEntety);
                return Ok(new { message = "Account updated" });
            }
            catch(Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpDelete("DeleteAccount/{Id}")]
        public IActionResult DeleteAccount(Guid Id)
        {
            try
            {
                if(Id == null)
                    return BadRequest(new { error = "Input was null" });

                _accountService.DeleteAccount(Id);

                return Ok(new { message = "Account deleted" });
            }
            catch(Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }
    }
}