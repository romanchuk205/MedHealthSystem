namespace MedHealth.Appointments.Domain;

public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; } // ID лікаря з іншого сервісу
    public DateTime AppointmentDate { get; set; }
    public bool IsConfirmed { get; set; }
}