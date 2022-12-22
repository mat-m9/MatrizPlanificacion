using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class sectorController :ControllerBase
    {
        private readonly DatabaseContext context;

        public sectorController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Sectores>>> Get()
        {
            var items = await context.sectores.ToListAsync();
            if (!items.Any())
                return NotFound();
            return items;
        }
    }
}
