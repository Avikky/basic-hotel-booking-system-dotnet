
using HotelBooking.Api.DependencyInjection;
using HotelBookingSystem.Domain.Persistence;
using HotelBookingSystem.Domain.Persistence.Seed;
using HotelBookingSystem.Middleware;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<Domain.Persistence.AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")));

            builder.Services.AddTransient<ApplicationMiddleware>();
            builder.Services.AddExceptionHandler<ExeptionMiddleware>();
            builder.Services.AddProblemDetails();

            builder.Services.AddServices();

            builder.Services.AddRepositories();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DatabaseSeeder.Initialize(db);

            }

            app.UseMiddleware<ApplicationMiddleware>();
            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
