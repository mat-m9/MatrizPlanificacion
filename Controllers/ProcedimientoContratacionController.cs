using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProcedimientoContratacionController : Controller
    {

        private readonly DatabaseContext context;

        public ProcedimientoContratacionController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProcedimientoContratacion>>> Get()
        {
            var procedimientos = await context.ProcedimientoContrataciones.ToListAsync();
            if (!procedimientos.Any())
                return NotFound();
            return procedimientos;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<ProcedimientoContratacion>>> GetProcedimiento(string id)
        {
            var procedimiento = await context.ProcedimientoContrataciones.Where(e => e.ProcedimientoContratacionId.Equals(id)).FirstOrDefaultAsync();
            if (procedimiento == null)
                return NotFound();
            return Ok(procedimiento);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(ProcedimientoContratacion procedimientoContratacion)
        {
            var created = context.ProcedimientoContrataciones.Add(procedimientoContratacion);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetProcedimiento", new { id = procedimientoContratacion.ProcedimientoContratacionId }, created.Entity);

        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, ProcedimientoContratacion procedimientoContratacion)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            procedimientoContratacion.ProcedimientoContratacionId = id;
            context.ProcedimientoContrataciones.Update(procedimientoContratacion);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var procedimiento = await context.ProcedimientoContrataciones.FindAsync(id);
            context.ProcedimientoContrataciones.Remove(procedimiento);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.ProcedimientoContrataciones.AnyAsync(p => p.ProcedimientoContratacionId == id);
        }
    }
}
