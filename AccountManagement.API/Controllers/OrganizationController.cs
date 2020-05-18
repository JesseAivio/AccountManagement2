using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class OrganizationController : ControllerBase
    {
        readonly ILogger _logger;
        readonly IOrganizationService _organizationService;
        public OrganizationController(ILogger<OrganizationController> logger, IOrganizationService organizationService)
        {
            _logger = logger;
            _organizationService = organizationService;
        }

        [HttpGet("Organizations")]
        public IActionResult GetOrganizations()
        {
            try
            {
                return Ok(_organizationService.GetOrganizations());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpPost("AddOrganization")]
        public IActionResult AddOrganization([FromBody] Organization organization)
        {
            try
            {
                if (organization == null)
                    return BadRequest(new { error = "Input was null" });

                _organizationService.AddOrganization(organization);
                return Ok(new { message = "Organization added" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpPut("UpdateOrganization")]
        public IActionResult UpdateOrganization([FromBody] Organization organization)
        {
            try
            {
                if (organization == null)
                    return BadRequest(new { error = "Input was null" });

                _organizationService.UpdateOrganization(organization);
                return Ok(new { message = "Organization updated" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpDelete("DeleteOrganization/{Id}")]
        public IActionResult DeleteOrganization(Guid Id)
        {
            try
            {
                if (Id == null)
                    return BadRequest(new { error = "Input was null" });

                _organizationService.DeleteOrganization(Id);

                return Ok(new { message = "Organization deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }
    }
}