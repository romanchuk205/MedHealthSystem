namespace MedHealth.Catalog.Api.Entities;

public class Specialization
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty; // Назва (Кардіолог, ЛОР)

    // Зв'язок: Одна спеціалізація у багатьох лікарів
    public List<Doctor> Doctors { get; set; } = new();
}