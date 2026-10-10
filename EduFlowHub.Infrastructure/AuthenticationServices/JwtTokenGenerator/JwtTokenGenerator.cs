using System.IdentityModel.Tokens.Jwt;
using EduFlowHub.Domain.Enums;

using System.Security.Claims;
using System.Text;
using EduFlowHub.Application.Abstractions.Services.Authentication;
using EduFlowHub.Infrastructure.AuthenticationServices.JwtTokenGenerator.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EduFlowHub.Infrastructure.AuthenticationServices.JwtTokenGenerator;

public class JwtTokenGenerator
    : IJwtTokenGenerator
{
    private readonly JwtAuthenticationOptions _config;

    public JwtTokenGenerator(IOptions<JwtAuthenticationOptions> options)
    {
        _config = options.Value;
    }
    
    public string GenerateJwtToken(
        Guid userId, 
        UserRole role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString())
        };

       var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _config.SecretKey
            ));

        var jwt = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_config.ExpirationMinutes)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}