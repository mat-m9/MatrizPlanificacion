using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public class Observacion
    {
        [Key]
        public string? ObservacionId { get; set; }

        [Required]
        [ForeignKey("ProcesoId")]
        public string ProcesoId { get; set; }
        public ProcesoCompra Proceso { get; set; }


        [Required]
        public DateOnly fechaObsservacion { get; set; }

        [Required]
        public string descripcionObservacion { get; set; }
    }
}
