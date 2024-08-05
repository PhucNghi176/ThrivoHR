using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Addresses;

public class AddressDto : IMapFrom<Address>
{
    public int Id { get; set; }
    public string AddressLine { get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string FullAddress { get; set; }

    public static AddressDto Create(Address address)
    {
        return new AddressDto
        {
            Id = address.Id,
            AddressLine = address.AddressLine,
            Ward = address.Ward,
            District = address.District,
            City = address.City,
            Country = address.Country,
            FullAddress = address.FullAddress
        };
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Address, AddressDto>().ReverseMap();
    }
}
