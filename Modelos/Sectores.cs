using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public class Sectores
    {
        [Key]
        public string? sectorId { get; set; }

        public string? sectorUnidad { get; set; }
    }
}
