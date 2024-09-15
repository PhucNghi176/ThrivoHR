using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Interfaces;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EXE201_BE_ThrivoHR.API.Services;

public class TokenService(IConfiguration configuration, IEmployeeRepository userRepository) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IEmployeeRepository _userRepository = userRepository;

    public async Task<TokenModel> GenerateTokenAsync(string EmployeeCode, string RoleName)
    {
        string token = GenerateJwt(EmployeeCode, RoleName);
        var RefreshToken = GenerateRefreshToken();
        var tokenModel = new TokenModel(token, RefreshToken);
        return await Task.FromResult(tokenModel);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    public async Task<TokenModel> RefreshTokenAsync(TokenModel tokenModel)
    {
        var userPrincipal = GetPrincipalFromExpiredToken(tokenModel.Token);
        var EmployeeCode = EmployeesMethod.ConvertEmployeeCodeToId(userPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userRepository.FindAsync(x => x.EmployeeId == EmployeeCode);
        if (user is null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new Employee.InvalidTokenException();
        }
        return await GenerateTokenAsync(user.EmployeeCode, userPrincipal.FindFirst(ClaimTypes.Role)!.Value);
    }
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var _jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThrivoHR API 123abc456 anh iu em....")),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = _jwtSettings["validAudience"],
            ValidIssuer = _jwtSettings["validIssuer"],
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new ForbiddenAccessException("Invalid token");
        }

        return principal;
    }
    private string GenerateJwt(string EmployeeCode, string RoleName) =>
    GenerateEncryptedToken(GetSigningCredentials(), GetClaims(EmployeeCode, RoleName));
    private static SigningCredentials GetSigningCredentials()
    {
        var secret = Encoding.UTF8.GetBytes("ThrivoHR API 123abc456 anh iu em....");
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }
    private static List<Claim> GetClaims(string EmployeeCode, string RoleName)
    =>
    [       
        //  new Claim(ClaimTypes.Email, appUser.Email),
        new Claim(ClaimTypes.NameIdentifier, EmployeeCode),
        new Claim(ClaimTypes.Role, RoleName),
        new Claim("EmployeeId", EmployeesMethod.ConvertEmployeeCodeToId(EmployeeCode).ToString()),
       
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


