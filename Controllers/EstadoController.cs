using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/Estado")]
    public class EstadoController : ControllerBase
    {

        private readonly DatabaseContext context;

        public EstadoController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Estado>>> Get()
        {
            return await context.Estados.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(Estado estado)
        {
            context.Add(estado);
            await context.SaveChangesAsync();
            return estado.EstadoId;
        }

        [HttpPut("{EstadoId:Guid}")]
        public async Task<ActionResult> Put(Guid id, Estado estado)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(estado);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{EstadoId:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new Estado() { EstadoId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.Estados.AnyAsync(p => p.EstadoId == id);
        }
    }
}

