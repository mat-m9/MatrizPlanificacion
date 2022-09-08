﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public partial class PlantaUnidadArea
    {
        [Key]
        [Required]
        public string PlantaUnidadAreaId { get; set; }

        [ForeignKey("PadreId")]
        public string PadreId { get; set; }
        public PlantaUnidadArea Padre { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public char tipo { get; set; }


        public ICollection<PlantaUnidadArea> PlantaUnidadAreas { get; set; }

        public ICollection<ProcesoCompra> ProcesoCompras { get; set; }

        public ICollection<User> Usuarios { get; set; }
    }
}
