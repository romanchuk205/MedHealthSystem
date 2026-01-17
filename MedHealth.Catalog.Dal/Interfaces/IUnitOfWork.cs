namespace MedHealth.Catalog.Dal.Interfaces;

public interface IUnitOfWork
{
    IDoctorRepository Doctors { get; }
    // Можна додати інші репозиторії, якщо треба
    
    Task SaveAsync();
}