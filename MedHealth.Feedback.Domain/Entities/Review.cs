using MedHealth.Feedback.Domain.Common;
using MedHealth.Feedback.Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace MedHealth.Feedback.Domain.Entities;

public class Review : BaseEntity
{
    public string DoctorName { get; private set; }
    public string PatientName { get; private set; }
    public string Text { get; private set; }
    public Rating Rating { get; private set; }

    [BsonConstructor]
    public Review(string doctorName, string patientName, string text, Rating rating)
    {
        DoctorName = doctorName;
        PatientName = patientName;
        Text = text;
        Rating = rating;
    }
}