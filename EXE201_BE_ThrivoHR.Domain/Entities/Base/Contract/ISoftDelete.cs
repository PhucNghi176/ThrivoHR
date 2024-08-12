namespace EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract
{
    public interface ISoftDelete
    {
        DateTime? DeletedOn { get; set; }
        string? DeletedBy { get; set; }

        bool IsDeleted { get; set; }
    }
}
