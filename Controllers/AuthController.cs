using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FirstAsp.Services;

namespace FirstAsp.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authService;

    public AuthController(AuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpGet("test-token")]
    [AllowAnonymous]
    public ActionResult<string> GetTestToken()
    {
        var token = _authService.GenerateTestToken();
        return Ok(token);
    }
}