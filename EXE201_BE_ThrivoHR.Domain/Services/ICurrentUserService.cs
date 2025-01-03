﻿namespace EXE201_BE_ThrivoHR.Domain.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserName { get; }
    Task<bool> IsInRoleAsync(string role);
    Task<bool> AuthorizeAsync(string policy);
}
