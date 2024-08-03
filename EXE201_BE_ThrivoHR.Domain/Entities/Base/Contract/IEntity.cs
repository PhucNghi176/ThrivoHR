namespace EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;

public interface IEntity<out Tid>
{
    Tid ID { get; }
}
