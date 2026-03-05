using BankBranches.Application.DTOs.Branch;
using BankBranches.Application.Interfaces;
using BankBranches.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankBranches.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BranchesController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchesController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    /// <summary>
    /// Consultar el listado completo de sucursales activas.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetAllBranchesAsync))]
    public async Task<RequestResult<IEnumerable<BranchDto>>> GetAllBranchesAsync() => await _branchService.GetAllBranchesAsync();

    /// <summary>
    /// Buscar el detalle de una sucursal por su ID único.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetBranchByIdAsync))]
    public async Task<RequestResult<BranchDto>> GetBranchByIdAsync([FromQuery] int branchId) => await _branchService.GetBranchByIdAsync(branchId);

    /// <summary>
    /// Filtrar y listar las sucursales pertenecientes a una región.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetBranchesByRegionIdAsync))]
    public async Task<RequestResult<IEnumerable<BranchDto>>> GetBranchesByRegionIdAsync([FromQuery] int regionId) => await _branchService.GetBranchesByRegionIdAsync(regionId);

    /// <summary>
    /// Filtrar y listar las sucursales pertenecientes a una ciudad.
    /// </summary>
    [HttpGet]
    [Route(nameof(GetBranchesByCityIdAsync))]
    public async Task<RequestResult<IEnumerable<BranchDto>>> GetBranchesByCityIdAsync([FromQuery] int cityId) => await _branchService.GetBranchesByCityIdAsync(cityId);

    /// <summary>
    /// Registrar una nueva sucursal bancaria. Requiere rol de Administrador.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route(nameof(CreateBranchAsync))]
    public async Task<RequestResult<string>> CreateBranchAsync([FromBody] CreateBranchDto createBranch) => await _branchService.CreateBranchAsync(createBranch);

    /// <summary>
    /// Actualizar la información de una sucursal existente. Requiere rol de Administrador.
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route(nameof(UpdateBranchAsync))]
    public async Task<RequestResult<string>> UpdateBranchAsync([FromBody] UpdateBranchDto updateBranch) => await _branchService.UpdateBranchAsync(updateBranch);

    /// <summary>
    /// Eliminar una sucursal del sistema mediante borrado lógico. Requiere rol de Administrador.
    /// </summary>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route(nameof(DeleteBranchAsync))]
    public async Task<RequestResult<string>> DeleteBranchAsync([FromQuery] int branchId) => await _branchService.DeleteBranchAsync(branchId);
}
