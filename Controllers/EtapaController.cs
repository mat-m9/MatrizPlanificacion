using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/Etapa")]
    public class EtapaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public EtapaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Etapa>>> Get()
        {
            return await context.Etapas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(Etapa etapa)
        {
            context.Add(etapa);
            await context.SaveChangesAsync();
            return etapa.EtapaId;
        }

        [HttpPut("{EtapaId:Guid}")]
        public async Task<ActionResult> Put(Guid id, Etapa etapa)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(etapa);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{EtapaId:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new Etapa() { EtapaId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.Etapas.AnyAsync(p => p.EtapaId == id);
        }
    }
}
