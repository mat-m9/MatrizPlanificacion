using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FechaReasignacionVueltaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public FechaReasignacionVueltaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<FechaReasignacionVuelta>>> Get()
        {
            var fechaVueltas = await context.FechaReasignacionVueltas.Include(r => r.Area).Include(r => r.Proceso).ToListAsync();
            if (!fechaVueltas.Any())
                return NotFound();
            return fechaVueltas;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<FechaReasignacionVuelta>>> GetFechaVuelta(string id)
        {
            var fechaVueltas = await context.FechaReasignacionVueltas.Where(e => e.IdVuelta.Equals(id)).Include(r => r.Area).Include(r => r.Proceso).FirstOrDefaultAsync();
            if (fechaVueltas == null)
                return NotFound();
            return Ok(fechaVueltas);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(FechaReasignacionVuelta fechaReasigVuelta)
        {
            var created = context.FechaReasignacionVueltas.Add(fechaReasigVuelta);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetFechaVuelta", new { id = fechaReasigVuelta.IdVuelta }, created.Entity);

        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, FechaReasignacionVuelta fechaReasigVuelta)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            fechaReasigVuelta.IdVuelta = id;
            context.FechaReasignacionVueltas.Update(fechaReasigVuelta);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var fechaVuelta = await context.FechaReasignacionVueltas.FindAsync(id);
            context.FechaReasignacionVueltas.Remove(fechaVuelta);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.FechaReasignacionVueltas.AnyAsync(p => p.IdVuelta == id);
        }
    }
}

