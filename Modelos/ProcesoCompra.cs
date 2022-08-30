using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MatrizPlanificacion.Modelos
{
    public partial class ProcesoCompra
    {
        [Key]
        [Required]
        public Guid ProcesoCompraId { get; set; }

        [Required]
        [ForeignKey("ProcedimientoId")]
        public Guid ProcedimientoId { get; set; }
        public ProcedimientoContratacion ProcedimientoContratacion { get; set; }

        [ForeignKey("PreparatoriaPId")]
        public Guid PreparatoriaPId { get; set; }
        public Preparatoria Preparatoria { get; set; }

        [Required]
        public Guid EstadoId { get; set; }
        [ForeignKey("EstadoId")]
        public Estado Estado { get; set; }


        [Required]
        public Guid EtapaId { get; set; }
        [ForeignKey("EtapaId")]
        public Etapa Etapa { get; set; }

        [Required]
        public Guid PlantaId { get; set; }
        [ForeignKey("PlantaId")]
        public PlantaUnidadArea PlantaUnidadArea { get; set; }

        [Required]
        [Display(Name = "Nro. Proceso")]
        [RegularExpression("([1-9][0-9]*)")]
        public int numProceso { get; set; }

        [Required]
        public long cpc { get; set; }

        [Required]
        [Display(Name = "Grupo de gasto")]
        [RegularExpression("([1-9][0-9]*)")]
        public int grupoGasto { get; set; }

        [Required]
        [Display(Name = "Item Presupuestario")]
        [RegularExpression("([1-9][0-9]*)")]
        public long itemPresup { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Total (iva)")]
        public decimal total { get; set; }

        [Required]
        [Display(Name = "Cuatrimestre Planificado")]
        [RegularExpression("([1-9][0-9]*)")]
        public int cuatrimestre { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mes Planificado")]
        public string mesPlanificado { get; set; }


        public ICollection<AlertaDSPPP> AlertasDSPPP { get; set; }
        public ICollection<Observacion> Observaciones { get; set; }

    }

}
