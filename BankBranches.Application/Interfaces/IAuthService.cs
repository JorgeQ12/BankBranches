using BankBranches.Application.DTOs.Auth;
using BankBranches.Domain.Common;

namespace BankBranches.Application.Interfaces;
public interface IAuthService
{
    /// <summary>
    /// Registrar un nuevo usuario.
    /// </summary>
    Task<RequestResult<string>> RegisterUserAsync(RegisterRequestDto registerRequest);

    /// <summary>
    /// Autenticar usuario y generar un nuevo token JWT.
    /// </summary>
    Task<RequestResult<AuthResponseDto>> LoginUserAsync(LoginRequestDto loginRequest);
}
