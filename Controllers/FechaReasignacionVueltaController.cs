using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/FechaReasignacionVuelta")]
    public class FechaReasignacionVueltaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public FechaReasignacionVueltaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<FechaReasignacionVuelta>>> Get()
        {
            return await context.FechaReasignacionVueltas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(FechaReasignacionVuelta fechaReasigVuelta)
        {
            context.Add(fechaReasigVuelta);
            await context.SaveChangesAsync();
            return fechaReasigVuelta.IdVuelta;
        }

        [HttpPut("{IdVuelta:Guid}")]
        public async Task<ActionResult> Put(Guid id, FechaReasignacionVuelta fechaReasigVuelta)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(fechaReasigVuelta);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{IdVuelta:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new FechaReasignacionVuelta() { IdVuelta = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.FechaReasignacionVueltas.AnyAsync(p => p.IdVuelta == id);
        }
    }
}

