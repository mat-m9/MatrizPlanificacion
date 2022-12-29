using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class Precontractual
    {
        [Key]
        public string? IdPrecontractual { get; set; }

        [ForeignKey("IdPreparatoria")]
        public string? IdPreparatoria { get; set; }
        public Preparatoria? Preparatoria {get; set; }


        [DataType(DataType.Date)]
        public DateTime? preguntasRespuestas { get; set; }
        [DataType(DataType.Date)]
        public DateTime? preguntasRespuestasReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? recepcionOfertas { get; set; }
        [DataType(DataType.Date)]
        public DateTime? recepcionOfertasReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? calificacionOfertas { get; set; }
        [DataType(DataType.Date)]
        public DateTime? calificacionOfertasReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? pujaNegociacion { get; set; }
        [DataType(DataType.Date)]
        public DateTime? pujaNegociacionReal { get; set; }

        [DataType(DataType.Date)]
        public DateTime? adjudicacion { get; set; }
        [DataType(DataType.Date)]
        public DateTime? adjudicacionReal { get; set; }
    }
}
