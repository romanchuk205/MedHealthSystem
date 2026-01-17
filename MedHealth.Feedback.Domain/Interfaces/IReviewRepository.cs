using MedHealth.Feedback.Domain.Entities;

namespace MedHealth.Feedback.Domain.Interfaces;

public interface IReviewRepository
{
    Task CreateAsync(Review review, CancellationToken cancellationToken);
    Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken);
}