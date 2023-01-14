using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class itemController : ControllerBase
    {

        private readonly DatabaseContext context;

        public itemController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ItemPresupuestario>>> Get()
        {
            var items = await context.ItemsPresup.ToListAsync();
            if (!items.Any())
                return NotFound();
            return items;
        }
    }
}
