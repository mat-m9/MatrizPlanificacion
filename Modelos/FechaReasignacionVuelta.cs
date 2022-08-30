using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class FechaReasignacionVuelta
    {
        [Key]
        [Required]
        public Guid IdVuelta { get; set; }

        [Required]
        [ForeignKey("ProcesoId")]
        public Guid ProcesoId { get; set; }

        [Required]
        [ForeignKey("AreaId")]
        public Guid AreaId { get; set; }

        [Required]
        public DateTime fechaVuelta { get; set; }
    }
}
