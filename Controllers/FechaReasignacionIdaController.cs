using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/FechaReasignacionIda")]
    public class FechaReasignacionIdaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public FechaReasignacionIdaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<FechaReasignacionIda>>> Get()
        {
            return await context.FechaReasignacionIdas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(FechaReasignacionIda fechasReasigIda)
        {
            context.Add(fechasReasigIda);
            await context.SaveChangesAsync();
            return fechasReasigIda.IdIda;
        }

        [HttpPut("{IdIda:Guid}")]
        public async Task<ActionResult> Put(Guid id, FechaReasignacionIda fechasReasigIda)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(fechasReasigIda);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{IdIda:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new FechaReasignacionIda() { IdIda = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.FechaReasignacionIdas.AnyAsync(p => p.IdIda == id);
        }
    }
}

