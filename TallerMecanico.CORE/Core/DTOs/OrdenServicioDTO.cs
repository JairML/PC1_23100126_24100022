using System;

namespace TallerMecanico.CORE.Core.DTOs
{
    // DTO de lectura: se devuelve al cliente. Incluye datos legibles
    // de las relaciones para no exponer la entidad completa.
    public class OrdenServicioDTO
    {
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string DescripcionProblema { get; set; } = null!;
        public decimal CostoEstimado { get; set; }
        public string Estado { get; set; } = null!;
        public int VehiculoId { get; set; }
        public string? VehiculoPlaca { get; set; }
        public int TipoServicioId { get; set; }
        public string? TipoServicioNombre { get; set; }
    }

    // DTO de creación: no incluye Id porque es autogenerado.
    public class OrdenServicioCreateDTO
    {
        public DateTime FechaIngreso { get; set; }
        public string DescripcionProblema { get; set; } = null!;
        public decimal CostoEstimado { get; set; }
        public string Estado { get; set; } = null!;
        public int VehiculoId { get; set; }
        public int TipoServicioId { get; set; }
    }

    // DTO de actualización: requiere el Id del registro a modificar.
    public class OrdenServicioUpdateDTO
    {
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string DescripcionProblema { get; set; } = null!;
        public decimal CostoEstimado { get; set; }
        public string Estado { get; set; } = null!;
        public int VehiculoId { get; set; }
        public int TipoServicioId { get; set; }
    }
}
