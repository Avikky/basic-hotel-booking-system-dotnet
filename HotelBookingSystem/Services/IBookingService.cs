using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Services
{
    public interface IBookingService
    {
        Task<string> CreateBookingAsync(Guid roomId, DateTime startDate, DateTime endDate, string guestName, int ocupants);
        Task<Booking?> GetBookingByReferenceAsync(string reference);
        Task<bool> IsHotelFullyBookedAsync(Guid hotelId, DateTime startDate, DateTime endDate);
        Task<bool> IsRoomBookedAsync(Guid roomId, DateTime startDate, DateTime endDate);
        Task<List<Booking>> ListAllBookingsByHotelId(Guid hotelId);
    }
}