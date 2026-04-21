namespace Infrastructure.Persistence.Entities.Bookings;

public sealed class BookingEntity
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string TrainingClassId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }

}
