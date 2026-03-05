using BankBranches.Domain.Entities;

namespace BankBranches.Domain.Interfaces;

public interface ICityRepository
{
    /// <summary>
    /// Obtener el listado de todas las ciudades registradas y activas.
    /// </summary>
    Task<IEnumerable<City>> GetAllCitiesAsync();

    /// <summary>
    /// Obtener una ciudad por ID.
    /// </summary>
    Task<City?> GetCityByIdAsync(int cityId);

    /// <summary>
    /// Obtener ciudades por región.
    /// </summary>
    Task<IEnumerable<City>> GetCitiesByRegionIdAsync(int regionId);
}
