using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("Modelos/User")]
    public class UserController : ControllerBase
    {

        private readonly DatabaseContext context;

        public UserController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<User>>> Get()
        {
            return await context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(User user)
        {
            context.Add(user);
            await context.SaveChangesAsync();
            return user.Id;
        }

        [HttpPut("{Id:Guid}")]
        public async Task<ActionResult> Put(string id, User user)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Update(user);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{{Id:Guid}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Remove(new User() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Users.AnyAsync(p => p.Id == id);
        }
    }
}
