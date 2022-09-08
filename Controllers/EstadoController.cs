using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/AlertaDSPPP")]
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
            var estados = await context.Estados.ToListAsync();
            if (!estados.Any())
                return NotFound();
            return estados;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Estado>>> GetEstado(string id)
        {
            var estado = await context.Estados.Where(e => e.EstadoId.Equals(id)).FirstOrDefaultAsync();
            if (estado == null)
                return NotFound();
            return Ok(estado);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Estado estado)
        {
            var created = context.Estados.Add(estado);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetEstado", new {id = estado.EstadoId}, created.Entity);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Estado estado)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Estados.Update(estado);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var estado = await context.Estados.FindAsync(id);
            context.Estados.Remove(estado);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Estados.AnyAsync(p => p.EstadoId == id);
        }
    }
}

