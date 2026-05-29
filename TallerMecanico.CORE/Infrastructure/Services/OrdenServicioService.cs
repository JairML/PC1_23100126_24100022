using TallerMecanico.CORE.Core.DTOs;
using TallerMecanico.CORE.Core.Entities;
using TallerMecanico.CORE.Core.Interfaces;

namespace TallerMecanico.CORE.Infrastructure.Services
{
    // Capa intermedia (lógica de negocio). Recibe/devuelve DTOs y se
    // apoya en el repositorio para el acceso a datos. El controlador
    // nunca toca el repositorio ni el DbContext directamente.
    public class OrdenServicioService : IOrdenServicioService
    {
        private readonly IOrdenServicioRepository _ordenServicioRepository;

        public OrdenServicioService(IOrdenServicioRepository ordenServicioRepository)
        {
            _ordenServicioRepository = ordenServicioRepository;
        }

        public async Task<IEnumerable<OrdenServicioDTO>> GetOrdenesServicio()
        {
            var ordenes = await _ordenServicioRepository.GetOrdenesServicio();
            return ordenes.Select(MapToDTO).ToList();
        }

        public async Task<OrdenServicioDTO?> GetOrdenServicioById(int id)
        {
            var orden = await _ordenServicioRepository.GetOrdenServicioById(id);
            return orden == null ? null : MapToDTO(orden);
        }

        public async Task CreateOrdenServicio(OrdenServicioCreateDTO dto)
        {
            var orden = new OrdenServicio
            {
                FechaIngreso = dto.FechaIngreso,
                DescripcionProblema = dto.DescripcionProblema,
                CostoEstimado = dto.CostoEstimado,
                // Regla de negocio simple: si no envían estado, queda "Pendiente".
                Estado = string.IsNullOrWhiteSpace(dto.Estado) ? "Pendiente" : dto.Estado,
                VehiculoId = dto.VehiculoId,
                TipoServicioId = dto.TipoServicioId
            };
            await _ordenServicioRepository.CreateOrdenServicio(orden);
        }

        public async Task<bool> UpdateOrdenServicio(OrdenServicioUpdateDTO dto)
        {
            var existing = await _ordenServicioRepository.GetOrdenServicioById(dto.Id);
            if (existing == null) return false;

            existing.FechaIngreso = dto.FechaIngreso;
            existing.DescripcionProblema = dto.DescripcionProblema;
            existing.CostoEstimado = dto.CostoEstimado;
            existing.Estado = dto.Estado;
            existing.VehiculoId = dto.VehiculoId;
            existing.TipoServicioId = dto.TipoServicioId;

            await _ordenServicioRepository.UpdateOrdenServicio(existing);
            return true;
        }

        public async Task<bool> DeleteOrdenServicio(int id)
        {
            var existing = await _ordenServicioRepository.GetOrdenServicioById(id);
            if (existing == null) return false;

            await _ordenServicioRepository.DeleteOrdenServicio(id);
            return true;
        }

        // Mapeo Entidad -> DTO centralizado en un solo lugar.
        private static OrdenServicioDTO MapToDTO(OrdenServicio o) => new()
        {
            Id = o.Id,
            FechaIngreso = o.FechaIngreso,
            DescripcionProblema = o.DescripcionProblema,
            CostoEstimado = o.CostoEstimado,
            Estado = o.Estado,
            VehiculoId = o.VehiculoId,
            VehiculoPlaca = o.Vehiculo?.Placa,
            TipoServicioId = o.TipoServicioId,
            TipoServicioNombre = o.TipoServicio?.Nombre
        };
    }
}
