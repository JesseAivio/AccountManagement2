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
    public class ApplicationOrganizationsController : ControllerBase
    {
        readonly ILogger _logger;
        readonly IApplicationOrganizationsService _applicationOrganizationsService;
        public ApplicationOrganizationsController(ILogger<ApplicationOrganizationsController> logger, IApplicationOrganizationsService applicationOrganizationsService)
        {
            _logger = logger;
            _applicationOrganizationsService = applicationOrganizationsService;
        }

        [HttpGet("ApplicationOrganizations")]
        public IActionResult GetApplicationOrganizations()
        {
            try
            {
                return Ok(_applicationOrganizationsService.GetApplicationOrganizations());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpPost("AddApplicationOrganization")]
        public IActionResult AddApplicationOrganization([FromBody] ApplicationOrganizationsEntity applicationOrganizationsEntity)
        {
            try
            {
                if (applicationOrganizationsEntity == null)
                    return BadRequest(new { error = "Input was null" });

                _applicationOrganizationsService.AddApplicationOrganization(applicationOrganizationsEntity);
                return Ok(new { message = "ApplicationOrganizations added" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }
    }
}