using HotelBookingSystem.Domain.Entities;

namespace HotelBooking.Api.Domain.Persistence.Repository
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task<Booking?> GetByReferenceAsync(string reference);
        Task<Room?> GetRoomByIdAsync(Guid roomId);
        Task<bool> IsHotelFullyBookedAsync(Guid hotelId, DateTime startDate, DateTime endDate);
        Task<bool> IsRoomBookedAsync(Guid roomId, DateTime startDate, DateTime endDate);
        Task<List<Booking>> ListAllBookingByHotelIdAsync(Guid hotelId);
        Task MarkRoomAsBooked(Guid roomId);
        Task SoftDeleteBookingAsync(Guid bookingId);
    }
}