using BankBranches.Application.DTOs.Auth;
using BankBranches.Application.Interfaces;
using BankBranches.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankBranches.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registrar un nuevo usuario. Requiere privilegios de Administrador.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route(nameof(RegisterUserAsync))]
    public async Task<RequestResult<string>> RegisterUserAsync([FromBody] RegisterRequestDto registerRequest) => await _authService.RegisterUserAsync(registerRequest);

    /// <summary>
    /// Iniciar sesión y obtener el token de acceso JWT correspondiente.
    /// </summary>
    [HttpPost]
    [Route(nameof(LoginUserAsync))]
    public async Task<RequestResult<AuthResponseDto>> LoginUserAsync([FromBody] LoginRequestDto loginRequest) => await _authService.LoginUserAsync(loginRequest);
}
