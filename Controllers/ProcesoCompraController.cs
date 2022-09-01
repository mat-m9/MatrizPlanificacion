using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/ProcesoCompra")]
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
            return await context.ProcesoCompras.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(ProcesoCompra procesoCompra)
        {
            context.Add(procesoCompra);
            await context.SaveChangesAsync();
            return procesoCompra.ProcesoCompraId;
        }

        [HttpPut("{ProcesoCompraId:Guid}")]
        public async Task<ActionResult> Put(Guid id, ProcesoCompra procesoCompra)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(procesoCompra);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{ProcesoCompraId:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new ProcesoCompra() { ProcesoCompraId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.ProcesoCompras.AnyAsync(p => p.ProcesoCompraId == id);
        }
    }
}

