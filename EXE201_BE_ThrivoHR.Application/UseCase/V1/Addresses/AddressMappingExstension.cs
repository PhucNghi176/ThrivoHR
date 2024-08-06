using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Addresses;

public static class AddressMappingExstension
{
    public static AddressDto MapToAddressDto(this Address address, IMapper mapper)
    {
        return mapper.Map<AddressDto>(address);
    }

    public static List<AddressDto> MapToAddressListDto(this IEnumerable<Address> addresses, IMapper mapper)
    {
        return addresses.Select(address => address.MapToAddressDto(mapper)).ToList();
    }
}
