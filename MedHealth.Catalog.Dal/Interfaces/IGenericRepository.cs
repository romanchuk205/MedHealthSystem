using System.Linq.Expressions;

namespace MedHealth.Catalog.Dal.Interfaces;

public interface IGenericRepository<T> where T : class
{
    // Отримати за ID
    Task<T?> GetByIdAsync(int id);
    
    // Отримати всі (тільки для читання)
    Task<IEnumerable<T>> GetAllAsync();
    
    // Додавання
    Task AddAsync(T entity);
    
    // Оновлення (EF Core відстежує зміни, тому метод синхронний)
    void Update(T entity);
    
    // Видалення
    void Delete(T entity);
}