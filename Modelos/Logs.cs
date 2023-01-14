using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.Modelos
{
    public class Logs
    {
        [Key]
        public string? Id { get; set; }
        public string userName { get; set; }
        public string action { get; set; }
        [DataType(DataType.Date)]
        public DateTime? date { get; set; }
    }
}
