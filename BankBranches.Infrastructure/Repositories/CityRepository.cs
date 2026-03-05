using BankBranches.Domain.Entities;
using BankBranches.Domain.Interfaces;

namespace BankBranches.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly IGenericRepository _genericRepository;

    public CityRepository(IGenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    /// <summary>
    /// Obtener el listado de todas las ciudades registradas y activas.
    /// </summary>
    public async Task<IEnumerable<City>> GetAllCitiesAsync() => await _genericRepository.GetProcedureAsync<City>("sp_GetAllCities");

    /// <summary>
    /// Obtener una ciudad por ID.
    /// </summary>
    public async Task<City?> GetCityByIdAsync(int cityId) => await _genericRepository.GetProcedureSingleAsync<City>("sp_GetCityById", new { Id = cityId });

    /// <summary>
    /// Obtener ciudades por región.
    /// </summary>
    public async Task<IEnumerable<City>> GetCitiesByRegionIdAsync(int regionId) => await _genericRepository.GetProcedureAsync<City>("sp_GetCitiesByRegionId", new { RegionId = regionId });
}
