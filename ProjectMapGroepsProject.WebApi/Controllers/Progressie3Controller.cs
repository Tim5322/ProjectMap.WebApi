using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectMapGroepsproject.WebApi.Models;
using ProjectMapGroepsproject.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMapGroepsproject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Progressie3Controller : ControllerBase
    {
        private readonly IProgressie3Repository _progressie3Repository;
        private readonly ILogger<Progressie3Controller> _logger;

        public Progressie3Controller(IProgressie3Repository progressie3Repository, ILogger<Progressie3Controller> logger)
        {
            _progressie3Repository = progressie3Repository;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllProgressie3")]
        public async Task<ActionResult<IEnumerable<Progressie3>>> GetAll()
        {
            try
            {
                var progressies = await _progressie3Repository.GetAllAsync();
                return Ok(progressies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all Progressie3 records.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetProgressie3ById")]
        public async Task<ActionResult<Progressie3>> Get(Guid id)
        {
            try
            {
                var progressie = await _progressie3Repository.GetByIdAsync(id);
                if (progressie == null)
                    return NotFound();

                return Ok(progressie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting Progressie3 record.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost(Name = "CreateProgressie3")]
        public async Task<ActionResult> Create([FromBody] Progressie3 progressie)
        {
            try
            {
                progressie.Id = Guid.NewGuid(); // Set a new Guid for the Progressie3 record
                var createdProgressie = await _progressie3Repository.CreateAsync(progressie);
                return CreatedAtRoute("GetProgressie3ById", new { id = createdProgressie.Id }, createdProgressie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating Progressie3.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}", Name = "UpdateProgressie3")]
        public async Task<ActionResult> Update(Guid id, [FromBody] Progressie3 updatedProgressie)
        {
            try
            {
                var existingProgressie = await _progressie3Repository.GetByIdAsync(id);

                if (existingProgressie == null)
                    return NotFound();

                await _progressie3Repository.UpdateAsync(id, updatedProgressie);
                return Ok(updatedProgressie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating Progressie3.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}", Name = "DeleteProgressie3")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingProgressie = await _progressie3Repository.GetByIdAsync(id);

                if (existingProgressie == null)
                    return NotFound();

                await _progressie3Repository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Progressie3.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
