using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectMapGroepsproject.WebApi.Models;
using ProjectMapGroepsproject.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMapGroepsproject.WebApi.Controllers
{
    [ApiController]
    [Route("api/progressie1")]
    public class Progressie1Controller : ControllerBase
    {
        private readonly IProgressie1Repository _progressie1Repository;
        private readonly ILogger<Progressie1Controller> _logger;
        private readonly IAuthenticationService _authenticationService;

        public Progressie1Controller(IProgressie1Repository progressie1Repository, ILogger<Progressie1Controller> logger, IAuthenticationService authenticationService)
        {
            _progressie1Repository = progressie1Repository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Progressie1>>> GetAll()
        {
            try
            {
                var userId = _authenticationService.GetCurrentAuthenticatedUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                var progressies = await _progressie1Repository.GetAllAsync();
                return Ok(progressies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all Progressie1 records.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Progressie1>> Get(Guid id)
        {
            try
            {
                var progressie = await _progressie1Repository.GetByIdAsync(id);
                if (progressie == null)
                {
                    return NotFound();
                }

                return Ok(progressie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting Progressie1 record.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Progressie1>> Create([FromBody] Progressie1 progressie)
        {
            try
            {
                progressie.Id = Guid.NewGuid(); // Set a new Guid for the Progressie1 record
                var createdProgressie = await _progressie1Repository.CreateAsync(progressie);
                return CreatedAtAction(nameof(Get), new { id = createdProgressie.Id }, createdProgressie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating Progressie1.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Progressie1 updatedProgressie)
        {
            try
            {
                var existingProgressie = await _progressie1Repository.GetByIdAsync(id);
                if (existingProgressie == null)
                {
                    return NotFound();
                }

                updatedProgressie.Id = id;
                await _progressie1Repository.UpdateAsync(id, updatedProgressie);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating Progressie1.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingProgressie = await _progressie1Repository.GetByIdAsync(id);
                if (existingProgressie == null)
                {
                    return NotFound();
                }

                await _progressie1Repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Progressie1.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

