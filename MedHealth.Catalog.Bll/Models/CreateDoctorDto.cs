namespace MedHealth.Catalog.Bll.Models;

public class CreateDoctorDto
{
    public string FullName { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    
    // Тут ми приймаємо ID, бо при створенні ми вказуємо, до кого прив'язати
    public int SpecializationId { get; set; }
    public int OfficeId { get; set; }
}