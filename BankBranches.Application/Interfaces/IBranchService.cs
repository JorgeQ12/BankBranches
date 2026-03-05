using BankBranches.Application.DTOs.Branch;
using BankBranches.Domain.Common;

namespace BankBranches.Application.Interfaces;
public interface IBranchService
{
    /// <summary>
    /// Obtener todas las sucursales del banco.
    /// </summary>
    Task<RequestResult<IEnumerable<BranchDto>>> GetAllBranchesAsync();

    /// <summary>
    /// Obtener una sucursal por ID.
    /// </summary>
    Task<RequestResult<BranchDto>> GetBranchByIdAsync(int branchId);

    /// <summary>
    /// Obtener las sucursales por región.
    /// </summary>
    Task<RequestResult<IEnumerable<BranchDto>>> GetBranchesByRegionIdAsync(int regionId);

    /// <summary>
    /// Obtener las sucursales por ciudad.
    /// </summary>
    Task<RequestResult<IEnumerable<BranchDto>>> GetBranchesByCityIdAsync(int cityId);

    /// <summary>
    /// Registrar una nueva sucursal.
    /// </summary>
    Task<RequestResult<string>> CreateBranchAsync(CreateBranchDto createBranchRequest);

    /// <summary>
    /// Actualizar los datos de una sucursal existente.
    /// </summary>
    Task<RequestResult<string>> UpdateBranchAsync(UpdateBranchDto updateBranchRequest);

    /// <summary>
    /// Eliminar una sucursal.
    /// </summary>
    Task<RequestResult<string>> DeleteBranchAsync(int branchId);
}
