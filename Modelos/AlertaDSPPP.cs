using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class AlertaDSPPP
    {
        [Key]
        [Required]
        public string AlertaDSPPPId { get; set; }

        [ForeignKey("ProcesoCompraId")]
        [Required]
        public string ProcesoCompraId { get; set; }
        public ProcesoCompra ProcesoCompra { get; set; }

        [Required]
        public string descripcionAlerta { get; set; }

    }
}
