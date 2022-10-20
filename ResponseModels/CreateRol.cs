using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.ResponseModels
{
    public class CreateRol
    {
        [Required]
        public string RolName { get; set; }    
    }
}
