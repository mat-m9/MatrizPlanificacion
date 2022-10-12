﻿using MatrizPlanificacion.Modelos;
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

        public IdentityService(UserManager<User> userName, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters, DatabaseContext databaseContext)
        {
            _userManager = userName;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            this.databaseContext = databaseContext;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password, PlantaUnidadArea planta)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);  
            if(existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Ya existe un usuario con este correo" }
                };
            }
            var newUser = new User
            {
                Email = email,
                UserName = email,
                AreaId = planta.PlantaUnidadAreaId

            };
            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }
            return await GenerateAthenticationResultForUserAsync(newUser);
        }

        public async  Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
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
            return await GenerateAthenticationResultForUserAsync(user);
        }



        private async Task<AuthenticationResult> GenerateAthenticationResultForUserAsync(User newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims: new[]
                {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value: newUser.Email),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    //new Claim(type: JwtRegisteredClaimNames.Jti, value: newUser.Email),
                    //new Claim(type: "id", value: newUser.Id)
                }),
                Expires = DateTime.Now.AddMinutes(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshToken
            {
                Token = tokenHandler.WriteToken(token),
                JwtId = token.Id,
                UserId = newUser.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.Now.AddMinutes(_jwtSettings.TokenLifeTime)
            };

            await databaseContext.RefreshTokens.AddAsync(refreshToken);
            await databaseContext.SaveChangesAsync();
            
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
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

            var storedRefreshToken = await databaseContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

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

       
    }
}
