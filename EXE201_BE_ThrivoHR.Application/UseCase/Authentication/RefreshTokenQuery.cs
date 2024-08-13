using EXE201_BE_ThrivoHR.Application.Model;

namespace EXE201_BE_ThrivoHR.Application.UseCase.Authentication;

public record RefreshTokenQuery(TokenModel TokenModel) : IQuery<TokenModel>;
internal sealed class RefreshTokenQueryHandler(ITokenService tokenService) : IQueryHandler<RefreshTokenQuery, TokenModel>
{
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<TokenModel>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var token = await _tokenService.RefreshTokenAsync(request.TokenModel);
        return Result<string>.Success(token);
    }
}
