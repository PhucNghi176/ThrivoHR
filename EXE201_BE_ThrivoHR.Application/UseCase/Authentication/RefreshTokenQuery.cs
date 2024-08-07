using EXE201_BE_ThrivoHR.Application.Model;
using System.Security.Claims;

namespace EXE201_BE_ThrivoHR.Application.UseCase.Authentication;

public record RefreshTokenQuery(TokenModel TokenModel) : IQuery<TokenModel>;
internal sealed class RefreshTokenQueryHandler : IQueryHandler<RefreshTokenQuery, TokenModel>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public RefreshTokenQueryHandler(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<Result<TokenModel>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var token = await _tokenService.RefreshTokenAsync(request.TokenModel);
        return Result<string>.Success(token);
    }
}
