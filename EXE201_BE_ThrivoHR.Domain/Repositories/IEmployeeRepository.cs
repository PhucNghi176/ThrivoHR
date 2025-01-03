﻿using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
namespace EXE201_BE_ThrivoHR.Domain.Repositories;

public interface IEmployeeRepository : IEFRepository<AppUser, AppUser>
{
    Task<bool> VerifyPasswordAsync(string password, string passwordHash);
    Task<AppUser> FindByEmployeeCode(string EmployeeCode, CancellationToken cancellationToken=default);

}
