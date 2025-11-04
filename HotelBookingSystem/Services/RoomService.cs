using HotelBooking.Api.Domain.Persistence.Repository;
using HotelBooking.Api.DTOs;
using HotelBookingSystem.Domain.Entities;

namespace HotelBooking.Api.Services;

public class RoomService(IRoomRepository roomRepository) : IRoomService
{
    public async Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime startDate, DateTime endDate)
    {
        return await roomRepository.IsRoomAvailableAsync(roomId, startDate, endDate);
    }

   public async Task<List<Room>> GetAvailableRoomsAsync(RoomSearchDto data)
   {
        return await roomRepository.GetAvailableRoomsAsync(data.StartDate, data.EndDate, data.People);
   }

    public async Task<Room?> GetRoomByIdAsync(Guid roomId)
    {
        return await roomRepository.GetByIdAsync(roomId);
    }

    public async Task<List<Room>> ListAllRoomsByHotelIdAsync(Guid hotelId)
    {
        return await roomRepository.ListAllByHotelIdAsync(hotelId);
    }

    public Task<bool> RoomExistsAsync(Guid roomId)
    {
        throw new NotImplementedException();
    }
}
