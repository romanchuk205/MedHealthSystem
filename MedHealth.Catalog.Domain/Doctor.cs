namespace MedHealth.Catalog.Domain;

public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    
    // Зовнішні ключі
    public int SpecializationId { get; set; }
    public int OfficeId { get; set; }

    // Навігаційні властивості (для зв'язків)
    public Specialization? Specialization { get; set; }
    public Office? Office { get; set; }
}