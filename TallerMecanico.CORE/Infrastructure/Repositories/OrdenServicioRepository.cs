using Microsoft.EntityFrameworkCore;
using TallerMecanico.CORE.Core.Entities;
using TallerMecanico.CORE.Core.Interfaces;
using TallerMecanico.CORE.Infrastructure.Data;

namespace TallerMecanico.CORE.Infrastructure.Repositories
{
    // Implementación del repositorio. Vive en Infrastructure porque es
    // la que conoce el detalle técnico (Entity Framework Core / SQL Server).
    public class OrdenServicioRepository : IOrdenServicioRepository
    {
        private readonly TallerMecanicoDbContext _context;

        public OrdenServicioRepository(TallerMecanicoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdenServicio>> GetOrdenesServicio()
        {
            // Include para traer datos relacionados (placa / nombre del servicio)
            // y poder mapearlos en el DTO de salida.
            return await _context.OrdenServicio
                .Include(o => o.Vehiculo)
                .Include(o => o.TipoServicio)
                .ToListAsync();
        }

        public async Task<OrdenServicio?> GetOrdenServicioById(int id)
        {
            return await _context.OrdenServicio
                .Include(o => o.Vehiculo)
                .Include(o => o.TipoServicio)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task CreateOrdenServicio(OrdenServicio ordenServicio)
        {
            _context.OrdenServicio.Add(ordenServicio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrdenServicio(OrdenServicio ordenServicio)
        {
            _context.OrdenServicio.Update(ordenServicio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrdenServicio(int id)
        {
            var existing = await _context.OrdenServicio
                .FirstOrDefaultAsync(o => o.Id == id);
            if (existing != null)
            {
                _context.OrdenServicio.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
