using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.ResponseModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Xamarin.Essentials;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AspNetRolesController : ControllerBase
    {

        private DatabaseContext context;
        private readonly AspNetRoleManager<Rol> roleManager;


        public AspNetRolesController(DatabaseContext context, AspNetRoleManager<Rol> _roleManager)
        {
            this.context = context;
            this.roleManager = _roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Rol>>> Get()
        {
            var roles = await context.Roles.ToListAsync();
            if (!roles.Any())
                return NotFound();
            return roles;

        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Rol>>> GetRol(string id)
        {
            var rol = await context.Roles.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
            if (rol == null)
                return NotFound();
            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<String>> Post(CreateRol creteRol)
        {

            if (ModelState.IsValid)
            {
                var newRol = new Rol
                {
                    Name = creteRol.RolName

                };

                var createdRol = await roleManager.CreateAsync(newRol);
                if (!createdRol.Succeeded)
                {
                    return Ok(createdRol);
                }
            }
            return NotFound();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var rol = await context.Roles.FindAsync(id);
            context.Roles.Remove(rol);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Roles.AnyAsync(p => p.Id == id);
        }
    }
}
