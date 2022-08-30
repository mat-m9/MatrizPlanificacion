using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class AlertaDSPPP
    {
        [Key]
        [Required]
        public Guid AlertaDSPPPId { get; set; }

        [Required]
        public Guid ProcesoCompraId { get; set; }
        [ForeignKey("ProcesoCompraId")]
        public ProcesoCompra ProcesoCompra { get; set; }

        [Required]
        public string descripcionAlerta { get; set; }

    }
}
