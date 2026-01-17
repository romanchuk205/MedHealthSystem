using MedHealth.Feedback.Domain.Entities;
using MedHealth.Feedback.Domain.Interfaces;
using MedHealth.Feedback.Infrastructure.Persistence;
using MongoDB.Driver;

namespace MedHealth.Feedback.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly MongoDbContext _context;

    public ReviewRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Review review, CancellationToken cancellationToken)
    {
        await _context.Reviews.InsertOneAsync(review, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Reviews.Find(_ => true).ToListAsync(cancellationToken);
    }
}