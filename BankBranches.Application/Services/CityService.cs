using BankBranches.Application.DTOs.City;
using BankBranches.Application.Interfaces;
using BankBranches.Domain.Common;
using BankBranches.Domain.Entities;
using BankBranches.Domain.Interfaces;

namespace BankBranches.Application.Services;
public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    /// <summary>
    /// Obtener todas las ciudades.
    /// </summary>
    public async Task<RequestResult<IEnumerable<CityDto>>> GetAllCitiesAsync()
    {
        IEnumerable<City> cities = await _cityRepository.GetAllCitiesAsync();
        IEnumerable<CityDto> cityDtos = cities.Where(x => x.IsActive).Select(MapToCityDto);
        return RequestResult<IEnumerable<CityDto>>.Success(cityDtos);
    }

    /// <summary>
    /// Obtener una ciudad por ID.
    /// </summary>
    public async Task<RequestResult<CityDto>> GetCityByIdAsync(int cityId)
    {
        City? city = await _cityRepository.GetCityByIdAsync(cityId);
        if (city is null)
            return RequestResult<CityDto>.NotFound($"No se encontró la ciudad con Id {cityId}.");

        return RequestResult<CityDto>.Success(MapToCityDto(city));
    }

    /// <summary>
    /// Obtener las ciudades por region.
    /// </summary>
    public async Task<RequestResult<IEnumerable<CityDto>>> GetCitiesByRegionIdAsync(int regionId)
    {
        IEnumerable<City> cities = await _cityRepository.GetCitiesByRegionIdAsync(regionId);
        IEnumerable<CityDto> cityDtos = cities.Select(MapToCityDto);
        return RequestResult<IEnumerable<CityDto>>.Success(cityDtos);
    }

    /// <summary>
    /// Realizar el mapeo de la entidad ciudad a su DTO de respuesta.
    /// </summary>
    private static CityDto MapToCityDto(City city) => new()
    {
        Id = city.Id,
        Name = city.Name,
        RegionId = city.RegionId,
    };
}
