using BankBranches.Domain.Entities;

namespace BankBranches.Application.Interfaces;

public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generar un token JWT.
    /// </summary>
    string GenerateToken(User user);
}
