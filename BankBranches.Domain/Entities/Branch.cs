namespace BankBranches.Domain.Entities;

public sealed class Branch
{
    /// ID único de la sucursal.
    public int Id { get; private set; }
    /// Nombre de la sucursal.
    public string Name { get; private set; } = default!;
    /// Dirección física de la sucursal.
    public string Address { get; private set; } = default!;
    /// Número de teléfono de contacto.
    public string Phone { get; private set; } = default!;
    /// ID de la ciudad donde se ubica la sucursal.
    public int CityId { get; private set; }
    /// ID de la región a la que pertenece la sucursal.
    public int RegionId { get; private set; }
    /// Indica si la sucursal está activa.
    public bool IsActive { get; private set; }
    /// Fecha y hora de creación del registro.
    public DateTime CreatedAt { get; private set; }
    /// Nombre de la ciudad.
    public string? CityName { get; private set; }
    /// Nombre de la región.
    public string? RegionName { get; private set; }

    /// <summary>
    /// Crear una nueva instancia de sucursal con validaciones de dominio.
    /// </summary>
    public static Branch Create(string name, string address, string phone, int cityId, int regionId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la sucursal es obligatorio.", nameof(name));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("La dirección de la sucursal es obligatoria.", nameof(address));

        return new Branch
        {
            Name = name.Trim(),
            Address = address.Trim(),
            Phone = phone.Trim(),
            CityId = cityId,
            RegionId = regionId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Actualizar la información de la sucursal.
    /// </summary>
    public void Update(string name, string address, string phone, int cityId, int regionId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la sucursal no puede estar vacío.", nameof(name));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("La dirección de la sucursal no puede estar vacía.", nameof(address));

        Name = name.Trim();
        Address = address.Trim();
        Phone = phone.Trim();
        CityId = cityId;
        RegionId = regionId;
    }
}
