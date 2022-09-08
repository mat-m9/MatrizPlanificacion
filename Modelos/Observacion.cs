using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public class Observacion
    {
        [Key]
        [Required]
        public string ObservacionId { get; set; }

        [Required]
        [ForeignKey("ProcesoId")]
        public string ProcesoId { get; set; }
        public ProcesoCompra ProcesoCompra { get; set; }


        [Required]
        public DateOnly fechaObsservacion { get; set; }

        [Required]
        public string descripcionObservacion { get; set; }
    }
}
