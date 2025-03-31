using Microsoft.AspNetCore.Mvc;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;
using System;

namespace ProjectMap.WebApi.Controllers;

[ApiController]
[Route("ProfielKeuzes")]
public class ProfielKeuzeController : ControllerBase
{
    private readonly ProfielKeuzeRepository _profielKeuzeRepository;
    private readonly ILogger<ProfielKeuzeController> _logger;

    public ProfielKeuzeController(ProfielKeuzeRepository profielKeuzeRepository, ILogger<ProfielKeuzeController> logger)
    {
        _profielKeuzeRepository = profielKeuzeRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadProfielKeuzes")]
    public async Task<ActionResult<IEnumerable<ProfielKeuze>>> Get()
    {
        var profielKeuzes = await _profielKeuzeRepository.ReadAsync();
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
        profielKeuze.Id = Guid.NewGuid();

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
