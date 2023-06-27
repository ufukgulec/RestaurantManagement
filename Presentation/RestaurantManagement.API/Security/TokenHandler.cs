using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantManagement.API.Security
{
    public static class TokenHandler
    {
        public static Token CreateToken(IConfiguration configuration, Employee employee)
        {
            Token token = new Token();

            IdentityOptions _options = new IdentityOptions();

            var claims = new List<Claim>()
            {
                new Claim(_options.ClaimsIdentity.UserIdClaimType, employee.Id.ToString()),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, employee.Fullname.ToString()),
                new Claim(_options.ClaimsIdentity.RoleClaimType, employee.Role.Name.ToString()),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["Token:Expiration"]));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration["Token:Issuer"],
                audience: configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials,
                claims: claims
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

            byte[] numbers = new byte[32];
            using RandomNumberGenerator numberGenerator = RandomNumberGenerator.Create();
            numberGenerator.GetBytes(numbers);

            token.RefreshToken = Convert.ToBase64String(numbers);

            return token;
        }

        public static bool ValidateToken(IConfiguration configuration, string token)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidAudience = configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
