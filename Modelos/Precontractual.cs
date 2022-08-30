using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class Precontractual
    {
        [Key]
        [Required]
        public Guid PrecontractualId { get; set; }

        [Required]
        public Guid PreparatoriaId { get; set; }
        [ForeignKey("PreparatoriaId")]
        public Preparatoria Preparatoria { get; set; }

        public Guid ContractualId { get; set; }
        [ForeignKey("ContractualId")]
        public Contractual Contractual { get; set; }

        [DataType(DataType.Date)]
        public DateOnly fechaAdjudicacion { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        [DataType(DataType.Currency)]
        public decimal valorAdjudicado { get; set; }

        [Required]
        public string administradorContrato { get; set; }

    }
}
