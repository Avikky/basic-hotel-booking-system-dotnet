namespace HotelBooking.Api.DTOs;

public class RoomSearchDto
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int People { get; set; }
}
