using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public class Observacion
    {
        [Key]
        [Required]
        public Guid ObservacionId { get; set; }

        [Required]
        public Guid ProcesoId { get; set; }
        [ForeignKey("ProcesoId")]
        public ProcesoCompra ProcesoCompra { get; set; }


        [Required]
        public DateOnly fechaObsservacion { get; set; }

        [Required]
        public string descripcionObservacion { get; set; }
    }
}
