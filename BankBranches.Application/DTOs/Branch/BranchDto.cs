namespace BankBranches.Application.DTOs.Branch;
public sealed class BranchDto
{
    /// ID único de la sucursal.
    public int Id { get; set; }
    /// Nombre de la sucursal.
    public string Name { get; set; } = default!;
    /// Dirección física de la sucursal.
    public string Address { get; set; } = default!;
    /// Número de teléfono de contacto.
    public string Phone { get; set; } = default!;
    /// ID de la ciudad.
    public int CityId { get; set; }
    /// Nombre de la ciudad.
    public string CityName { get; set; } = default!;
    /// ID de la región.
    public int RegionId { get; set; }
    /// Nombre de la región.
    public string RegionName { get; set; } = default!;
}
