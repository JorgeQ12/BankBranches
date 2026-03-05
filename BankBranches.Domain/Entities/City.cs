namespace BankBranches.Domain.Entities;

public sealed class City
{
    /// ID único de la ciudad.
    public int Id { get; private set; }
    /// Nombre de la ciudad.
    public string Name { get; private set; } = default!;
    /// ID de la región a la que pertenece la ciudad.
    public int RegionId { get; private set; }
    /// Indica si la ciudad está activa.
    public bool IsActive { get; private set; }

    /// <summary>
    /// Crear una nueva instancia de ciudad.
    /// </summary>
    public static City Create(string name, int regionId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la ciudad es obligatorio.", nameof(name));

        return new City
        {
            Name = name.Trim(),
            RegionId = regionId,
            IsActive = true
        };
    }
}
