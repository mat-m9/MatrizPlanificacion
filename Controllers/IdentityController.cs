using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.ResponseModels;
using MatrizPlanificacion.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace MatrizPlanificacion.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(template: ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage))
                });
            }

            var authResponse = await _identityService.RegisterAsync(request.userName, request.email, request.password, request.rol, request.planta);
            

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                }); ;
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                 RefreshedToken = authResponse.RefreshToken
            });
        }

        [HttpPost(template: ApiRoutes.Identity.Login)]
        public async Task<IActionResult>Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.userName, request.password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                }); ;
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshedToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshedToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                }); ;
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshedToken = authResponse.RefreshToken
            });
        }
    }

}
