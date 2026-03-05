using BankBranches.Application.DTOs.Branch;
using BankBranches.Application.Interfaces;
using BankBranches.Domain.Common;
using BankBranches.Domain.Entities;
using BankBranches.Domain.Interfaces;

namespace BankBranches.Application.Services;
public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;

    public BranchService(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    /// <summary>
    /// Obtener todas las sucursales del banco.
    /// </summary>
    public async Task<RequestResult<IEnumerable<BranchDto>>> GetAllBranchesAsync()
    {
        IEnumerable<Branch> branches = await _branchRepository.GetAllBranchesAsync();
        IEnumerable<BranchDto> branchDtos = branches.Select(MapToBranchDto);
        return RequestResult<IEnumerable<BranchDto>>.Success(branchDtos);
    }

    /// <summary>
    /// Obtener una sucursal por ID.
    /// </summary>
    public async Task<RequestResult<BranchDto>> GetBranchByIdAsync(int branchId)
    {
        Branch? branch = await _branchRepository.GetBranchByIdAsync(branchId);
        if (branch is null)
            return RequestResult<BranchDto>.NotFound($"No se encontró la sucursal con Id {branchId}.");

        return RequestResult<BranchDto>.Success(MapToBranchDto(branch));
    }

    /// <summary>
    /// Obtener las sucursales por región.
    /// </summary>
    public async Task<RequestResult<IEnumerable<BranchDto>>> GetBranchesByRegionIdAsync(int regionId)
    {
        IEnumerable<Branch> branches = await _branchRepository.GetBranchesByRegionIdAsync(regionId);
        IEnumerable<BranchDto> branchDtos = branches.Select(MapToBranchDto);
        return RequestResult<IEnumerable<BranchDto>>.Success(branchDtos);
    }

    /// <summary>
    /// Obtener las sucursales por ciudad.
    /// </summary>
    public async Task<RequestResult<IEnumerable<BranchDto>>> GetBranchesByCityIdAsync(int cityId)
    {
        IEnumerable<Branch> branches = await _branchRepository.GetBranchesByCityIdAsync(cityId);
        IEnumerable<BranchDto> branchDtos = branches.Select(MapToBranchDto);
        return RequestResult<IEnumerable<BranchDto>>.Success(branchDtos);
    }

    /// <summary>
    /// Registrar una nueva sucursal.
    /// </summary>
    public async Task<RequestResult<string>> CreateBranchAsync(CreateBranchDto createBranchRequest)
    {
        Branch newBranch = Branch.Create(
            createBranchRequest.Name,
            createBranchRequest.Address,
            createBranchRequest.Phone,
            createBranchRequest.CityId,
            createBranchRequest.RegionId
        );
        return await _branchRepository.CreateBranchAsync(newBranch) ? RequestResult<string>.Success("Sucursal Creada Con Exito") : RequestResult<string>.Failure("No Fue Posible Crear La Sucursal");
    }

    /// <summary>
    /// Actualizar la información de una sucursal existente.
    /// </summary>
    public async Task<RequestResult<string>> UpdateBranchAsync(UpdateBranchDto updateBranchRequest)
    {
        Branch? existingBranch = await _branchRepository.GetBranchByIdAsync(updateBranchRequest.Id);
        if (existingBranch is null)
            return RequestResult<string>.NotFound($"No se encontró la sucursal con Id {updateBranchRequest.Id}.");

        existingBranch.Update(updateBranchRequest.Name, updateBranchRequest.Address, updateBranchRequest.Phone, updateBranchRequest.CityId, updateBranchRequest.RegionId);
        return await _branchRepository.UpdateBranchAsync(existingBranch) ? RequestResult<string>.Success("Sucursal Actualizada Con Exito") : RequestResult<string>.Failure("No Fue Posible Actualizar La Sucursal");
    }

    /// <summary>
    /// Eliminar una sucursal.
    /// </summary>
    public async Task<RequestResult<string>> DeleteBranchAsync(int branchId)
    {
        Branch? existingBranch = await _branchRepository.GetBranchByIdAsync(branchId);
        if (existingBranch is null)
            return RequestResult<string>.NotFound($"No se encontró la sucursal con Id {branchId}.");

        return await _branchRepository.DeleteBranchAsync(branchId) ? RequestResult<string>.Success("Sucursal Eliminada Con Exito") : RequestResult<string>.Failure("No Fue Posible Eliminar La Sucursal");
    }

    /// <summary>
    /// Realizar el mapeo de entidad de sucursal a DTO de respuesta.
    /// </summary>
    private static BranchDto MapToBranchDto(Branch branch) => new()
    {
        Id = branch.Id,
        Name = branch.Name,
        Address = branch.Address,
        Phone = branch.Phone,
        CityId = branch.CityId,
        CityName = branch.CityName ?? default!,
        RegionId = branch.RegionId,
        RegionName = branch.RegionName ?? default!,
    };
}
