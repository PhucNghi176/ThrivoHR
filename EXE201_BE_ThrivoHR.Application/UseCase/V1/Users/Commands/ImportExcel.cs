﻿using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using IronXL;
using Microsoft.AspNetCore.Http;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

public record ImportExcel(IFormFile File) : ICommand<string>;

internal sealed class ImortExcelHandler(IEmployeeRepository _employeeRepository, IAddressRepository _addressRepository, IEmployeeContractRepository _employeeContractRepository, IDepartmentRepository _departmentRepository) : ICommandHandler<ImportExcel, string>
{

    public async Task<Result<string>> Handle(ImportExcel request, CancellationToken cancellationToken)
    {
        WorkBook workBook = WorkBook.Load(request.File.OpenReadStream());
        WorkSheet workSheet = workBook.WorkSheets[0];
        int row = 2;
        while (!string.IsNullOrEmpty(workSheet[$"A{row}"].StringValue))
        {
            int DId = workSheet[$"S{row}"].IntValue;
            var Manager = await _departmentRepository.FindAsync(x => x.Id == DId, cancellationToken);
            int PId = workSheet[$"U{row}"].IntValue;
            var StartDate = DateOnly.FromDateTime((DateTime)workSheet[$"W{row}"].DateTimeValue!);
            DateOnly? EndDate = DateOnly.FromDateTime((DateTime)workSheet[$"X{row}"].DateTimeValue);
            bool? IsExpiry = EndDate == null;
            var address = new Address
            {
                AddressLine = workSheet[$"K{row}"].StringValue,
                City = workSheet[$"L{row}"].StringValue,
                District = workSheet[$"M{row}"].StringValue,
                Ward = workSheet[$"N{row}"].StringValue,
                Country = "Việt Nam"
            };
            await _addressRepository.AddAsync(address);

            var Employee = new AppUser
            {
                LastName = workSheet[$"B{row}"].StringValue,
                FirstName = workSheet[$"C{row}"].StringValue,
                FullName = workSheet[$"D{row}"].StringValue,
                Email = workSheet[$"E{row}"].StringValue,
                PhoneNumber = workSheet[$"F{row}"].StringValue,
                DateOfBirth = DateOnly.FromDateTime((DateTime)workSheet[$"G{row}"].DateTimeValue!),
                IdentityNumber = workSheet[$"H{row}"].StringValue,
                TaxCode = workSheet[$"I{row}"].StringValue,
                BankAccount = workSheet[$"J{row}"].StringValue,
                Address = address,
                Sex = workSheet[$"O{row}"].BoolValue,
                Ethnicity = workSheet[$"P{row}"].StringValue,
                Religion = workSheet[$"Q{row}"].StringValue,
                DepartmentId = DId,
                PositionId = PId,
                ManagerId = Manager?.HeadOfDepartmentId
            };
            await _employeeRepository.AddAsync(Employee);
            var employeeContract = new EmployeeContract
            {
                EmployeeId = Employee.Id,
                DepartmentId = DId,
                PositionId = PId,
                StartDate = StartDate,
                EndDate = EndDate,
                Salary = workSheet[$"V{row}"].DecimalValue,
                Notes = workSheet[$"Z{row}"].StringValue,
                Duration = workSheet[$"Y{row}"].IntValue,
                IsNoExpiry = IsExpiry.Value

            };
            await _employeeContractRepository.AddAsync(employeeContract);
            row++;
        }
        await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success("Import Success");
    }
}
