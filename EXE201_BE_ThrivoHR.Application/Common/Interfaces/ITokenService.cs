using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Application.Common.Interfaces;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(string EmployeeCode,string RoleName);
    Task<TokenModel> RefreshTokenAsync(string token, string refreshToken);
}
