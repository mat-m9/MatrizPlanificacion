using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public class ItemPresupuestario
    {
        [Key]
        public string? itemId { get; set; }

        public string nunItem { get; set; } 
    }
}
