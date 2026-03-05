namespace BankBranches.Application.DTOs.Region;
public sealed class RegionDto
{
    /// ID de la región.
    public int Id { get; set; }
    /// Nombre de la región.
    public string Name { get; set; } = default!;
    /// Descripción de la región.
    public string Description { get; set; } = default!;
}
