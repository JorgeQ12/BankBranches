using BankBranches.Domain.Entities;
using BankBranches.Domain.Interfaces;

namespace BankBranches.Infrastructure.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly IGenericRepository _genericRepository;

    public BranchRepository(IGenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    /// <summary>
    /// Obtener el listado de todas las sucursales activas.
    /// </summary>
    public async Task<IEnumerable<Branch>> GetAllBranchesAsync() => await _genericRepository.GetProcedureAsync<Branch>("sp_GetAllBranches");

    /// <summary>
    /// Obtener una sucursal por ID.
    /// </summary>
    public async Task<Branch?> GetBranchByIdAsync(int branchId) => await _genericRepository.GetProcedureSingleAsync<Branch>("sp_GetBranchById", new { Id = branchId });

    /// <summary>
    /// Obtener una sucursal por region.
    /// </summary>
    public async Task<IEnumerable<Branch>> GetBranchesByRegionIdAsync(int regionId) => await _genericRepository.GetProcedureAsync<Branch>("sp_GetBranchesByRegionId", new { RegionId = regionId });

    /// <summary>
    /// Obtener las sucursales por ciudad.
    /// </summary>
    public async Task<IEnumerable<Branch>> GetBranchesByCityIdAsync(int cityId) => await _genericRepository.GetProcedureAsync<Branch>("sp_GetBranchesByCityId", new { CityId = cityId });

    /// <summary>
    /// Registrar una nueva sucursal.
    /// </summary>
    public async Task<bool> CreateBranchAsync(Branch branch)
    {
        int createdId = await _genericRepository.ExecuteScalarProcedureAsync<int>("sp_CreateBranch", new
        {
            branch.Name,
            branch.Address,
            branch.Phone,
            branch.CityId,
            branch.RegionId,
            branch.IsActive,
            branch.CreatedAt
        });
        return createdId > 0;
    }

    /// <summary>
    /// Actualizar la información de una sucursal existente.
    /// </summary>
    public async Task<bool> UpdateBranchAsync(Branch branch)
    {
        int rowsAffected = await _genericRepository.ExecuteProcedureAsync("sp_UpdateBranch", new
        {
            branch.Id,
            branch.Name,
            branch.Address,
            branch.Phone,
            branch.CityId,
            branch.RegionId
        });
        return rowsAffected > 0;
    }

    /// <summary>
    /// Realizar la eliminación de una sucursal.
    /// </summary>
    public async Task<bool> DeleteBranchAsync(int branchId)
    {
        int rowsAffected = await _genericRepository.ExecuteProcedureAsync("sp_DeleteBranch", new { Id = branchId });
        return rowsAffected > 0;
    }
}
