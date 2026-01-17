namespace MedHealth.Appointments.Api.Models;

public class Patient
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Email { get; set; } // Знак питання означає, що може бути пустим
    public DateTime CreatedAt { get; set; }
}