using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var preparatorias = await context.Preparatorias.Include(p => p.ProcesoCompra).ToListAsync();
            if (!preparatorias.Any())
                return NotFound();
            return preparatorias;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Preparatoria>>> GetPreparatoria(string id)
        {
            var preparatoria = await context.Preparatorias.Where(e => e.PreparatoriaId.Equals(id)).Include(p => p.ProcesoCompra).FirstOrDefaultAsync();
            if (preparatoria == null)
                return NotFound();
            return Ok(preparatoria);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Preparatoria preparatoria)
        {
            try
            {
                preparatoria.fechaProgramada = DateTime.SpecifyKind(preparatoria.fechaProgramada.Value, DateTimeKind.Utc);
                preparatoria.fechaSolicitud = DateTime.SpecifyKind(preparatoria.fechaSolicitud.Value, DateTimeKind.Utc);
                preparatoria.fechaRespuesta = DateTime.SpecifyKind(preparatoria.fechaRespuesta.Value, DateTimeKind.Utc);
                preparatoria.fechaMesa = DateTime.SpecifyKind(preparatoria.fechaMesa.Value, DateTimeKind.Utc);
                preparatoria.fechaEmision = DateTime.SpecifyKind(preparatoria.fechaEmision.Value, DateTimeKind.Utc);
                preparatoria.fechaReal = DateTime.SpecifyKind(preparatoria.fechaReal.Value, DateTimeKind.Utc);
                preparatoria.fechaAutorizacion = DateTime.SpecifyKind(preparatoria.fechaAutorizacion.Value, DateTimeKind.Utc);
                preparatoria.fechaPublicacion = DateTime.SpecifyKind(preparatoria.fechaPublicacion.Value, DateTimeKind.Utc);
                var created = context.Preparatorias.Add(preparatoria);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetEstado", new { id = preparatoria.PreparatoriaId }, created.Entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Preparatoria preparatoria)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            preparatoria.PreparatoriaId = id;
            try
            {
                preparatoria.fechaProgramada = DateTime.SpecifyKind(preparatoria.fechaProgramada.Value, DateTimeKind.Utc);
                preparatoria.fechaSolicitud = DateTime.SpecifyKind(preparatoria.fechaSolicitud.Value, DateTimeKind.Utc);
                preparatoria.fechaRespuesta = DateTime.SpecifyKind(preparatoria.fechaRespuesta.Value, DateTimeKind.Utc);
                preparatoria.fechaMesa = DateTime.SpecifyKind(preparatoria.fechaMesa.Value, DateTimeKind.Utc);
                preparatoria.fechaEmision = DateTime.SpecifyKind(preparatoria.fechaEmision.Value, DateTimeKind.Utc);
                preparatoria.fechaReal = DateTime.SpecifyKind(preparatoria.fechaReal.Value, DateTimeKind.Utc);
                preparatoria.fechaAutorizacion = DateTime.SpecifyKind(preparatoria.fechaAutorizacion.Value, DateTimeKind.Utc);
                preparatoria.fechaPublicacion = DateTime.SpecifyKind(preparatoria.fechaPublicacion.Value, DateTimeKind.Utc);
                context.Preparatorias.Update(preparatoria);
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

            var preparatoria = await context.Preparatorias.FindAsync(id);
            context.Preparatorias.Remove(preparatoria);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Preparatorias.AnyAsync(p => p.PreparatoriaId == id);
        }
    }
}
