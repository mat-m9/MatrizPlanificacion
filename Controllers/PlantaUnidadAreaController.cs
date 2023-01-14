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
    public class PlantaUnidadAreaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public PlantaUnidadAreaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Unidad>>> Get()
        {
            var areas = await context.PlantaUnidadAreas.ToListAsync();
            if (!areas.Any())
                return NotFound();
            return areas;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Unidad>>> GetArea(string id)
        {
            var area = await context.PlantaUnidadAreas.Where(e => e.UnidadId.Equals(id)).FirstOrDefaultAsync();
            if (area == null)
                return NotFound();
            return Ok(area);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Unidad plantaUnidadArea)
        {
            var created = context.PlantaUnidadAreas.Add(plantaUnidadArea);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetArea", new { id = plantaUnidadArea.UnidadId }, created.Entity);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Unidad plantaUnidadArea)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            plantaUnidadArea.UnidadId = id;
            context.PlantaUnidadAreas.Update(plantaUnidadArea);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var area = await context.PlantaUnidadAreas.FindAsync(id);
            context.PlantaUnidadAreas.Remove(area);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.PlantaUnidadAreas.AnyAsync(p => p.UnidadId == id);
        }
    }
}

