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
            var observaciones = await context.Observaciones.Include(p => p.Proceso).ToListAsync();
            if (!observaciones.Any())
                return NotFound();
            return observaciones;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Observacion>>> GetObservacion(string id)
        {
            var observacion = await context.Observaciones.Where(e => e.ObservacionId.Equals(id)).Include(p => p.Proceso).FirstOrDefaultAsync();
            if (observacion == null)
                return NotFound();
            return Ok(observacion);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Observacion observacion)
        {
            try
            {
                observacion.fechaObsservacion = DateTime.SpecifyKind(observacion.fechaObsservacion.Value, DateTimeKind.Utc);
                var created = context.Observaciones.Add(observacion);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetObservacion", new { id = observacion.ObservacionId }, created.Entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Observacion observacion)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            observacion.ObservacionId = id;
            try
            {
                observacion.fechaObsservacion = DateTime.SpecifyKind(observacion.fechaObsservacion.Value, DateTimeKind.Utc);
                context.Observaciones.Update(observacion);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
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
