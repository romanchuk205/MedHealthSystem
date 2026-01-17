using MedHealth.Feedback.Domain.Entities;
using MedHealth.Feedback.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedHealth.Feedback.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    // Ми отримуємо налаштування через Options pattern
    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    // Це наша "таблиця" (колекція) відгуків
    public IMongoCollection<Review> Reviews => _database.GetCollection<Review>("Reviews");
}