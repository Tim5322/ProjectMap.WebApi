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
        private readonly AgendaRepository _repository;
        private readonly ILogger<AgendaController> _logger;

        public AgendaController(AgendaRepository repository, ILogger<AgendaController> logger)
        {
            _repository = repository;
            _logger = logger;
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

        [HttpPost]
        public async Task<ActionResult<Agenda>> Create([FromBody] Agenda agenda)
        {
            var createdAgenda = await _repository.InsertAsync(agenda);
            return CreatedAtAction(nameof(Get), new { id = createdAgenda.Id }, createdAgenda);
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
