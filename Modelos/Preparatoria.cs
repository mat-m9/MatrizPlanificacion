using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MatrizPlanificacion.Modelos
{
    public partial class Preparatoria
    {
        [Key]
        public string? PreparatoriaId { get; set; }

        [ForeignKey("IdProcesoCompra")]
        public string? IdProcesoCompra { get; set; }
        public ProcesoCompra? ProcesoCompra { get; set; }

        [DataType(DataType.Date)]
        public DateTime? informeNecesidad { get; set; }
        [DataType(DataType.Date)]
        public DateTime? informeNecesidadReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? terminosReferencia { get; set; }
        [DataType(DataType.Date)]
        public DateTime? terminosReferenciaReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? solicitudPublicacion { get; set; }
        [DataType(DataType.Date)]
        public DateTime? solicitudPublicacionReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? publicacionNecesidad { get; set; }
        [DataType(DataType.Date)]
        public DateTime? publicacionNecesidadReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? recepcionCotizaciones { get; set; }
        [DataType(DataType.Date)]
        public DateTime? recepcionCotizacionesReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? elaboracionEstudioMercado { get; set; }
        [DataType(DataType.Date)]
        public DateTime? elaboracionEstudioMercadoReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? solicitudPAPP { get; set; }
        [DataType(DataType.Date)]
        public DateTime? solicitudPAPPReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? emisionPAPP { get; set; }
        [DataType(DataType.Date)]
        public DateTime? emisionPAPPReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? solicitudPresup { get; set; }
        [DataType(DataType.Date)]
        public DateTime? solicitudPresupReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? emisionPresup { get; set; }
        [DataType(DataType.Date)]
        public DateTime? emisionPresupReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? solicitudPAC { get; set; }
        [DataType(DataType.Date)]
        public DateTime? solicitudPACReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? emisionPAC { get; set; }
        [DataType(DataType.Date)]
        public DateTime? emisionPACReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? solicitudCoordinadorZonal { get; set; }
        [DataType(DataType.Date)]
        public DateTime? solicitudCoordinadorZonalReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? resolucionInicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime? resolucionInicioReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? publicacionProceso { get; set; }
        [DataType(DataType.Date)]
        public DateTime? publicacionProcesoReal { get; set; }
    }

}
