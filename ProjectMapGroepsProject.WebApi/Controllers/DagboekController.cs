using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectMapGroepsproject.WebApi.Models;
using ProjectMap.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMap.WebApi.Controllers
{
    [ApiController]
    [Route("api/dagboeken")]
    public class DagboekController : ControllerBase
    {
        private readonly IDagboekRepository _repository;
        private readonly ILogger<DagboekController> _logger;

        public DagboekController(IDagboekRepository repository, ILogger<DagboekController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dagboek>>> GetAll()
        {
            var dagboekItems = await _repository.ReadAsync();
            return Ok(dagboekItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dagboek>> Get(Guid id)
        {
            var dagboek = await _repository.ReadAsync(id);
            if (dagboek == null)
            {
                return NotFound();
            }
            return Ok(dagboek);
        }

        [HttpGet("profielkeuze/{profielKeuzeId}")]
        public async Task<ActionResult<IEnumerable<Dagboek>>> GetByProfielKeuzeId(Guid profielKeuzeId)
        {
            var dagboekItems = await _repository.ReadByProfielKeuzeIdAsync(profielKeuzeId);
            return Ok(dagboekItems);
        }

        [HttpPost]
        public async Task<ActionResult<Dagboek>> Create([FromBody] Dagboek dagboek)
        {
            _logger.LogInformation("Creating a new dagboek with ProfielKeuzeId: {ProfielKeuzeId}", dagboek.ProfielKeuzeId);

            try
            {
                var createdDagboek = await _repository.InsertAsync(dagboek);
                return CreatedAtAction(nameof(Get), new { id = createdDagboek.Id }, createdDagboek);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the dagboek");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Dagboek updatedDagboek)
        {
            var existingDagboek = await _repository.ReadAsync(id);
            if (existingDagboek == null)
            {
                return NotFound();
            }

            updatedDagboek.Id = id;
            await _repository.UpdateAsync(updatedDagboek);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingDagboek = await _repository.ReadAsync(id);
            if (existingDagboek == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
