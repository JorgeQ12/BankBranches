using BankBranches.Domain.Entities;

namespace BankBranches.Domain.Interfaces;
public interface IUserRepository
{
    /// <summary>
    /// Buscar un usuario por correo electrónico.
    /// </summary>
    Task<User?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Registrar un nuevo usuario.
    /// </summary>
    Task<bool> CreateUserAsync(User user);
}
