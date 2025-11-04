using HotelBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Domain.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Entities.Room> Rooms => Set<Entities.Room>();
    public DbSet<Entities.Booking> Bookings => Set<Entities.Booking>();

    public DbSet<Entities.Hotel> Hotels => Set<Entities.Hotel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Entities.Hotel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Address).IsRequired();
            entity.Property(e => e.City);
            entity.Property(e => e.Country);
            entity.Property(e => e.Rating);
        });


        modelBuilder.Entity<Entities.Room>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Capacity).IsRequired();
            entity.Property(e => e.PricePerNight).IsRequired().HasColumnType("decimal(18,2)");

            entity.HasOne(r => r.Hotel)
                  .WithMany(h => h.Rooms)
                  .HasForeignKey(r => r.HotelId)
                  .OnDelete(DeleteBehavior.Cascade); // Optional
        });


        modelBuilder.Entity<Entities.Booking>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.GuestName).IsRequired();
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();
            entity.Property(e => e.Ocupants);
            entity.Property(e => e.BookingReference).IsRequired();
            entity.HasOne(e => e.Room)
                  .WithMany()
                  .HasForeignKey(e => e.RoomId);



            entity.HasOne(e => e.Room)
                  .WithMany(r => r.Bookings)
                  .HasForeignKey(e => e.RoomId)
                  .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete for safety

        });
    }
}
