using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var areas = await context.PlantaUnidadAreas.Include(p => p.Padre).ToListAsync();
            if (!areas.Any())
                return NotFound();
            return areas;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<PlantaUnidadArea>>> GetArea(string id)
        {
            var area = await context.PlantaUnidadAreas.Where(e => e.PlantaUnidadAreaId.Equals(id)).Include(p => p.Padre).FirstOrDefaultAsync();
            if (area == null)
                return NotFound();
            return Ok(area);
        }

        [HttpGet(template: ApiRoutes.Area.Tipo)]
        public async Task<ActionResult<ICollection<PlantaUnidadArea>>> Get(char tipo)
        {
            var areas = await context.PlantaUnidadAreas.Include(p => p.Padre).Where(p=>p.tipo == tipo) .ToListAsync();
            if (!areas.Any())
                return NotFound();
            return areas;
        }

        [HttpGet(template: ApiRoutes.Area.Padre)]
        public async Task<ActionResult<ICollection<PlantaUnidadArea>>> Get(string padreId)
        {
            var areas = await context.PlantaUnidadAreas.Include(p => p.Padre).Where(p => p.PadreId == padreId).ToListAsync();
            if (!areas.Any())
                return NotFound();
            return areas;
        }


        [HttpPost]
        public async Task<ActionResult<string>> Post(PlantaUnidadArea plantaUnidadArea)
        {
            if(plantaUnidadArea.tipo == 'P')
            {
                if(plantaUnidadArea.PadreId == null)
                {
                    var created = context.PlantaUnidadAreas.Add(plantaUnidadArea);
                    await context.SaveChangesAsync();
                    return CreatedAtAction("GetArea", new { id = plantaUnidadArea.PlantaUnidadAreaId }, created.Entity);
                }
                return BadRequest();
            }
            if(plantaUnidadArea.tipo == 'U')
            {
                var padre = await context.PlantaUnidadAreas.FindAsync(plantaUnidadArea.PadreId);
                if (padre != null) 
                {
                    if (padre.tipo == 'P')
                    {
                        var created = context.PlantaUnidadAreas.Add(plantaUnidadArea);
                        await context.SaveChangesAsync();
                        return CreatedAtAction("GetArea", new { id = plantaUnidadArea.PlantaUnidadAreaId }, created.Entity);
                    }
                }
                return BadRequest();
            }
            if(plantaUnidadArea.tipo == 'A')
            {
                var padre = await context.PlantaUnidadAreas.FindAsync(plantaUnidadArea.PadreId);
                if(padre != null)
                {
                    if (padre.tipo == 'U')
                    {
                        var created = context.PlantaUnidadAreas.Add(plantaUnidadArea);
                        await context.SaveChangesAsync();
                        return CreatedAtAction("GetArea", new { id = plantaUnidadArea.PlantaUnidadAreaId }, created.Entity);
                    }
                }
                return BadRequest();
            }

            return BadRequest();
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, PlantaUnidadArea plantaUnidadArea)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            plantaUnidadArea.PlantaUnidadAreaId = id;
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

