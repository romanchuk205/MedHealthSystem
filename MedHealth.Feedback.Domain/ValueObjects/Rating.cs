using MongoDB.Bson.Serialization.Attributes;

namespace MedHealth.Feedback.Domain.ValueObjects;

public class Rating
{
    public int Value { get; private set; }

    [BsonConstructor]
    public Rating(int value)
    {
        if (value < 1 || value > 5)
            throw new ArgumentException("Рейтинг має бути від 1 до 5");
        Value = value;
    }
    
    public static implicit operator int(Rating rating) => rating.Value;
    public static implicit operator Rating(int value) => new(value);
}