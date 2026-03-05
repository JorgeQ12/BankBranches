namespace BankBranches.Application.DTOs.Auth;
public sealed class RegisterRequestDto
{
    /// Nombre completo del usuario.
    public string FullName { get; set; } = default!;
    /// Correo electrónico del usuario.
    public string Email { get; set; } = default!;
    /// Contraseña del usuario.
    public string Password { get; set; } = default!;
    /// Rol del usuario (Admin, Usuario).
    public string Role { get; set; } = default!;
}
