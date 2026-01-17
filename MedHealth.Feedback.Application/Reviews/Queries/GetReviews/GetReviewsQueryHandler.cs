using MedHealth.Feedback.Domain.Entities;
using MedHealth.Feedback.Domain.Interfaces;
using MediatR;

namespace MedHealth.Feedback.Application.Reviews.Queries.GetReviews;

public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, IEnumerable<Review>>
{
    private readonly IReviewRepository _repository;

    public GetReviewsQueryHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Review>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}