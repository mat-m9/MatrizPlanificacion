using MatrizPlanificacion.Modelos;
using System.ComponentModel.DataAnnotations;

namespace MatrizPlanificacion.ResponseModels
{
    public class RolRequest
    {
        public string UserName { get; set; }
        public string Rol { get; set; }
    }
}
