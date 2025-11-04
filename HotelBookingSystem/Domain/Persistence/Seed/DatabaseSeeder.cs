using HotelBooking.Api.Domain.Enums;
using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Persistence.Seed;

public static class DatabaseSeeder
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Hotels.Any())
        {
            var hotel1 = new Hotel
            {
                Id = Guid.NewGuid(),
                Name = "Grand Palace",
                Address = "City Center",
                Rooms = new List<Room>()
            };
            var hotel2 = new Hotel
            {
                Id = Guid.NewGuid(),
                Name = "Ocean View",
                Address = "Beach Road",
                Rooms = new List<Room>()
            };

            context.Hotels.AddRange(hotel1, hotel2);

            var room1 = new Room
            {
                Id = Guid.NewGuid(),
                Type = "Single",
                Capacity = 1,
                PricePerNight = 50,
                HotelId = hotel1.Id,
                Hotel = hotel1,
                Status = RoomStatus.Booked,
                Bookings = new List<Booking>()
            };
            var room2 = new Room
            {
                Id = Guid.NewGuid(),
                Type = "Double",
                Capacity = 2,
                PricePerNight = 80,
                HotelId = hotel2.Id,
                Hotel = hotel2,
                Status = RoomStatus.Available,
                Bookings = new List<Booking>()
            };

            context.Rooms.AddRange(room1, room2);

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                RoomId = room1.Id,
                Room = room1,
                GuestName = "John Doe",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(2)
            };

            context.Bookings.Add(booking);

            context.SaveChanges();
        }
    }
}


