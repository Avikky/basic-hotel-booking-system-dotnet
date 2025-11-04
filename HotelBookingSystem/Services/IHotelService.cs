using HotelBookingSystem.Domain.Entities;

namespace HotelBooking.Api.Services
{
    public interface IHotelService
    {
        Task<List<Hotel>> GetHotelsPagedAsync(int pageNumber, int pageSize);
        Task<List<Hotel>> SearchHotelsAsync(string searchKey);
    }
}