using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrecontractualController : ControllerBase
    {

        private readonly DatabaseContext context;

        public PrecontractualController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Precontractual>>> Get()
        {
            var precontractuales = await context.Precontractuales.Include(p => p.Preparatoria).ToListAsync();
            if (!precontractuales.Any())
                return NotFound();
            return precontractuales;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Precontractual>>> GetPrecontractual(string id)
        {
            var precontractual = await context.Precontractuales.Where(e => e.IdPrecontractual.Equals(id)).Include(p => p.Preparatoria).FirstOrDefaultAsync();
            if (precontractual == null)
                return NotFound();
            return Ok(precontractual);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Precontractual precontractual)
        {
            try
            {
                precontractual.fechaAdjudicacion = DateTime.SpecifyKind(precontractual.fechaAdjudicacion.Value, DateTimeKind.Utc);
                var created = context.Precontractuales.Add(precontractual);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetPrecontractual", new { id = precontractual.IdPrecontractual }, created.Entity);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }      
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Precontractual precontractual)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            precontractual.IdPrecontractual = id;
            try
            {
                precontractual.fechaAdjudicacion = DateTime.SpecifyKind(precontractual.fechaAdjudicacion.Value, DateTimeKind.Utc);
                context.Precontractuales.Update(precontractual);
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

            var precontactual = await context.Precontractuales.FindAsync(id);
            context.Precontractuales.Remove(precontactual);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Precontractuales.AnyAsync(p => p.IdPrecontractual == id);
        }
    }
}

