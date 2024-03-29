﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class ProcedimientoContratacion
    {
        [Key]
        public string? ProcedimientoContratacionId { get; set; }
        

        [Required]
        [StringLength(32)]
        public string tipoProcedimiento { get; set; }

        public ICollection<ProcesoCompra>? ProcesoCompra { get; set; }

    }
}
