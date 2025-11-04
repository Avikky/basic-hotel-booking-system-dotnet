using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Persistence;
using Microsoft.EntityFrameworkCore;



namespace HotelBooking.Api.Domain.Persistence.Repository;

public class HotelRepository(AppDbContext context) : IHotelRepository
{
    public async Task<List<Hotel>> GetHotelsPagedAsync(int pageNumber, int pageSize)
    {
        return await context.Hotels
            .Include(h => h.Rooms) // Include related Rooms
            .OrderByDescending(h => h.Name) // Sort descending by Name
            .Skip((pageNumber - 1) * pageSize) // Skip previous pages
            .Take(pageSize) // Take current page
            .ToListAsync();
    }
    public async Task<List<Hotel>> SearchHotelsAsync(string searchKey)
    {
        return await context.Hotels.Include(h => h.Rooms).Where(h => EF.Functions.Like(h.Name, $"%{searchKey}%")).ToListAsync();
    }


}
