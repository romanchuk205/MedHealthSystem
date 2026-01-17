namespace MedHealth.Catalog.Api.Entities;

public class Office
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty; // Номер (напр. "101-A")
    public int Floor { get; set; }

    // Зв'язок: В кабінеті можуть бути приписані кілька лікарів (позмінно)
    public List<Doctor> Doctors { get; set; } = new();
}