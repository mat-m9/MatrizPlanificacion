using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObservacionController : ControllerBase
    {

        private readonly DatabaseContext context;

        public ObservacionController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Observacion>>> Get()
        {
            var observaciones = await context.Observaciones.ToListAsync();
            if (!observaciones.Any())
                return NotFound();
            return observaciones;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Observacion>>> GetObservacion(string id)
        {
            var observacion = await context.Observaciones.Where(e => e.ObservacionId.Equals(id)).FirstOrDefaultAsync();
            if (observacion == null)
                return NotFound();
            return Ok(observacion);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Observacion observacion)
        {
            var created = context.Observaciones.Add(observacion);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetEstado", new { id = observacion.ObservacionId }, created.Entity);

        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Observacion observacion)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Observaciones.Update(observacion);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var observacion = await context.Observaciones.FindAsync(id);
            context.Observaciones.Remove(observacion);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Observaciones.AnyAsync(p => p.ObservacionId == id);
        }
    }
}
