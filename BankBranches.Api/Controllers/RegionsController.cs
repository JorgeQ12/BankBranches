using BankBranches.Domain.Common;
using BankBranches.Domain.Entities;
using BankBranches.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankBranches.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RegionsController : ControllerBase
{
    private readonly IRegionExternalService _regionExternalService;

    public RegionsController(IRegionExternalService regionExternalService)
    {
        _regionExternalService = regionExternalService;
    }

    /// <summary>
    /// Consultar el listado completo de regiones desde la API externa de Colombia.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetAllRegionsAsync))]
    public async Task<RequestResult<IEnumerable<Region>>> GetAllRegionsAsync()
    {
        IEnumerable<Region> regions = await _regionExternalService.GetAllRegionsAsync();
        return RequestResult<IEnumerable<Region>>.Success(regions);
    }

    /// <summary>
    /// Consultar el detalle de una región específica por su ID único.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetRegionByIdAsync))]
    public async Task<RequestResult<Region>> GetRegionByIdAsync([FromQuery] int regionId)
    {
        Region? region = await _regionExternalService.GetRegionByIdAsync(regionId);
        if (region is null)
            return RequestResult<Region>.NotFound($"No se encontró la región con Id {regionId} en el servicio externo.");

        return RequestResult<Region>.Success(region);
    }
}
