namespace BankBranches.Domain.Interfaces;

public interface IRegionExternalService
{
    /// <summary>
    /// Obtener el listado completo de regiones desde el servicio externo.
    /// </summary>
    Task<string> GetAllRegionsAsync();

    /// <summary>
    /// Obtener el detalle de una región específica consultando el servicio externo por su Id.
    /// </summary>
    Task<string> GetRegionByIdAsync(int regionId);
}
