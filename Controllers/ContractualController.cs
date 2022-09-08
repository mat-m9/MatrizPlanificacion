using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/AlertaDSPPP")]
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
            var contractuales = await context.Contractuales.ToListAsync();
            if (!contractuales.Any())
                return NotFound();
            return contractuales;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Contractual>>> GetContractual(string id)
        {
            var contractual = await context.Contractuales.Where(e => e.ContractualId.Equals(id)).FirstOrDefaultAsync();
            if (contractual == null)
                return NotFound();
            return Ok(contractual);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Contractual contractual)
        {
            var created = context.Contractuales.Add(contractual);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetContractual", new { id = contractual.ContractualId }, created.Entity);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Contractual contractual)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Contractuales.Update(contractual);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var contractual = await context.Contractuales.FindAsync(id);
            context.Contractuales.Remove(contractual);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Contractuales.AnyAsync(p => p.ContractualId == id);
        }
    }
}
