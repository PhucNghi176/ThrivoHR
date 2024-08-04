using EXE201_BE_ThrivoHR.Application.Common.Interfaces;
using EXE201_BE_ThrivoHR.Application.Common.Models;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Domain.Repositories;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

public record GenerateEmployeesCommand : ICommand
{
}

internal sealed class GenerateEmployeesCommandHandler : ICommandHandler<GenerateEmployeesCommand>
{

    private readonly IUserRepository _userRepository;

    public GenerateEmployeesCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(GenerateEmployeesCommand request, CancellationToken cancellationToken)
    {
        var firstNames = new List<string> { "John", "Jane", "Alex", "Emily", "Chris", "Anna", "Michael", "Laura", "David", "Sara" };
        var lastNames = new List<string> { "Smith", "Johnson", "Brown", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin" };

        var random = new Random();

        for (int i = 0; i < 10000; i++)
        {
            var firstName = firstNames[random.Next(firstNames.Count)];
            var lastName = lastNames[random.Next(lastNames.Count)];
            var fullName = $"{firstName} {lastName}";
            var identityNumber = random.Next(100000000, 999999999).ToString();
            var dob = new DateTime(random.Next(1950, 2000), random.Next(1, 12), random.Next(1, 28));
            var phoneNumber = random.Next(1000000000, 1999999999).ToString();
            var taxCode = random.Next(1000000, 99999999).ToString();
            var addressId = random.Next(1, 21);
            var departmentId = random.Next(1, 6);
            var positionId = random.Next(1, 6);
            var bankAccount = random.Next(1000000, 99999999).ToString();


            _userRepository.Add(new AppUser
            {
                Id = Guid.NewGuid().ToString("N"),
                UserName = $"user{i}",
                NormalizedUserName = $"USER{i}",
                Email = $"user{i}@example.com",
                BankAccount = bankAccount,
                IdentityNumber = identityNumber,
                PhoneNumber = phoneNumber,
                TaxCode = taxCode,
                FirstName = firstName,
                LastName = lastName,
                FullName = fullName,
                EmploeeyCode = i.ToString(),
                DayOfBirth = dob,
                AddressId = addressId,
                DepartmentId = departmentId,
                PositionId = positionId
            });


        }
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
