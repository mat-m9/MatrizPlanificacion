using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/AlertaDSPPP")]
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
            var users = await context.Users.ToListAsync();
            if (!users.Any())
                return NotFound();
            return users;

        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<User>>> GetUser(string id)
        {
            var user = await context.Users.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<String>> Post(User user)
        {
            var created = context.Users.Add(user);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, created.Entity);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, User user)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.Users.Update(user);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var user = await context.Users.FindAsync(id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Users.AnyAsync(p => p.Id == id);
        }
    }
}
