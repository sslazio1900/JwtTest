using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AuthService.Interfaces;

using ErrorOr;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services;

public class MyAuthenticationService : IAuthService
{
    private readonly JwtConfig _jwtOptions;

    public MyAuthenticationService(IOptions<JwtConfig> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }


    public ErrorOr<string> AuthenticateUser(string user, string password)
    {
        if (user.Equals("Pippo", StringComparison.Ordinal) && password.Equals("Pluto", StringComparison.Ordinal))
        {
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user),
                new Claim(JwtRegisteredClaimNames.FamilyName, password)
            };
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireInMins),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        return Error.Conflict();
    }
}