﻿using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class Estado
    {
        [Key]
        public string? EstadoId { get; set; }

        [Required]
        public string tipoEstado { get; set; }

        public ICollection<ProcesoCompra>? ProcesoCompras { get; set; }
    }
}
