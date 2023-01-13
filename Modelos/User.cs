using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MatrizPlanificacion.Modelos
{
    public partial class User : IdentityUser
    {
        public string AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Unidad? Area { get; set; }

        public bool needChange { get; set; } = true;
    }
}
