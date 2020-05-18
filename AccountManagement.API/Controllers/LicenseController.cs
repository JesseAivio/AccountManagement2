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
    public class LicenseController : ControllerBase
    {
        readonly ILogger _logger;
        readonly ILicenseService _licenseService;
        public LicenseController(ILogger<LicenseController> logger, ILicenseService licenseService)
        {
            _logger = logger;
            _licenseService = licenseService;
        }

        [HttpGet("Licenses")]
        public IActionResult GetLicenses()
        {
            try
            {
                return Ok(_licenseService.GetLicenses());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpGet("AddLicense/{amount}")]
        public IActionResult AddLicense(int amount)
        {
            try
            {
                _licenseService.AddLicenses(amount);
                return Ok(new { message = "Licenses added" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption:{ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpGet("AddLicense/{amount}/{application}")]
        public IActionResult AddLicensesForApplication(int amount, string application)
        {
            try
            {
                _licenseService.AddLicensesForApplication(amount, application);
                return Ok(new { message = "Licenses added" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption:{ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpPut("UpdateLicense")]
        public IActionResult UpdateLicense([FromBody] LicenseEntity licenseEntity)
        {
            try
            {
                if (licenseEntity == null)
                    return BadRequest(new { error = "Input was null" });

                _licenseService.UpdateLicense(licenseEntity);
                return Ok(new { message = "License updated" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpDelete("DeleteLicense/{Id}")]
        public IActionResult DeleteLicense(Guid Id)
        {
            try
            {
                if (Id == null)
                    return BadRequest(new { error = "Input was null" });

                _licenseService.DeleteLicense(Id);

                return Ok(new { message = "License deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }
    }
}