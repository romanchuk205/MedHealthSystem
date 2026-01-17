using MedHealth.Feedback.Domain.Entities;
using MedHealth.Feedback.Domain.Interfaces;
using MedHealth.Feedback.Domain.ValueObjects; // <--- Додали цей using
using MediatR;

namespace MedHealth.Feedback.Application.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, string>
{
    private readonly IReviewRepository _repository;

    public CreateReviewCommandHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        // 1. Створюємо сутність
        // Ми вручну створюємо об'єкт Rating з числа, яке прийшло в команді
        var review = new Review(request.DoctorName, request.PatientName, request.Text, new Rating(request.Rating));

        // 2. Зберігаємо через репозиторій
        await _repository.CreateAsync(review, cancellationToken);

        // 3. Повертаємо ID
        return review.Id;
    }
}