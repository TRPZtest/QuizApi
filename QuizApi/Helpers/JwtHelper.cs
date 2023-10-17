using Microsoft.IdentityModel.Tokens;
using QuizApi.Configuration;
using QuizApi.Data.Db.Enteties;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuizApi.Helpers
{
    public static class JwtHelper
    {
        public static string GetJwt(User user)
        {
            var claims = new Claim[] { new Claim(ClaimTypes.Name, user.Id.ToString()) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60 * 24)), // время действия 2 минуты
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
