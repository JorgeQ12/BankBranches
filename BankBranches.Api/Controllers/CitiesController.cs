using BankBranches.Application.DTOs.City;
using BankBranches.Application.Interfaces;
using BankBranches.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankBranches.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    /// <summary>
    /// Consultar el listado de todas las ciudades activas registradas.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetAllCitiesAsync))]
    public async Task<RequestResult<IEnumerable<CityDto>>> GetAllCitiesAsync() => await _cityService.GetAllCitiesAsync();

    /// <summary>
    /// Buscar una ciudad específica por su Id.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetCityByIdAsync))]
    public async Task<RequestResult<CityDto>> GetCityByIdAsync([FromQuery] int cityId) => await _cityService.GetCityByIdAsync(cityId);

    /// <summary>
    /// Filtrar y listar las ciudades pertenecientes a una región específica.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetCitiesByRegionIdAsync))]
    public async Task<RequestResult<IEnumerable<CityDto>>> GetCitiesByRegionIdAsync([FromQuery] int regionId) => await _cityService.GetCitiesByRegionIdAsync(regionId);
}
