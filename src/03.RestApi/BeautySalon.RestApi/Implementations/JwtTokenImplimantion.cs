using BeautySalon.Common.Interfaces;
using BeautySalon.Services.JWTTokenService;
using BeautySalon.Services.Users.Contracts.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeautySalon.RestApi.Implementations;

public class JwtTokenImplementation : IJwtTokenService
{
    private readonly IJwtSettingService _jwtSettings;

    public JwtTokenImplementation(IJwtSettingService jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public async Task<string> GenerateToken(GetUserForLoginDto dto)
    {
        var claims = new List<Claim>()
        {
            new Claim (JwtRegisteredClaimNames.Sub,dto.Id!),
            new Claim(JwtRegisteredClaimNames.UniqueName,dto.UserName!),
        };

        claims.AddRange(dto.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtSetting.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.JwtSetting.Issuer,
            audience: _jwtSettings.JwtSetting.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.JwtSetting.AccessTokenExpirationMinutes),
            signingCredentials: creds
        );
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        await Task.CompletedTask;
        return tokenValue;

    }
}
