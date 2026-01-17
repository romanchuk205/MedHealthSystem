using MediatR;

namespace MedHealth.Feedback.Application.Reviews.Commands.CreateReview;

// Команда повертає string (ID створеного запису)
public record CreateReviewCommand(string DoctorName, string PatientName, string Text, int Rating) : IRequest<string>;