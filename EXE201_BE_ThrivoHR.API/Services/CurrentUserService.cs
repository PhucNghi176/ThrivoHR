﻿using EXE201_BE_ThrivoHR.Domain.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EXE201_BE_ThrivoHR.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal? _claimsPrincipal;
    private readonly IAuthorizationService _authorizationService;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
    {
        _claimsPrincipal = httpContextAccessor?.HttpContext?.User;
        _authorizationService = authorizationService;
    }

    public string? UserId => _claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    public string? UserName => _claimsPrincipal?.FindFirst(JwtClaimTypes.Name)?.Value;

    public async Task<bool> AuthorizeAsync(string policy)
    {
        if (_claimsPrincipal == null) return false;
        return (await _authorizationService.AuthorizeAsync(_claimsPrincipal, policy)).Succeeded;
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        return await Task.FromResult(_claimsPrincipal?.IsInRole(role) ?? false);
    }

}
