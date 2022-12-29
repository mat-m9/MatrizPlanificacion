using MatrizPlanificacion.Modelos;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.ResponseModels
{
    public class UserRegisterRequest
    { 
        public string userName { get; set; }

        public string rol { get; set; }

        public string planta { get; set; }
    }
}
