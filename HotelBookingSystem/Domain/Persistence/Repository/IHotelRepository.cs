using HotelBookingSystem.Domain.Entities;

namespace HotelBooking.Api.Domain.Persistence.Repository
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetHotelsPagedAsync(int pageNumber, int pageSize);
        Task<List<Hotel>> SearchHotelsAsync(string searchKey);
    }
}