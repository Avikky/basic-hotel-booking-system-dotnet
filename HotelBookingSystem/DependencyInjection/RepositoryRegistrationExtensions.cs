using HotelBooking.Api.Domain.Persistence.Repository;
using HotelBooking.Api.Services;
using HotelBookingSystem.Services;

namespace HotelBooking.Api.DependencyInjection;


public static class RepositoryRegistrationExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Register all your interfaces and implementations here
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();


        return services;
    }
}
