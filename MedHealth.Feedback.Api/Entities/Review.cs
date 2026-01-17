using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedHealth.Feedback.Api.Entities;

public class Review
{
    // У MongoDB ID — це спеціальний тип ObjectId, а не просто число
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string DoctorName { get; set; } = string.Empty; // Ім'я лікаря (дублювання даних, як вимагає NoSQL)
    public string PatientName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int Rating { get; set; } // Оцінка від 1 до 5
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Вкладений масив (Embedded Document) - відповіді адміністрації
    public List<AdminReply> Replies { get; set; } = new();
}

// Це допоміжний клас, він буде жити всередині Review
public class AdminReply
{
    public string AdminName { get; set; } = string.Empty;
    public string ReplyText { get; set; } = string.Empty;
    public DateTime RepliedAt { get; set; } = DateTime.UtcNow;
}