using EXE201_BE_ThrivoHR.Application.Common.Interfaces;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EXE201_BE_ThrivoHR.API.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    

    public async Task<string> GenerateTokenAsync(string EmployeeCode, string RoleName)
    {
        string token = GenerateJwt(EmployeeCode, RoleName);
        return await Task.FromResult(token);
    }

    public Task<TokenModel> RefreshTokenAsync(string token, string refreshToken)
    {
        throw new NotImplementedException();
    }

    private string GenerateJwt(string EmployeeCode, string RoleName) =>
    GenerateEncryptedToken(GetSigningCredentials(), GetClaims(EmployeeCode, RoleName));
    private static SigningCredentials GetSigningCredentials()
    {
        var secret = Encoding.UTF8.GetBytes("ThrivoHR API 123abc456 anh iu em...");
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }
    private static List<Claim> GetClaims(string EmployeeCode, string RoleName)
    =>
    [       
        //  new Claim(ClaimTypes.Email, appUser.Email),
        new Claim(ClaimTypes.NameIdentifier, EmployeeCode),
        new Claim(ClaimTypes.Role, RoleName),
    ];

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var _jwtSettings = _configuration.GetSection("JwtSettings");
        var token = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings["expires"])),
           signingCredentials: signingCredentials,
           issuer: _jwtSettings["validIssuer"],
           audience: _jwtSettings["validAudience"]
           );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}


