using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.ResponseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion
{
    public partial class DatabaseContextLogs : IdentityDbContext<User>
    {
        public DatabaseContextLogs(DbContextOptions<DatabaseContextLogs> options)
            : base(options)
        {
        }

        public virtual DbSet<Logs> Logs { get; set; } = null!;
       

       
    }
}
