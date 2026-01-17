using MedHealth.Feedback.Domain.Entities;
using MedHealth.Feedback.Domain.ValueObjects;
using MedHealth.Feedback.Infrastructure.Persistence;
using MongoDB.Driver;

namespace MedHealth.Feedback.Infrastructure.Seeding;

public class DataSeeder
{
    private readonly MongoDbContext _context;

    public DataSeeder(MongoDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // 1. Перевіряємо, чи є вже відгуки в базі
        var count = await _context.Reviews.CountDocumentsAsync(_ => true);
        
        // Якщо база не порожня — нічого не робимо
        if (count > 0) return;

        // 2. Якщо порожня — створюємо тестові дані
        var reviews = new List<Review>
        {
            new Review(
                "Лікар Хаус", 
                "Іван Іванов", 
                "Діагноз поставив швидко, але був грубим.", 
                new Rating(4)
            ),
            new Review(
                "Лікар Ватсон", 
                "Шерлок Холмс", 
                "Дуже уважний лікар, рекомендую!", 
                new Rating(5)
            ),
            new Review(
                "Лікар Айболить", 
                "Зайчик", 
                "Пришив ніжку, все супер.", 
                new Rating(5)
            )
        };

        // 3. Записуємо їх у MongoDB
        await _context.Reviews.InsertManyAsync(reviews);
    }
}