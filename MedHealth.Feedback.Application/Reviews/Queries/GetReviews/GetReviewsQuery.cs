using MedHealth.Feedback.Domain.Entities;
using MediatR;

namespace MedHealth.Feedback.Application.Reviews.Queries.GetReviews;

public record GetReviewsQuery : IRequest<IEnumerable<Review>>;