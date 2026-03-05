using System.Text.Json;
using BankBranches.Domain.Entities;
using BankBranches.Domain.Interfaces;

namespace BankBranches.Infrastructure.ExternalServices;

public class ColombiaApiService : IRegionExternalService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ColombiaApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    /// <summary>
    /// Obtener el listado completo de regiones desde el servicio externo.
    /// </summary>
    public async Task<IEnumerable<Region>> GetAllRegionsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/v1/Region");
        if (!response.IsSuccessStatusCode) return Enumerable.Empty<Region>();

        string content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<Region>>(content, _jsonOptions) ?? Enumerable.Empty<Region>();
    }

    /// <summary>
    /// Obtener el detalle de una región específica por su Id.
    /// </summary>
    public async Task<Region?> GetRegionByIdAsync(int regionId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/Region/{regionId}");
        if (!response.IsSuccessStatusCode) return null;

        string content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Region>(content, _jsonOptions);
    }
}
