using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMap.WebApi.Controllers
{
    [ApiController]
    [Route("ProfielKeuzes")]
    public class ProfielKeuzeController : ControllerBase
    {
        private readonly ProfielKeuzeRepository _profielKeuzeRepository;
        private readonly ILogger<ProfielKeuzeController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public ProfielKeuzeController(ProfielKeuzeRepository profielKeuzeRepository, ILogger<ProfielKeuzeController> logger, IAuthenticationService authenticationService)
        {
            _profielKeuzeRepository = profielKeuzeRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpGet(Name = "ReadProfielKeuzes")]
        public async Task<ActionResult<IEnumerable<ProfielKeuze>>> Get()
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var profielKeuzes = await _profielKeuzeRepository.GetProfielKeuzesByUserIdAsync(Guid.Parse(userId));
            return Ok(profielKeuzes);
        }

        [HttpGet("{profielKeuzeId}", Name = "ReadProfielKeuze")]
        public async Task<ActionResult<ProfielKeuze>> Get(Guid profielKeuzeId)
        {
            var profielKeuze = await _profielKeuzeRepository.ReadAsync(profielKeuzeId);
            if (profielKeuze == null)
                return NotFound();

            return Ok(profielKeuze);
        }

        [HttpPost(Name = "CreateProfielKeuze")]
        public async Task<ActionResult> Add(ProfielKeuze profielKeuze)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var existingProfielen = await _profielKeuzeRepository.GetProfielKeuzesByUserIdAsync(Guid.Parse(userId));
            if (existingProfielen.Count() >= 6)
            {
                return BadRequest("Er kunnen maximaal 6 profielen aangemaakt worden.");
            }

            profielKeuze.Id = Guid.NewGuid();
            profielKeuze.UserId = Guid.Parse(userId);
            var createdProfielKeuze = await _profielKeuzeRepository.InsertAsync(profielKeuze);
            return CreatedAtRoute("ReadProfielKeuze", new { profielKeuzeId = createdProfielKeuze.Id }, createdProfielKeuze);
        }

        [HttpPut("{profielKeuzeId}", Name = "UpdateProfielKeuze")]
        public async Task<ActionResult> Update(Guid profielKeuzeId, ProfielKeuze newProfielKeuze)
        {
            var existingProfielKeuze = await _profielKeuzeRepository.ReadAsync(profielKeuzeId);

            if (existingProfielKeuze == null)
                return NotFound();

            await _profielKeuzeRepository.UpdateAsync(newProfielKeuze);

            return Ok(newProfielKeuze);
        }

        [HttpDelete("{profielKeuzeId}", Name = "DeleteProfielKeuze")]
        public async Task<IActionResult> Delete(Guid profielKeuzeId)
        {
            var existingProfielKeuze = await _profielKeuzeRepository.ReadAsync(profielKeuzeId);

            if (existingProfielKeuze == null)
                return NotFound();

            await _profielKeuzeRepository.DeleteAsync(profielKeuzeId);

            return Ok();
        }
    }
}

