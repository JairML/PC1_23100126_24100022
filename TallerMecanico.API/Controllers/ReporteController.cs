using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.CORE.Infrastructure.Data;

namespace TallerMecanico.API.Controllers
{
    // Controlador de reportes. Usa Include para traer datos relacionados
    // (clientes con la lista de sus vehículos) en una sola consulta.
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly TallerMecanicoDbContext _context;

        public ReporteController(TallerMecanicoDbContext context)
        {
            _context = context;
        }

        // GET: api/Reporte/clientes-con-vehiculos
        [HttpGet("clientes-con-vehiculos")]
        public async Task<IActionResult> GetClientesConVehiculos()
        {
            var data = await _context.Cliente
                .Include(c => c.Vehiculo)
                .Select(c => new
                {
                    c.Id,
                    cliente = c.Nombres + " " + c.Paterno + " " + c.Materno,
                    totalVehiculos = c.Vehiculo.Count,
                    vehiculos = c.Vehiculo.Select(v => new { v.Placa, v.Marca, v.Modelo, v.Anio })
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}
