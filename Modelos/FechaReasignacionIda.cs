using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrizPlanificacion.Modelos
{
    public partial class FechaReasignacionIda
    {
        [Key]
        [Required]
        public string IdIda { get; set; }

        [Required]
        [ForeignKey("ProcesoId")]
        public string ProcesoId { get; set; }
        public ProcesoCompra? ProcesoCompra {get; set;}

        [Required]
        [ForeignKey("AreaId")]
        public string AreaId { get; set; }
        public PlantaUnidadArea? Area { get; set; }

        [Required]
        public DateTime fechaIda { get; set; }
    }
}
