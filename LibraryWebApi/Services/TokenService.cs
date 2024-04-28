using LibraryWebApi.Interfaces;
using LibraryWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryWebApi.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<Librarian> _userManager;

        public TokenService(IConfiguration config, UserManager<Librarian> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _userManager = userManager;
        }

        public async Task<(string token, string refreshToken)> CreateToken(Librarian user)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName , user.UserName),
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
            };

            var refreshClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };
            
            var RefreshTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(refreshClaims),
                Expires = DateTime.Now.AddMonths(1),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = tokenHandler.CreateToken(RefreshTokenDescriptor);

            return (token: tokenHandler.WriteToken(token), refreshToken: tokenHandler.WriteToken(refreshToken));
        }

        public string? ValidateRefreshToken(string refreshToken)
        {
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ValidateIssuer = true,
                    ValidIssuer = _config["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["JWT:Audience"],
                    ValidateLifetime = true, // Ensure the token is not expired
                    ClockSkew = TimeSpan.Zero // Set to zero to compensate for server/client time differences
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out validatedToken);

                // Extract claims from the validated token
                var userId = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                // Perform additional validation or checks if needed (e.g., verify user existence)

                return userId; // Return the user ID
            }
            catch (Exception ex)
            {
                // Token validation failed
                // Handle exception or return null
                return null;
            }
        }

    }
}
