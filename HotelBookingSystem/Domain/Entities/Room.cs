using HotelBooking.Api.Domain.Entities;
using HotelBooking.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Domain.Entities;

public class Room : BaseEntity
{
    public required string Type { get; set; } // Single, Double, Deluxe
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public Guid HotelId { get; set; }
    public required Hotel Hotel { get; set; }

    public string Status { get; set; } = RoomStatus.Available;

    public required ICollection<Booking> Bookings { get; set; } = [ ];
}
