using HotelBooking.Api.Domain.Enums;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Domain.Persistence.Repository;

public class RoomRepository(AppDbContext context) : IRoomRepository
{


    public async Task<List<Room>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate, int occupants) => await context.Rooms
            .Where(r => r.Capacity >= occupants &&
                        r.Status == RoomStatus.Available &&
                        !context.Bookings.Any(b =>
                            b.RoomId == r.Id &&
                            !b.IsDeleted &&
                            b.StartDate < endDate &&
                            startDate < b.EndDate))
            .ToListAsync();


    public async Task<Room?> GetByIdAsync(Guid roomId)
    {
        return await context.Rooms.FindAsync(roomId);
    }

    public async Task<List<Room>> ListAllByHotelIdAsync(Guid hotelId)
    {
        return await context.Rooms
            .Where(r => r.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime startDate, DateTime endDate)
    {
        var isBooked = await context.Bookings.AnyAsync(b =>
            b.RoomId == roomId &&
            b.StartDate < endDate &&
            startDate < b.EndDate);
        return !isBooked;
    }

    public Task<bool> ExistsAsync(Guid roomId)
    {
        throw new NotImplementedException();
    }

}
