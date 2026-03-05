using BankBranches.Domain.Interfaces;

namespace BankBranches.Infrastructure.ExternalServices;

public class ColombiaApiService : IRegionExternalService
{
    private readonly HttpClient _httpClient;

    public ColombiaApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtener el listado completo de regiones desde el servicio externo.
    /// </summary>
    public async Task<string> GetAllRegionsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/v1/Region");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Obtener el detalle de una región específica consultando el servicio externo por su Id.
    /// </summary>
    public async Task<string> GetRegionByIdAsync(int regionId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/Region/{regionId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
