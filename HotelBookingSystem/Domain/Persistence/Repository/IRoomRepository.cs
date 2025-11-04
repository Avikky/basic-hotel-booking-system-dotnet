using HotelBookingSystem.Domain.Entities;

namespace HotelBooking.Api.Domain.Persistence.Repository
{
    public interface IRoomRepository
    {
        Task<bool> ExistsAsync(Guid roomId);
        Task<List<Room>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate, int occupants);
        Task<Room?> GetByIdAsync(Guid roomId);
        Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime startDate, DateTime endDate);
        Task<List<Room>> ListAllByHotelIdAsync(Guid hotelId);
    }
}