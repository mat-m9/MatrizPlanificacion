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
        public async Task<ActionResult<ICollection<PlantaUnidadArea>>> GetArea(Guid id)
        {
            var area = await context.PlantaUnidadAreas.Where(e => e.PlantaUnidadAreaId.Equals(id)).FirstOrDefaultAsync();
            if (area == null)
                return NotFound();
            return Ok(area);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(PlantaUnidadArea plantaUnidadArea)
        {
            var created = context.PlantaUnidadAreas.Add(plantaUnidadArea));
            await context.SaveChangesAsync();
            return CreatedAtAction("GetArea", new { id = plantaUnidadArea.PlantaUnidadAreaId }, created.Entity);

        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(Guid id, PlantaUnidadArea plantaUnidadArea)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.PlantaUnidadAreas.Update(plantaUnidadArea);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var area = await context.PlantaUnidadAreas.FindAsync(id);
            context.PlantaUnidadAreas.Remove(area);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.PlantaUnidadAreas.AnyAsync(p => p.PlantaUnidadAreaId == id);
        }
    }
}

