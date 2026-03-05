using BankBranches.Application.DTOs.Auth;
using BankBranches.Application.Interfaces;
using BankBranches.Domain.Common;
using BankBranches.Domain.Entities;
using BankBranches.Domain.Interfaces;

namespace BankBranches.Application.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    /// <summary>
    /// Registrar un nuevo usuario.
    /// </summary>
    public async Task<RequestResult<string>> RegisterUserAsync(RegisterRequestDto registerRequest)
    {
        User? existingUser = await _userRepository.GetUserByEmailAsync(registerRequest.Email);
        if (existingUser is not null)
            return RequestResult<string>.Failure("Ya existe un usuario registrado con este email.");

        User newUser = User.Create(
            registerRequest.FullName,
            registerRequest.Email,
            BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
            registerRequest.Role
        );
        return await _userRepository.CreateUserAsync(newUser) ? RequestResult<string>.Success("Usuario Creado Correctamente") : RequestResult<string>.Failure("No Fue Posible Crear El Usuario");
    }

    /// <summary>
    /// Validar las credenciales del usuario y generar el token de sesión.
    /// </summary>
    public async Task<RequestResult<AuthResponseDto>> LoginUserAsync(LoginRequestDto loginRequest)
    {
        if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
            return RequestResult<AuthResponseDto>.Failure("El email y la contraseña son requeridos.");

        User? authenticatedUser = await _userRepository.GetUserByEmailAsync(loginRequest.Email.ToLowerInvariant().Trim());
        if (authenticatedUser is null || !authenticatedUser.IsActive)
            return RequestResult<AuthResponseDto>.Unauthorized("Credenciales inválidas.");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, authenticatedUser.PasswordHash);
        if (!isPasswordValid)
            return RequestResult<AuthResponseDto>.Unauthorized("Credenciales inválidas.");

        return RequestResult<AuthResponseDto>.Success(new AuthResponseDto { Token = _jwtTokenGenerator.GenerateToken(authenticatedUser) });
    }
}
