using HotelBooking.Api.Domain.Enums;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Domain.Persistence.Repository;

public class BookingRepository(AppDbContext context) : IBookingRepository
{

    public async Task<Booking?> GetByReferenceAsync(string reference)
    {
        return await context.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .FirstOrDefaultAsync(b => b.BookingReference == reference && !b.IsDeleted);
    }

    public async Task<bool> IsRoomBookedAsync(Guid roomId, DateTime startDate, DateTime endDate)
    {
        return await context.Bookings.AnyAsync(b =>
            b.RoomId == roomId &&
            !b.IsDeleted &&
            b.StartDate < endDate &&
            startDate < b.EndDate);
    }


    public async Task AddAsync(Booking booking)
    {
        await context.Bookings.AddAsync(booking);

        await context.SaveChangesAsync();
    }

    public async Task<List<Booking>> ListAllBookingByHotelIdAsync(Guid hotelId)
    {
        return await context.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .Where(b => b.Room.HotelId == hotelId && !b.IsDeleted)
            .ToListAsync();
    }

    public async Task<Room?> GetRoomByIdAsync(Guid roomId)
    {
        return await context.Rooms.FindAsync(roomId);
    }

    public async Task<bool> IsHotelFullyBookedAsync(Guid hotelId, DateTime startDate, DateTime endDate)
    {
        const int MaxRooms = 6;

        // Count how many rooms in this hotel have overlapping bookings
        var bookedRoomCount = await context.Bookings
            .Where(b => b.Room.HotelId == hotelId &&
                        !b.IsDeleted &&
                        b.StartDate < endDate &&
                        startDate < b.EndDate)
            .Select(b => b.RoomId)
            .Distinct()
            .CountAsync();

        return bookedRoomCount >= MaxRooms;
    }

    public async Task MarkRoomAsBooked(Guid roomId)
    {
        var room = await context.Rooms.FindAsync(roomId) ?? throw new ArgumentException("Room does not exist.");
        room.Status = RoomStatus.Booked;
        await context.SaveChangesAsync();
    }

    public async Task SoftDeleteBookingAsync(Guid bookingId)
    {
        var booking = await context.Bookings.FindAsync(bookingId);
        if (booking != null)
        {
            booking.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }



}