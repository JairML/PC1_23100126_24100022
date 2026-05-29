using Microsoft.AspNetCore.Mvc;
using TallerMecanico.CORE.Core.DTOs;
using TallerMecanico.CORE.Core.Interfaces;

namespace TallerMecanico.API.Controllers
{
    // PREGUNTA 5: Controlador con patrón Repository + Interface.
    // El controlador SOLO conoce la interfaz IOrdenServicioService (DI).
    // No sabe nada de EF Core ni del repositorio: separación de capas.
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenServicioController : ControllerBase
    {
        private readonly IOrdenServicioService _ordenServicioService;

        public OrdenServicioController(IOrdenServicioService ordenServicioService)
        {
            _ordenServicioService = ordenServicioService;
        }

        // GET: api/OrdenServicio
        [HttpGet]
        public async Task<IActionResult> GetOrdenesServicio()
        {
            var ordenes = await _ordenServicioService.GetOrdenesServicio();
            return Ok(ordenes);
        }

        // GET: api/OrdenServicio/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdenServicioById(int id)
        {
            var orden = await _ordenServicioService.GetOrdenServicioById(id);
            if (orden == null)
            {
                return NotFound();
            }
            return Ok(orden);
        }

        // POST: api/OrdenServicio
        [HttpPost]
        public async Task<IActionResult> CreateOrdenServicio([FromBody] OrdenServicioCreateDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            await _ordenServicioService.CreateOrdenServicio(dto);
            return Ok();
        }

        // PUT: api/OrdenServicio
        [HttpPut]
        public async Task<IActionResult> UpdateOrdenServicio([FromBody] OrdenServicioUpdateDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var updated = await _ordenServicioService.UpdateOrdenServicio(dto);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/OrdenServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenServicio(int id)
        {
            var deleted = await _ordenServicioService.DeleteOrdenServicio(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
