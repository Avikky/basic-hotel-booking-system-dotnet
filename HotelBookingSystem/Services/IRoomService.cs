using HotelBooking.Api.DTOs;
using HotelBookingSystem.Domain.Entities;

namespace HotelBooking.Api.Services
{
    public interface IRoomService
    {
        Task<Room?> GetRoomByIdAsync(Guid roomId);
        Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime startDate, DateTime endDate);
        Task<List<Room>> ListAllRoomsByHotelIdAsync(Guid hotelId);
        Task<bool> RoomExistsAsync(Guid roomId);

        Task<List<Room>> GetAvailableRoomsAsync(RoomSearchDto data);
    }
}