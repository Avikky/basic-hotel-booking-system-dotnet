using HotelBooking.Api.DTOs;
using HotelBooking.Api.Services;
using HotelBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(RoomService roomService) : ControllerBase
    {
        [HttpGet("search")]
        public async Task<IActionResult> GetAvailableRooms([FromQuery] RoomSearchDto request)
        {
            try
            {
                var availableRooms = await roomService.GetAvailableRoomsAsync(request);

                return Ok(availableRooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomById(Guid roomId)
        {
            var room = await roomService.GetRoomByIdAsync(roomId);
            if (room == null)
                return NotFound();
            return Ok(room);
        }


        [HttpGet("hotel/{hotelId}/all")]
        public async Task<IActionResult> ListAllRoomsByHotelId(Guid hotelId)
        {
            var rooms = await roomService.ListAllRoomsByHotelIdAsync(hotelId);
            return Ok(rooms);
        }


    }
}
