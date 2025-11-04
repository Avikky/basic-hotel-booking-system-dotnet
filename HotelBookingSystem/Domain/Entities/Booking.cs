using HotelBooking.Api.Domain.Entities;

namespace HotelBookingSystem.Domain.Entities;

public class Booking : BaseEntity
{
    private bool isDeleted = false;

    public Guid RoomId { get; set; }
    public required Room Room { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int Ocupants { get; set; } = 1;
    public required string GuestName { get; set; }
    public string BookingReference { get; set; } = string.Empty;

    public bool IsDeleted { get => isDeleted; set => isDeleted = value; }
}
