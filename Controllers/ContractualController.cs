using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/Contractual")]
    public class ContractualController : ControllerBase
    {

        private readonly DatabaseContext context;

        public ContractualController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Contractual>>> Get()
        {
            return await context.Contractuales.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(Contractual contractual)
        {
            context.Add(contractual);
            await context.SaveChangesAsync();
            return contractual.ContractualId;
        }

        [HttpPut("{ContractualId:Guid}")]
        public async Task<ActionResult> Put(Guid id, Contractual contractual)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(contractual);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{ContractualId:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new Contractual() { ContractualId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.Contractuales.AnyAsync(p => p.ContractualId == id);
        }
    }
}
