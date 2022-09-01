using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/PlantaUnidadArea")]
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
            return await context.PlantaUnidadAreas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(PlantaUnidadArea plantaUnidadArea)
        {
            context.Add(plantaUnidadArea);
            await context.SaveChangesAsync();
            return plantaUnidadArea.PlantaUnidadAreaId;
        }

        [HttpPut("{PlantaUnidadAreaId:Guid}")]
        public async Task<ActionResult> Put(Guid id, PlantaUnidadArea plantaUnidadArea)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(plantaUnidadArea);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{PlantaUnidadAreaId:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new PlantaUnidadArea() { PlantaUnidadAreaId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.PlantaUnidadAreas.AnyAsync(p => p.PlantaUnidadAreaId == id);
        }
    }
}

