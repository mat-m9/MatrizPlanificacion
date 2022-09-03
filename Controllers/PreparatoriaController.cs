using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/Preparatoria")]
    public class PreparatoriaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public PreparatoriaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Preparatoria>>> Get()
        {
            return await context.Preparatorias.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(Preparatoria preparatoria)
        {
            context.Add(preparatoria);
            await context.SaveChangesAsync();
            return preparatoria.PreparatoriaId;
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(Guid id, Preparatoria preparatoria)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(preparatoria);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new Preparatoria() { PreparatoriaId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.Preparatorias.AnyAsync(p => p.PreparatoriaId == id);
        }
    }
}
