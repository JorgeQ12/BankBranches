using BankBranches.Domain.Entities;
using BankBranches.Domain.Interfaces;

namespace BankBranches.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IGenericRepository _genericRepository;

    public UserRepository(IGenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    /// <summary>
    /// Buscar un usuario por correo electrónico.
    /// </summary>
    public async Task<User?> GetUserByEmailAsync(string email) => await _genericRepository.GetProcedureSingleAsync<User>("sp_GetUserByEmail", new { Email = email });

    /// <summary>
    /// Registrar un nuevo usuario.
    /// </summary>
    public async Task<bool> CreateUserAsync(User user)
    {
        int rowsAffected = await _genericRepository.ExecuteScalarProcedureAsync<int>("sp_CreateUser", new
        {
            user.FullName,
            user.Email,
            user.PasswordHash,
            user.Role,
            user.CreatedAt,
            user.IsActive
        });
        return rowsAffected > 0;
    }
}
