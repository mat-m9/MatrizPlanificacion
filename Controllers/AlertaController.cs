using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/AlertaDSPPP")]
    public class AlertaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public AlertaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<AlertaDSPPP>>> Get()
        {
            return await context.Alertas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(AlertaDSPPP alerta)
        {
            context.Add(alerta);
            await context.SaveChangesAsync();
            return alerta.AlertaDSPPPId;
        }

        [HttpPut("{AlertaDSPPPId:Guid}")]
        public async Task<ActionResult> Put(Guid id, AlertaDSPPP alerta)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(alerta);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{AlertaDSPPPId:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new AlertaDSPPP() { AlertaDSPPPId = id});
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.Alertas.AnyAsync(p => p.AlertaDSPPPId == id);
        }
    }
}
