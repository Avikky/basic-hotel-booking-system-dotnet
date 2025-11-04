using HotelBooking.Api.Domain.Entities;

namespace HotelBookingSystem.Domain.Entities;

public class Hotel : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public string? Country { get; set; } = string.Empty;
    public int? Rating { get; set; } = 0;  // e.g., 1 to 5 stars

    public required ICollection<Room> Rooms { get; set; } = [];

}
