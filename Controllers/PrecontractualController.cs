using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/Precontractual")]
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
            return await context.Precontractuales.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(Precontractual precontractual)
        {
            context.Add(precontractual);
            await context.SaveChangesAsync();
            return precontractual.IdPreparatoria;
        }

        [HttpPut("{PrecontractualId:Guid}")]
        public async Task<ActionResult> Put(Guid id, Precontractual precontractual)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(precontractual);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{PrecontractualId:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new Precontractual() { IdPrecontractual = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.Precontractuales.AnyAsync(p => p.IdPrecontractual == id);
        }
    }
}

