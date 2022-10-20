using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.ResponseModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
//
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Xamarin.Essentials;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    [Authorize]
    public class RolesController : ControllerBase
    {

        private DatabaseContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<RolesController> _logger;


        public RolesController(DatabaseContext context, RoleManager<IdentityRole> _roleManager, ILogger<RolesController> log)
        {
            this.context = context;
            this.roleManager = _roleManager;
            this._logger = log;
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<ICollection<IdentityRole>>> Get()
        {
            var roles = await roleManager.Roles.ToListAsync();
            if (!roles.Any())
                return NotFound();
            return roles;
        }

        [HttpPost]
        public async Task<ActionResult<String>> Post(CreateRol createRol)
        {

            var newRol = new IdentityRole
            {
                Name = createRol.RolName,
                NormalizedName = createRol.RolName

            };
            var createdRol = await roleManager.CreateAsync(newRol);
            if (createdRol.Succeeded)
            {
                return Ok(createdRol);
            }
            return BadRequest();
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
