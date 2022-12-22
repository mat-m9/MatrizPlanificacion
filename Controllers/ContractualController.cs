using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var contractuales = await context.Contractuales.Include(r => r.Precontractual).ToListAsync();
            if (!contractuales.Any())
                return NotFound();
            return contractuales;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Contractual>>> GetContractual(string id)
        {
            var contractual = await context.Contractuales.Where(e => e.ContractualId.Equals(id)).Include(r => r.Precontractual).FirstOrDefaultAsync();
            if (contractual == null)
                return NotFound();
            return Ok(contractual);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Contractual contractual)
        {
            try
            {
                contractual.fechaSuscripcion = DateTime.SpecifyKind(contractual.fechaSuscripcion.Value, DateTimeKind.Utc);
                var created = context.Contractuales.Add(contractual);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetContractual", new { id = contractual.ContractualId }, created.Entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Contractual contractual)
        {
            ProgressService progress = new ProgressService(context);
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            contractual.ContractualId = id;
            try
            {
                contractual.fechaSuscripcion = DateTime.SpecifyKind(contractual.fechaSuscripcion.Value, DateTimeKind.Utc);
                if(contractual.fechaSuscripcionReal != null)
                    contractual.fechaSuscripcionReal = DateTime.SpecifyKind(contractual.fechaSuscripcionReal.Value, DateTimeKind.Utc);
                context.Contractuales.Update(contractual);

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
