using BankBranches.Domain.Entities;

namespace BankBranches.Domain.Interfaces;

public interface IBranchRepository
{
    /// <summary>
    /// Obtener el listado de todas las sucursales activas.
    /// </summary>
    Task<IEnumerable<Branch>> GetAllBranchesAsync();

    /// <summary>
    /// Obtener una sucursal por ID.
    /// </summary>
    Task<Branch?> GetBranchByIdAsync(int branchId);

    /// <summary>
    /// Obtener una sucursal por region.
    /// </summary>
    Task<IEnumerable<Branch>> GetBranchesByRegionIdAsync(int regionId);

    /// <summary>
    /// Obtener las sucursales por ciudad.
    /// </summary>
    Task<IEnumerable<Branch>> GetBranchesByCityIdAsync(int cityId);

    /// <summary>
    /// Registrar una nueva sucursal.
    /// </summary>
    Task<bool> CreateBranchAsync(Branch branch);

    /// <summary>
    /// Actualizar la información de una sucursal existente.
    /// </summary>
    Task<bool> UpdateBranchAsync(Branch branch);

    /// <summary>
    /// Realizar la eliminación de una sucursal.
    /// </summary>
    Task<bool> DeleteBranchAsync(int branchId);
}
