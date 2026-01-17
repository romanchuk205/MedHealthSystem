using MedHealth.Aggregator.Models;

namespace MedHealth.Aggregator.Services;

public class AppointmentsClient
{
    private readonly HttpClient _client;

    public AppointmentsClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<PatientDto>> GetPatientsAsync()
    {
        return await _client.GetFromJsonAsync<IEnumerable<PatientDto>>("/api/Patients") 
               ?? Enumerable.Empty<PatientDto>();
    }
    
    // Тут можна додати метод для отримання записів, але для тесту вистачить пацієнтів
}