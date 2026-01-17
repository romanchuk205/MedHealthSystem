using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedHealth.Feedback.Domain.Common;

public abstract class BaseEntity
{
    // Це спеціальний тип ID для MongoDB
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; protected set; } = ObjectId.GenerateNewId().ToString();

    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
}