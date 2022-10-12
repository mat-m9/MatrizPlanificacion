using MatrizPlanificacion.Modelos;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.ResponseModels
{
    public class UserRegisterRequest
    { 
        [EmailAddress]
        public string email { get; set; }

        public string password { get; set; }

        public PlantaUnidadArea? planta { get; set; }
    }
}
