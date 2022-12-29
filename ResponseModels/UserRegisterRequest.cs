using MatrizPlanificacion.Modelos;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.ResponseModels
{
    public class UserRegisterRequest
    { 
        public string userName { get; set; }

        [EmailAddress]
        public string email { get; set; }

        public string password { get; set; }

        public string rol { get; set; }

        public string planta { get; set; }
    }
}
