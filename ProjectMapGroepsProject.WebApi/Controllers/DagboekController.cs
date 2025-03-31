using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectMapGroepsproject.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMap.WebApi.Controllers
{
    [ApiController]
    [Route("dagboeken")]
    public class DagboekController : ControllerBase
    {
        private static readonly Dictionary<Guid, Dagboek> _dagboeken = new();
        private readonly ILogger<DagboekController> _logger;

        public DagboekController(ILogger<DagboekController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Dagboek>> GetAll()
        {
            return Ok(_dagboeken.Values);
        }

        [HttpGet("{id}")]
        public ActionResult<Dagboek> Get(Guid id)
        {
            if (!_dagboeken.TryGetValue(id, out var dagboek))
            {
                return NotFound();
            }
            return Ok(dagboek);
        }

        [HttpPost]
        public ActionResult<Dagboek> Create([FromBody] Dagboek dagboek)
        {
            var id = Guid.NewGuid();
            dagboek.Id = id;
            _dagboeken[id] = dagboek;
            return CreatedAtAction(nameof(Get), new { id }, dagboek);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Dagboek updatedDagboek)
        {
            if (!_dagboeken.ContainsKey(id))
            {
                return NotFound();
            }
            updatedDagboek.Id = id;
            _dagboeken[id] = updatedDagboek;
            return Ok(updatedDagboek);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_dagboeken.Remove(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}



