namespace BankBranches.Application.DTOs.Auth;
public sealed class AuthResponseDto
{
    /// Token JWT generado.
    public string Token { get; set; } = default!;
}
