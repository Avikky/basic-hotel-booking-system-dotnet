using HotelBooking.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController(HotelService hotelService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var hotel =  await hotelService.GetHotelsPagedAsync();

        return Ok(hotel);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchHotels([FromQuery] string searchKey)
    {
        var hotels = await hotelService.SearchHotelsAsync(searchKey);
        return Ok(hotels);
    }


}
