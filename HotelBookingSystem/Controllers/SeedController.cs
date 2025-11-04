using HotelBooking.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        [HttpPost("reset")]
        public async Task<IActionResult> ResetAndSeed([FromServices] ResetDataService resetDataService)
        {
            await resetDataService.ResetAndSeedAsync();
            return Ok("Database has been reset and seeded.");
        }
    }
}
