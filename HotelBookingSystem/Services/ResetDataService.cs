using HotelBookingSystem.Domain.Persistence;
using HotelBookingSystem.Domain.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Services;

public class ResetDataService(AppDbContext dbContext) : IResetDataService
{

    public async Task ResetAndSeedAsync()
    {
        // Drop database
        await dbContext.Database.EnsureDeletedAsync();

        // Apply migrations (creates schema based on migrations)
        await dbContext.Database.MigrateAsync();

        // Seed data
        DatabaseSeeder.Initialize(dbContext);
    }
}
