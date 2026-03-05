namespace BankBranches.Application.DTOs.Auth;
public sealed class LoginRequestDto
{
    /// Correo electrónico del usuario.
    public string Email { get; set; } = default!;
    /// Contraseña del usuario.
    public string Password { get; set; } = default!;
}
