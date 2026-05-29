using TallerMecanico.CORE.Core.Entities;

namespace TallerMecanico.CORE.Core.Interfaces
{
    // Interfaz del repositorio. Vive en Core para que el resto de las
    // capas dependa de la abstracción y no de la implementación (DIP).
    // Trabaja directamente con entidades.
    public interface IOrdenServicioRepository
    {
        Task<IEnumerable<OrdenServicio>> GetOrdenesServicio();
        Task<OrdenServicio?> GetOrdenServicioById(int id);
        Task CreateOrdenServicio(OrdenServicio ordenServicio);
        Task UpdateOrdenServicio(OrdenServicio ordenServicio);
        Task DeleteOrdenServicio(int id);
    }
}
