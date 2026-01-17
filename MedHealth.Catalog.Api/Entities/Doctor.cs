namespace MedHealth.Catalog.Api.Entities;

public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    
    // Зв'язок: У лікаря одна спеціалізація (наприклад, Хірург)
    public int SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }

    // Зв'язок: Лікар приймає в одному кабінеті
    public int OfficeId { get; set; }
    public Office? Office { get; set; }
}