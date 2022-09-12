using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcesoCompraController : ControllerBase
    {

        private readonly DatabaseContext context;

        public ProcesoCompraController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProcesoCompra>>> Get()
        {
            var procesos = await context.ProcesoCompras.ToListAsync();
            if (!procesos.Any())
                return NotFound();
            return procesos;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<ProcesoCompra>>> GetProceso(string id)
        {
            var proceso = await context.ProcesoCompras.Where(e => e.ProcesoCompraId.Equals(id)).FirstOrDefaultAsync();
            if (proceso == null)
                return NotFound();
            return Ok(proceso);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(ProcesoCompra procesoCompra)
        {
            var created = context.ProcesoCompras.Add(procesoCompra);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetProceso", new { id = procesoCompra.ProcesoCompraId }, created.Entity);

        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, ProcesoCompra procesoCompra)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.ProcesoCompras.Update(procesoCompra);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var proceso = await context.ProcesoCompras.FindAsync(id);
            context.ProcesoCompras.Remove(proceso);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.ProcesoCompras.AnyAsync(p => p.ProcesoCompraId == id);
        }
    }
}

