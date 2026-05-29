using TallerMecanico.CORE.Core.DTOs;

namespace TallerMecanico.CORE.Core.Interfaces
{
    // Interfaz del servicio (capa intermedia / lógica de negocio).
    // Vive en Core y trabaja con DTOs, nunca con entidades, para no
    // exponer el modelo de datos hacia la capa de presentación.
    public interface IOrdenServicioService
    {
        Task<IEnumerable<OrdenServicioDTO>> GetOrdenesServicio();
        Task<OrdenServicioDTO?> GetOrdenServicioById(int id);
        Task CreateOrdenServicio(OrdenServicioCreateDTO ordenServicioCreateDTO);
        Task<bool> UpdateOrdenServicio(OrdenServicioUpdateDTO ordenServicioUpdateDTO);
        Task<bool> DeleteOrdenServicio(int id);
    }
}
