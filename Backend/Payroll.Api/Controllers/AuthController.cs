using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payroll.Api.Domain.DTOs;
using Payroll.Api.Services;

namespace Payroll.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sm, ITokenService ts)
    {
        _userManager = um; _signInManager = sm; _tokenService = ts;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto dto)
    {
        var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return Unauthorized();

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded) return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user, roles);
        return Ok(new AuthResponse(token));
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult<object> Me() => Ok(new { userId = User.Identity?.Name, claims = User.Claims.Select(c => new { c.Type, c.Value }) });
}
