namespace HotelBooking.Api.DTOs;


public class BookingRequestDto
{
    public Guid RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public int Occupants { get; set; }
}
