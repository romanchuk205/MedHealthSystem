namespace MedHealth.Catalog.Bll.Models;

public class DoctorDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    
    // Зверни увагу: ми повертаємо не ID, а одразу назви (Flattening)
    public string SpecializationTitle { get; set; } = string.Empty;
    public string OfficeRoomNumber { get; set; } = string.Empty;
}