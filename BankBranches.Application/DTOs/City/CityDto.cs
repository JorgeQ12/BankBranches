namespace BankBranches.Application.DTOs.City;
public sealed class CityDto
{
    /// ID de la ciudad.
    public int Id { get; set; }
    /// Nombre de la ciudad.
    public string Name { get; set; } = default!;
    /// ID de la región a la que pertenece.
    public int RegionId { get; set; }
}
