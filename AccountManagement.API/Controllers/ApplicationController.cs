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
    public class ApplicationController : ControllerBase
    {
        readonly ILogger _logger;
        readonly IApplicationService _applicationService;
        public ApplicationController(ILogger<ApplicationController> logger, IApplicationService applicationService)
        {
            _logger = logger;
            _applicationService = applicationService;
        }

        [HttpGet("Applications")]
        public IActionResult GetApplications()
        {
            try
            {
                return Ok(_applicationService.GetApplications());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpPost("AddApplication")]
        public IActionResult AddApplication([FromBody] Application application)
        {
            try
            {
                if (application == null)
                    return BadRequest(new { error = "Input was null" });

                _applicationService.AddApplication(application);
                return Ok(new { message = "Application added" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpPut("UpdateApplication")]
        public IActionResult UpdateApplication([FromBody] Application application)
        {
            try
            {
                if (application == null)
                    return BadRequest(new { error = "Input was null" });

                _applicationService.UpdateApplication(application);
                return Ok(new { message = "Application updated" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }

        [HttpDelete("DeleteApplication/{Id}")]
        public IActionResult DeleteApplication(Guid Id)
        {
            try
            {
                if (Id == null)
                    return BadRequest(new { error = "Input was null" });

                _applicationService.DeleteApplication(Id);

                return Ok(new { message = "Application deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}, StackTracec:{ex.StackTrace}, InnerExeption: {ex.InnerException}");
                return BadRequest(new { error = "Error" });
            }
        }
    }
}