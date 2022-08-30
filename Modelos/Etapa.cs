using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class Etapa
    {
        [Key]
        [Required]
        public Guid EtapaId { get; set; }

        [Required]
        [StringLength(40)]
        public string tipoEtapa { get; set; }

        public ICollection<ProcesoCompra> ProcesoCompras { get; set; }
    }
}
