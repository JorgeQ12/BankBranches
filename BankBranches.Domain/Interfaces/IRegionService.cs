using BankBranches.Domain.Entities;

namespace BankBranches.Domain.Interfaces;

public interface IRegionExternalService
{
    /// <summary>
    /// Obtener el listado completo de regiones desde el servicio externo.
    /// </summary>
    Task<IEnumerable<Region>> GetAllRegionsAsync();

    /// <summary>
    /// Obtener el detalle de una región específica por su Id.
    /// </summary>
    Task<Region?> GetRegionByIdAsync(int regionId);
}
