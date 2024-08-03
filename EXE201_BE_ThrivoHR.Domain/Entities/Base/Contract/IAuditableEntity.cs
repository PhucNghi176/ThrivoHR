﻿namespace EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;

public interface IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}
