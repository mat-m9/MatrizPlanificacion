using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrizPlanificacion.Modelos
{
    public partial class FechaReasignacionIda
    {
        [Key]
        [Required]
        public Guid IdIda { get; set; }

        [Required]
        [ForeignKey("ProcesoId")]
        public Guid ProcesoId { get; set; }

        [Required]
        [ForeignKey("AreaId")]
        public Guid AreaId { get; set; }

        [Required]
        public DateTime fechaIda { get; set; }
    }
}
