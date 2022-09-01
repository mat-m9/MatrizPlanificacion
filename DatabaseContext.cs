using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion
{
    public partial class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlertaDSPPP> Alertas { get; set; } = null!;
        public virtual DbSet<Contractual> Contractuales { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Etapa> Etapas { get; set; } = null!;
        public virtual DbSet<Observacion> Observaciones { get; set; } = null!;
        public virtual DbSet<PlantaUnidadArea> PlantaUnidadAreas { get; set; } = null!;
        public virtual DbSet<Precontractual> Precontractuales { get; set; } = null!;
        public virtual DbSet<Preparatoria> Preparatorias { get; set; } = null!;
        public virtual DbSet<ProcedimientoContratacion> ProcedimientoContrataciones { get; set; } = null!;
        public virtual DbSet<ProcesoCompra> ProcesoCompras { get; set; } = null!;
        public virtual DbSet<User> Usuarios { get; set; } = null!;
        public virtual DbSet<FechaReasignacionIda> FechaReasignacionIdas { get; set; } = null!;
        public virtual DbSet<FechaReasignacionVuelta> FechaReasignacionVueltas { get; set; } = null!;
    }

    
}
