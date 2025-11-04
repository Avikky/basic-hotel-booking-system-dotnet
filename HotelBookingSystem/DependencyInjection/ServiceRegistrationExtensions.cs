using HotelBooking.Api.Services;
using HotelBookingSystem.Services;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Register all your interfaces and implementations here
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IResetDataService, ResetDataService>();

        return services;
    }
}