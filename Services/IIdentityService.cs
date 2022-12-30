using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.ResponseModels;
using Microsoft.AspNetCore.Authentication;

namespace MatrizPlanificacion.Services
{
    public interface IIdentityService
    {
        Task<string> RegisterAsync(string userId, string rol, string planta);
        Task<AuthenticationResult> LoginAsync(string userName, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refresToken);
        Task ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
