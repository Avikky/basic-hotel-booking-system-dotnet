using HotelBooking.Api.Domain.Persistence.Repository;
using HotelBooking.Api.Services;
using HotelBookingSystem.Domain.Entities;
using SQLitePCL;

namespace HotelBookingSystem.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    public async Task<string> CreateBookingAsync(Guid roomId, DateTime startDate, DateTime endDate, string guestName, int ocupants)
    {
        if (startDate >= endDate)
            throw new ArgumentException("End date must be after start date.");

        bool isBooked = await bookingRepository.IsRoomBookedAsync(roomId, startDate, endDate);
        if (isBooked)
            throw new InvalidOperationException("Room is already booked for the selected dates.");


        //fetch room details to set required Room property
        var roomDetails = await bookingRepository.GetRoomByIdAsync(roomId) ?? throw new ArgumentException("Room does not exist.");

        //capacity check

        if (CapactityExceeded(roomDetails.Capacity, ocupants)) // assuming 1 occupant for simplicity
            throw new InvalidOperationException("Room capacity exceeded.");



        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = roomId,
            Room = roomDetails, // Set required Room property
            GuestName = guestName,
            StartDate = startDate,
            EndDate = endDate,
            BookingReference = Guid.NewGuid().ToString()
        };

        await bookingRepository.AddAsync(booking);

        // mark room as booked
        await bookingRepository.MarkRoomAsBooked(roomId);

        return booking.BookingReference;
    }



    private static bool CapactityExceeded(int roomCapacity, int ocupants)
    {
        return ocupants > roomCapacity;
    }

    public async Task<Booking?> GetBookingByReferenceAsync(string reference)
    {
        return await bookingRepository.GetByReferenceAsync(reference);
    }


    public async Task<bool> IsRoomBookedAsync(Guid roomId, DateTime startDate, DateTime endDate)
    {
        return await bookingRepository.IsRoomBookedAsync(roomId, startDate, endDate);
    }

    public async Task<List<Booking>> ListAllBookingsByHotelId(Guid hotelId)
    {
        return await bookingRepository.ListAllBookingByHotelIdAsync(hotelId);
    }

    public async Task<bool> IsHotelFullyBookedAsync(Guid hotelId, DateTime startDate, DateTime endDate)
    {
        return await bookingRepository.IsHotelFullyBookedAsync(hotelId, startDate, endDate);
    }



}