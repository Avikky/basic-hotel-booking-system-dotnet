using HotelBooking.Api.Domain.Persistence.Repository;
using HotelBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Services;

public class HotelService(IHotelRepository hotelRepository) : IHotelService
{
    public async Task<List<Hotel>> GetHotelsPagedAsync(int pageNumber, int pageSize)
    {
        return await hotelRepository.GetHotelsPagedAsync(pageNumber, pageSize);
    }

    public async Task<List<Hotel>> SearchHotelsAsync(string searchKey)
    {
        return await hotelRepository.SearchHotelsAsync(searchKey);
    }

    internal async Task<IActionResult> GetHotelsPagedAsync()
    {
        throw new NotImplementedException();
    }
}
