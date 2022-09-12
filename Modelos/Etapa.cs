using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class Etapa
    {
        [Key]
        public string? EtapaId { get; set; }

        [Required]
        [MaxLength(40)]
        public string tipoEtapa { get; set; }

        public ICollection<ProcesoCompra>? ProcesoCompras { get; set; }
    }
}
