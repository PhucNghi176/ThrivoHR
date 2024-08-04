namespace EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract
{
    public interface ISoftDelete
    {
        DateTimeOffset? DeletedOn { get; set; }
        string? DeletedBy { get; set; }
    }
}
