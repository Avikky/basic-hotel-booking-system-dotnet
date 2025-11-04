using HotelBooking.Api.DTOs;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController(BookingService bookingService) : Controller
{
    [HttpGet("{hotelId}/all")]
    public async Task<IActionResult> Get(Guid Id)
    {
        var bookings = await bookingService.ListAllBookingsByHotelId(Id);

        return Ok(bookings);
    }

    [HttpGet("reference/{reference}")]
    public async Task<IActionResult> GetByReference(string reference)
    {
        var booking = await bookingService.GetBookingByReferenceAsync(reference);
        if (booking == null)
            return NotFound();
        return Ok(booking);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto request)
    {
        try
        {
            var bookingReference = await bookingService.CreateBookingAsync
                (
                    request.RoomId,
                    request.StartDate, 
                    request.EndDate, 
                    request.GuestName, 
                    request.Occupants
                );

            return Ok(new { BookingReference = bookingReference });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
