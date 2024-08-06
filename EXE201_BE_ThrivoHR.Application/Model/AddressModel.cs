namespace EXE201_BE_ThrivoHR.Application.Model;

public class AddressModel
{

    public required string AddressLine { get; set; }
    public required string Ward { get; set; }
    public required string District { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }

}

