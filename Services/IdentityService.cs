using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.Options;
using MatrizPlanificacion.ResponseModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MatrizPlanificacion.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DatabaseContext databaseContext;
        private readonly PasswordGeneratorService passwordGenerator;

        public IdentityService(UserManager<User> userName, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters, DatabaseContext databaseContext, PasswordGeneratorService passwordGeneratorService)
        {
            _userManager = userName;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            this.databaseContext = databaseContext;
            this.passwordGenerator = passwordGeneratorService;
        }

        public async Task<string> RegisterAsync(string userName, string rol, string plantaId)
        {
            var existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                return null;
            }
            var newUser = new User
            {
                UserName = userName,
                AreaId = plantaId,

            };

            string password = await passwordGenerator.GeneratePassword();

            var createdUser = await _userManager.CreateAsync(newUser, password);
           
            if (!createdUser.Succeeded)
            {
                return null;
            }
            var getUser = await _userManager.FindByNameAsync(userName);
            var setRole = await _userManager.AddToRoleAsync(getUser, rol.ToUpper());

            return password;
        }

        public async  Task<AuthenticationResult> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "El usuario no existe" }
                };
            }
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "El usuario o la contraseña son incorrectos" }
                };
            }
            var getRol = await _userManager.GetRolesAsync(user);
            return await GenerateAthenticationResultForUserAsync(user);
        }



        private async Task<AuthenticationResult> GenerateAthenticationResultForUserAsync(User newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var roles = await _userManager.GetRolesAsync(newUser);
            var claims = new List<Claim>();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims: new[]
                {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value: newUser.UserName),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim(type: "id", value: newUser.Id),//para identificar al usuario del token validado ARREGLAR
                    new Claim(type: "idArea", value: newUser.AreaId),
                    new Claim(type: "needChange", value: newUser.needChange.ToString())

                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };
            foreach (var role in roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(type: ClaimTypes.Role, value:role));
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = newUser.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifeTime)
            };

            await databaseContext.RefreshTokens.AddAsync(refreshToken);
            await databaseContext.SaveChangesAsync();
            
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.JwtId
            };
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);
            if(validatedToken == null)
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new AuthenticationResult { Errors = new[] { "This token has't expired yet" } };

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            
            var storedRefreshToken = await databaseContext.RefreshTokens.Where(rT => rT.JwtId.Equals(refreshToken)).FirstOrDefaultAsync(); //.SingleOrDefaultAsync(x => x.JwtId == refreshToken);

            if (storedRefreshToken == null)
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };

            if(DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };

            if (storedRefreshToken.Ivalidated)
                return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };

            if (storedRefreshToken.Used)
                return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };

            if (storedRefreshToken.JwtId != jti)
                return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JWT" } };

            storedRefreshToken.Used = true;
            databaseContext.RefreshTokens.Update(storedRefreshToken);
            await databaseContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            //var user = await _userManager.FindByNameAsync(validatedToken.Claims.Single(x => x.Type == "Sub").Value);
            return await GenerateAthenticationResultForUserAsync(user);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task ChangePassword(string userId, string oldPassword, string newPassWord)
        {
            User tempUser = await _userManager.FindByIdAsync(userId);
            tempUser.needChange = false;
            databaseContext.Users.Update(tempUser);
            await databaseContext.SaveChangesAsync();
            await _userManager.ChangePasswordAsync(tempUser, oldPassword, newPassWord);
        }

    }
}
