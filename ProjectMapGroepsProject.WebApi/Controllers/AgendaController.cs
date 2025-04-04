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
    [Route("api/agenda")]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaRepository _repository;
        private readonly ILogger<AgendaController> _logger;
        private readonly IProfielKeuzeRepository _profielKeuzeRepository;

        public AgendaController(IAgendaRepository repository, ILogger<AgendaController> logger, IProfielKeuzeRepository profielKeuzeRepository)
        {
            _repository = repository;
            _logger = logger;
            _profielKeuzeRepository = profielKeuzeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agenda>>> GetAll()
        {
            var agendaItems = await _repository.ReadAsync();
            return Ok(agendaItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agenda>> Get(Guid id)
        {
            var agenda = await _repository.ReadAsync(id);
            if (agenda == null)
            {
                return NotFound();
            }
            return Ok(agenda);
        }

        [HttpGet("profielkeuze/{profielKeuzeId}")]
        public async Task<ActionResult<IEnumerable<Agenda>>> GetByProfielKeuzeId(Guid profielKeuzeId)
        {
            var agendaItems = await _repository.ReadByProfielKeuzeIdAsync(profielKeuzeId);
            return Ok(agendaItems);
        }

        [HttpPost]
        public async Task<ActionResult<Agenda>> Create([FromBody] Agenda agenda)
        {
            _logger.LogInformation("Creating a new agenda with ProfielKeuzeId: {ProfielKeuzeId}", agenda.ProfielKeuzeId);

            try
            {
                var profielKeuze = await _profielKeuzeRepository.ReadAsync(agenda.ProfielKeuzeId);
                if (profielKeuze == null)
                {
                    return BadRequest("Invalid ProfielKeuzeId.");
                }

                agenda.Id = Guid.NewGuid();
                var createdAgenda = await _repository.InsertAsync(agenda);
                return CreatedAtAction(nameof(Get), new { id = createdAgenda.Id }, createdAgenda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the agenda");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Agenda updatedAgenda)
        {
            var existingAgenda = await _repository.ReadAsync(id);
            if (existingAgenda == null)
            {
                return NotFound();
            }

            updatedAgenda.Id = id;
            await _repository.UpdateAsync(updatedAgenda);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingAgenda = await _repository.ReadAsync(id);
            if (existingAgenda == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
