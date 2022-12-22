using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrecontractualController : ControllerBase
    {

        private readonly DatabaseContext context;

        public PrecontractualController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Precontractual>>> Get()
        {
            var precontractuales = await context.Precontractuales.Include(p => p.Preparatoria).ToListAsync();
            if (!precontractuales.Any())
                return NotFound();
            return precontractuales;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Precontractual>>> GetPrecontractual(string id)
        {
            var precontractual = await context.Precontractuales.Where(e => e.IdPrecontractual.Equals(id)).Include(p => p.Preparatoria).FirstOrDefaultAsync();
            if (precontractual == null)
                return NotFound();
            return Ok(precontractual);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Precontractual precontractual)
        {
            try
            {
                precontractual.preguntasRespuestas = DateTime.SpecifyKind(precontractual.preguntasRespuestas.Value, DateTimeKind.Utc);
                precontractual.recepcionOfertas = DateTime.SpecifyKind(precontractual.recepcionOfertas.Value, DateTimeKind.Utc);
                precontractual.calificacionOfertas = DateTime.SpecifyKind(precontractual.calificacionOfertas.Value, DateTimeKind.Utc);
                precontractual.pujaNegociacion = DateTime.SpecifyKind(precontractual.pujaNegociacion.Value, DateTimeKind.Utc);
                precontractual.adjudicacion = DateTime.SpecifyKind(precontractual.adjudicacion.Value, DateTimeKind.Utc);

                var created = context.Precontractuales.Add(precontractual);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetPrecontractual", new { id = precontractual.IdPrecontractual }, created.Entity);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }      
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Precontractual precontractual)
        {
            ProgressService progress = new ProgressService(context);
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            precontractual.IdPrecontractual = id;
            try
            {
                precontractual.preguntasRespuestas = DateTime.SpecifyKind(precontractual.preguntasRespuestas.Value, DateTimeKind.Utc);
                if(precontractual.preguntasRespuestasReal != null)
                    precontractual.preguntasRespuestasReal = DateTime.SpecifyKind(precontractual.preguntasRespuestasReal.Value, DateTimeKind.Utc);
                precontractual.recepcionOfertas = DateTime.SpecifyKind(precontractual.recepcionOfertas.Value, DateTimeKind.Utc);
                if(precontractual.recepcionOfertasReal != null)
                    precontractual.recepcionOfertasReal = DateTime.SpecifyKind(precontractual.recepcionOfertasReal.Value, DateTimeKind.Utc);
                precontractual.calificacionOfertas = DateTime.SpecifyKind(precontractual.calificacionOfertas.Value, DateTimeKind.Utc);
                if(precontractual.calificacionOfertasReal != null)
                    precontractual.calificacionOfertasReal = DateTime.SpecifyKind(precontractual.calificacionOfertasReal.Value, DateTimeKind.Utc);
                precontractual.pujaNegociacion = DateTime.SpecifyKind(precontractual.pujaNegociacion.Value, DateTimeKind.Utc);
                if(precontractual.pujaNegociacionReal != null)
                    precontractual.pujaNegociacionReal = DateTime.SpecifyKind(precontractual.pujaNegociacionReal.Value, DateTimeKind.Utc);
                precontractual.adjudicacion = DateTime.SpecifyKind(precontractual.adjudicacion.Value, DateTimeKind.Utc);
                if(precontractual.adjudicacionReal != null)
                    precontractual.adjudicacionReal = DateTime.SpecifyKind(precontractual.adjudicacionReal.Value, DateTimeKind.Utc);
                context.Precontractuales.Update(precontractual);

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

            var precontactual = await context.Precontractuales.FindAsync(id);
            context.Precontractuales.Remove(precontactual);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Precontractuales.AnyAsync(p => p.IdPrecontractual == id);
        }
    }
}

