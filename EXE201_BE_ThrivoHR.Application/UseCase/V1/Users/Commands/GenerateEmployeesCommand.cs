using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

public record GenerateEmployeesCommand : ICommand
{
}

internal sealed class GenerateEmployeesCommandHandler(IEmployeeRepository userRepository) : ICommandHandler<GenerateEmployeesCommand>
{
    public async Task<Result> Handle(GenerateEmployeesCommand request, CancellationToken cancellationToken)
    {
        var firstNames = new List<string> { "John", "Jane", "Alex", "Emily", "Chris", "Anna", "Michael", "Laura", "David", "Sara" };
        var lastNames = new List<string> { "Smith", "Johnson", "Brown", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin" };

        var random = new Random();

        for (int i = 0; i < 1000; i++)
        {
            var firstName = firstNames[random.Next(firstNames.Count)];
            var lastName = lastNames[random.Next(lastNames.Count)];
            var fullName = $"{firstName} {lastName}";
            var identityNumber = random.Next(0, 999999999).ToString();

            var dob = new DateOnly(random.Next(1950, 2000), random.Next(1, 12), random.Next(1, 28));
            var phoneNumber = Guid.NewGuid().ToString()[..10];
            var taxCode = Guid.NewGuid().ToString()[..10];
            var addressId = random.Next(1, 21);
            var departmentId = random.Next(1, 6);
            var positionId = random.Next(1, 6);
            var bankAccount = Guid.NewGuid().ToString()[..10];


            await userRepository.AddAsync(new AppUser
            {
                Id = Guid.NewGuid().ToString("N"),
                UserName = $"user{i}",
                NormalizedUserName = $"USER{i}",
                Email = $"user{i}@example.com",
                BankAccount = bankAccount,
                IdentityNumber = identityNumber,
                Religion = "1",
                Ethnicity = "1",
                Sex = true,
                PhoneNumber = phoneNumber,
                TaxCode = taxCode,
                FirstName = firstName,
                LastName = lastName,
                FullName = fullName,

                DateOfBirth = dob,
                AddressId = addressId,
                DepartmentId = departmentId,
                PositionId = positionId
            });


        }
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
