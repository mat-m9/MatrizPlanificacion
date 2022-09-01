using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/Observacion")]
    public class ObservacionController : ControllerBase
    {

        private readonly DatabaseContext context;

        public ObservacionController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Observacion>>> Get()
        {
            return await context.Observaciones.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(Observacion observacion)
        {
            context.Add(observacion);
            await context.SaveChangesAsync();
            return observacion.ObservacionId;
        }

        [HttpPut("{ObservacionId:Guid}")]
        public async Task<ActionResult> Put(Guid id, Observacion observacion)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(observacion);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{ObservacionId:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new Observacion() { ObservacionId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.Observaciones.AnyAsync(p => p.ObservacionId == id);
        }
    }
}
