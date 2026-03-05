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
    public async Task<string> GetAllRegionsAsync() => await _regionExternalService.GetAllRegionsAsync();

    /// <summary>
    /// Consultar el detalle de una región específica por su ID único.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetRegionByIdAsync))]
    public async Task<string> GetRegionByIdAsync([FromQuery] int regionId) => await _regionExternalService.GetRegionByIdAsync(regionId);
}
