using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.API.Data.Enteties;
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
    public class AccountLogController : ControllerBase
    {
        readonly ILogger _logger;
        readonly IAccountLogService _accountLogService;
        public AccountLogController(ILogger<AccountLogController> logger, IAccountLogService accountLogService)
        {
            _logger = logger;
            _accountLogService = accountLogService;
        }

        [HttpGet("AccountLogs")]
        public IActionResult GetAccountLogs()
        {
            try
            {
                return Ok(_accountLogService.GetAccountLogs());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpPost("AddAccountLog")]
        public IActionResult AddAccountLog([FromBody] AccountLogEntity accountLogEntity)
        {
            try
            {
                if (accountLogEntity == null)
                    return BadRequest(new { error = "Input was null" });

                _accountLogService.AddAccountLog(accountLogEntity);
                return Ok(new { message = "AccountLog added" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }
    }
}