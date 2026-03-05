namespace BankBranches.Domain.Entities;

public sealed class User
{
    /// ID único del usuario.
    public int Id { get; private set; }
    /// Nombre completo del usuario.
    public string FullName { get; private set; } = default!;
    /// Correo electrónico del usuario (normalizado a minúsculas).
    public string Email { get; private set; } = default!;
    /// Hash BCrypt de la contraseña del usuario.
    public string PasswordHash { get; private set; } = default!;
    /// Rol del usuario en el sistema (Admin, Usuario).
    public string Role { get; private set; } = default!;
    /// Fecha y hora de creación del registro.
    public DateTime CreatedAt { get; private set; }
    /// Indica si el usuario está activo en el sistema.
    public bool IsActive { get; private set; }

    /// <summary>
    /// Crear una nueva instancia de usuario con validaciones de dominio.
    /// </summary>
    public static User Create(string fullName, string email, string passwordHash, string? role = null)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("El nombre completo es obligatorio.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El correo electrónico es obligatorio.", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("El hash de la contraseña es obligatorio.", nameof(passwordHash));

        return new User
        {
            FullName = fullName.Trim(),
            Email = email.ToLowerInvariant().Trim(),
            PasswordHash = passwordHash,
            Role = string.IsNullOrWhiteSpace(role) ? "Usuario" : role,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }
}
