using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/ProcedimientoContratacion")]
    public class ProcedimientoContratacionController : ControllerBase
    {

        private readonly DatabaseContext context;

        public ProcedimientoContratacionController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProcedimientoContratacion>>> Get()
        {
            return await context.ProcedimientoContrataciones.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(ProcedimientoContratacion procedimientoContratacion)
        {
            context.Add(procedimientoContratacion);
            await context.SaveChangesAsync();
            return procedimientoContratacion.ProcedimientoContratacionId;
        }

        [HttpPut("{ProcedimientoContratacionId:Guid}")]
        public async Task<ActionResult> Put(Guid id, ProcedimientoContratacion procedimientoContratacion)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(procedimientoContratacion);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{ProcedimientoContratacionId:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new ProcedimientoContratacion() { ProcedimientoContratacionId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.ProcedimientoContrataciones.AnyAsync(p => p.ProcedimientoContratacionId == id);
        }
    }
}
