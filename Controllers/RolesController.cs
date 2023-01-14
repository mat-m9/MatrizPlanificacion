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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RolesController> _logger;
        private readonly UserManager<User> _userManager;


        public RolesController(DatabaseContext context, RoleManager<IdentityRole> roleManager, ILogger<RolesController> log, UserManager<User> userManager)
        {
            this.context = context;
            this._roleManager = roleManager;
            this._logger = log;
            this._userManager = userManager;
        }

        //[Authorize(Roles = "SUPERADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult<List<string>>> Get()
        {
            List<string> rolesString = new List<string>();
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var role in roles)
            {
                rolesString.Add(role.Name);
            }
            if (!roles.Any())
                return NotFound();
            return rolesString;
        }
        
        [HttpPost]
        public async Task<ActionResult<String>> Post(CreateRol createRol)
        {

            var newRol = new IdentityRole
            {
                Name = createRol.RolName.ToUpper(),
                NormalizedName = createRol.RolName

            };
            var createdRol = await _roleManager.CreateAsync(newRol);
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

        [HttpPost(template: ApiRoutes.Rol.Grant)]
        public async Task<IActionResult>GrantRole([FromBody] RolRequest rolRequest )
        {
            var user = await _userManager.FindByNameAsync(rolRequest.UserName);
            var grantResult = await _userManager.AddToRoleAsync(user, rolRequest.Rol);
            
            if(grantResult.Succeeded)
                return Ok();

            return BadRequest();
        }


        [HttpDelete(template: ApiRoutes.Rol.Revoke)]
        public async Task<IActionResult> RevokeRole([FromBody] RolRequest rolRequest)
        {
            var user = await _userManager.FindByNameAsync(rolRequest.UserName);
            var grantResult = await _userManager.RemoveFromRoleAsync(user, rolRequest.Rol);

            if (grantResult.Succeeded)
                return Ok();

            return BadRequest();
        }


        private async Task<bool> Existe(string id)
        {
            return await context.Roles.AnyAsync(p => p.Id == id);
        }
    }
}
