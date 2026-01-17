using MedHealth.Catalog.Bll.Interfaces;
using MedHealth.Catalog.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedHealth.Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    // GET: api/Doctors
    // Тепер сюди можна передавати параметри через URL
    // Наприклад: /api/Doctors?search=Петро&sortBy=experience&isDescending=true
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search, 
        [FromQuery] string? sortBy, 
        [FromQuery] bool isDescending = false, 
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10)
    {
        var doctors = await _doctorService.GetAllAsync(search, sortBy, isDescending, page, pageSize);
        return Ok(doctors);
    }

    // GET: api/Doctors/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        
        if (doctor == null)
        {
            return NotFound(new { message = $"Лікаря з ID {id} не знайдено." });
        }

        return Ok(doctor);
    }

    // POST: api/Doctors
    [HttpPost]
    public async Task<IActionResult> Create(CreateDoctorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _doctorService.CreateAsync(dto);

        // Повертаємо 201 Created
        return CreatedAtAction(nameof(GetAll), null);
    }
}