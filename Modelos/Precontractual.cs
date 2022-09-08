﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class Precontractual
    {
        [Key]
        [Required]
        public string IdPrecontractual { get; set; }

        [ForeignKey("IdPreparatoria")]
        public string? IdPreparatoria { get; set; }
        public Preparatoria? Preparatoria {get; set; }

        [ForeignKey("IdPrecontractual")]
        public ICollection<Contractual>? Contractuales { get; set; }

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
