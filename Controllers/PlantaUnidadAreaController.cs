using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/AlertaDSPPP")]
    public class PlantaUnidadAreaController : ControllerBase
    {

        private readonly DatabaseContext context;

        public PlantaUnidadAreaController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PlantaUnidadArea>>> Get()
        {
            var areas = await context.PlantaUnidadAreas.ToListAsync();
            if (!areas.Any())
                return NotFound();
            return areas;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<PlantaUnidadArea>>> GetArea(string id)
        {
            var area = await context.PlantaUnidadAreas.Where(e => e.PlantaUnidadAreaId.Equals(id)).FirstOrDefaultAsync();
            if (area == null)
                return NotFound();
            return Ok(area);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(PlantaUnidadArea plantaUnidadArea)
        {
            var created = context.PlantaUnidadAreas.Add(plantaUnidadArea);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetArea", new { id = plantaUnidadArea.PlantaUnidadAreaId }, created.Entity);

        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, PlantaUnidadArea plantaUnidadArea)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

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
            return await context.PlantaUnidadAreas.AnyAsync(p => p.PlantaUnidadAreaId == id);
        }
    }
}

