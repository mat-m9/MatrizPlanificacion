using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.ResponseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion
{
    public class DatabaseContextLogs : DbContext
    {
        public DatabaseContextLogs(DbContextOptions<DatabaseContextLogs> options)
            : base(options)
        {
        }

        public virtual DbSet<Logs> Logs { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Logs>(o =>
               o.Property(x => x.Id)
               .HasDefaultValue("uuid_generate_v4()")
               .ValueGeneratedOnAdd());

            base.OnModelCreating(builder);
        }

       
    }
}
