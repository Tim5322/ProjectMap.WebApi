using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

[Route("account/[controller]")]
[ApiController]
[Authorize] // Beveilig de API
public class AccountManagementController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AccountManagementController> _logger;

    public AccountManagementController(
        ILogger<AccountManagementController> logger,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    // Endpoint om een claim toe te voegen aan een gebruiker
    [HttpPost("add-claim")]
    public async Task<IActionResult> AddClaimToUser([FromBody] ClaimRequestModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return NotFound(new { message = "Gebruiker niet gevonden" });

        var claim = new Claim(model.ClaimType, model.ClaimValue);
        var result = await _userManager.AddClaimAsync(user, claim);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "Claim toegevoegd!" });
    }
}

// Model voor de API-call
public class ClaimRequestModel
{
    public string Email { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}

