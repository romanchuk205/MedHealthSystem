using MedHealth.Feedback.Application.Reviews.Commands.CreateReview;
using MedHealth.Feedback.Application.Reviews.Queries.GetReviews;
using MedHealth.Feedback.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MedHealth.Feedback.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    // Замість MongoDB ми підключаємо MediatR
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/Reviews
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewCommand command)
    {
        // Ми просто кидаємо команду в MediatR, а він сам знайде потрібний Handler в шарі Application
        var id = await _mediator.Send(command);
        
        // Повертаємо статус 201 Created
        return CreatedAtAction(nameof(GetAll), new { id }, id);
    }

    // GET: api/Reviews
    [HttpGet]
    public async Task<IEnumerable<Review>> GetAll()
    {
        // Кидаємо запит (Query) в MediatR для отримання списку
        return await _mediator.Send(new GetReviewsQuery());
    }
}