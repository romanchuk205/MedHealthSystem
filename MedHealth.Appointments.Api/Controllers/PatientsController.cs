using MedHealth.Appointments.Bll.Interfaces;
using MedHealth.Appointments.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedHealth.Appointments.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    // Тепер ми залежимо від Сервісу, а не від бази даних
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    // GET: api/Patients
    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        // Контролер просто передає запит у сервіс
        var patients = await _patientService.GetAllPatientsAsync();
        
        // І повертає результат (200 OK)
        return Ok(patients);
    }

    // POST: api/Patients
    [HttpPost]
    public async Task<IActionResult> CreatePatient(CreatePatientDto dto)
    {
        // Валідація (якщо прийшли пусті дані)
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Викликаємо бізнес-логіку
        var newId = await _patientService.CreatePatientAsync(dto);

        // Повертаємо 201 Created і ID нового пацієнта
        return CreatedAtAction(nameof(GetAllPatients), new { id = newId }, newId);
    }
}