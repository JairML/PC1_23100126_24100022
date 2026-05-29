using Microsoft.AspNetCore.Mvc;

namespace TallerMecanico.API.Controllers
{
    // Endpoint simple de estado para verificar que la API está activa.
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        // GET: api/Health
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "OK",
                servicio = "TallerMecanico API",
                fecha = DateTime.Now
            });
        }
    }
}
