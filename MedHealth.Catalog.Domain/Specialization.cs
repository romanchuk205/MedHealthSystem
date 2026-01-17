namespace MedHealth.Catalog.Domain;

public class Specialization
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    // Зв'язок 1-до-багатьох
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}