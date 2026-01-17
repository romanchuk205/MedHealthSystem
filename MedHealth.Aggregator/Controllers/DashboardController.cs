using MedHealth.Aggregator.Models;
using MedHealth.Aggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedHealth.Aggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly CatalogClient _catalogClient;
    private readonly AppointmentsClient _appointmentsClient;

    public DashboardController(CatalogClient catalogClient, AppointmentsClient appointmentsClient)
    {
        _catalogClient = catalogClient;
        _appointmentsClient = appointmentsClient;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardData>> GetDashboard()
    {
        // ЗАПУСКАЄМО ЗАПИТИ ПАРАЛЕЛЬНО (Вимога лаби про Task.WhenAll)
        var doctorsTask = _catalogClient.GetDoctorsAsync();
        var patientsTask = _appointmentsClient.GetPatientsAsync();

        // Чекаємо завершення обох
        await Task.WhenAll(doctorsTask, patientsTask);

        var doctors = await doctorsTask;
        var patients = await patientsTask;

        // Формуємо загальну відповідь
        var result = new DashboardData(
            Patient: patients.FirstOrDefault(), // Просто беремо першого для прикладу
            Appointments: new List<AppointmentDto>(), // Поки пустий список
            RecommendedDoctors: doctors
        );

        return Ok(result);
    }
}