using EXE201_BE_ThrivoHR.Application.Common.Models;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EXE201_BE_ThrivoHR.API.Services;

public class JwtService
{
    public Result<string> CreateToken(EmployeeDto employeeDto)
    {
        List<Claim> claims = new()
            {
                new(JwtRegisteredClaimNames.Sub, employeeDto.EmployeeCode),
               // new(ClaimTypes.Role, employeeDto..ToString()),
                new("Position",employeeDto.Position.Id.ToString()),
                new("Department",employeeDto.Department.Id.ToString()),
            };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("ThrivoHR @PI 123abc456 anh iu em"));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
             issuer: "https://deercoffeesystem.azurewebsites.net/",
             audience: "api",
            claims: claims,
            expires: DateTime.Now.AddYears(1),
            signingCredentials: creds);

        var AccessToken = new JwtSecurityTokenHandler().WriteToken(token);


        return AccessToken;
    }
}
