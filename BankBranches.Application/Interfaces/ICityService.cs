using BankBranches.Application.DTOs.City;
using BankBranches.Domain.Common;

namespace BankBranches.Application.Interfaces;
public interface ICityService
{
    /// <summary>
    /// Obtener todas las ciudades.
    /// </summary>
    Task<RequestResult<IEnumerable<CityDto>>> GetAllCitiesAsync();

    /// <summary>
    /// Obtener una ciudad por ID.
    /// </summary>
    Task<RequestResult<CityDto>> GetCityByIdAsync(int cityId);

    /// <summary>
    /// Obtener las ciudades por region.
    /// </summary>
    Task<RequestResult<IEnumerable<CityDto>>> GetCitiesByRegionIdAsync(int regionId);
}
