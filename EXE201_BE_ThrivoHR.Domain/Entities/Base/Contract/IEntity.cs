using System.ComponentModel.DataAnnotations;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;

public interface IEntity<out Tid>
{
    [Key]
    Tid Id { get; }
}
