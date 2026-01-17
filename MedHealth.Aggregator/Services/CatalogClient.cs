using MedHealth.Aggregator.Models;

namespace MedHealth.Aggregator.Services;

public class CatalogClient
{
    private readonly HttpClient _client;

    public CatalogClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<DoctorDto>> GetDoctorsAsync()
    {
        // Робимо запит до Catalog API
        // "api/Doctors" - це шлях контролера в Catalog
        return await _client.GetFromJsonAsync<IEnumerable<DoctorDto>>("/api/Doctors") 
               ?? Enumerable.Empty<DoctorDto>();
    }
}