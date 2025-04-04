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
    public class Progressie2Controller : ControllerBase
    {
        private readonly IProgressie2Repository _progressie2Repository;
        private readonly ILogger<Progressie2Controller> _logger;

        public Progressie2Controller(IProgressie2Repository progressie2Repository, ILogger<Progressie2Controller> logger)
        {
            _progressie2Repository = progressie2Repository;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllProgressie2")]
        public async Task<ActionResult<IEnumerable<Progressie2>>> GetAll()
        {
            try
            {
                var progressies = await _progressie2Repository.GetAllAsync();
                return Ok(progressies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all Progressie2 records.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetProgressie2ById")]
        public async Task<ActionResult<Progressie2>> Get(Guid id)
        {
            try
            {
                var progressie = await _progressie2Repository.GetByIdAsync(id);
                if (progressie == null)
                    return NotFound();

                return Ok(progressie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting Progressie2 record.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost(Name = "CreateProgressie2")]
        public async Task<ActionResult> Create([FromBody] Progressie2 progressie)
        {
            try
            {
                progressie.Id = Guid.NewGuid(); // Set a new Guid for the Progressie2 record
                var createdProgressie = await _progressie2Repository.CreateAsync(progressie);
                return CreatedAtRoute("GetProgressie2ById", new { id = createdProgressie.Id }, createdProgressie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating Progressie2.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}", Name = "UpdateProgressie2")]
        public async Task<ActionResult> Update(Guid id, [FromBody] Progressie2 updatedProgressie)
        {
            try
            {
                var existingProgressie = await _progressie2Repository.GetByIdAsync(id);

                if (existingProgressie == null)
                    return NotFound();

                await _progressie2Repository.UpdateAsync(id, updatedProgressie);
                return Ok(updatedProgressie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating Progressie2.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}", Name = "DeleteProgressie2")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingProgressie = await _progressie2Repository.GetByIdAsync(id);

                if (existingProgressie == null)
                    return NotFound();

                await _progressie2Repository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Progressie2.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
