﻿using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ResignForms;

public class ResginFormDto : IMapFrom<ResignForm>
{
    public new string Id { get; set; }
    public string? EmployeeId { get; set; }
    public DateTime DateTime { get; set; }
    public string? Reason { get; set; }
    public string? ApproverName { get; set; }
    public string? Status { get; set; }

    public static ResginFormDto Create(ResignForm entity)
    {
        return new ResginFormDto
        {
            Id = entity.Id,
            EmployeeId = entity.EmployeeId,
            DateTime = entity.DateTime,
            Reason = entity.Reason,
            ApproverName = entity.Approver?.FullName,
            Status = entity.Status.ToString(),
        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ResignForm, ResginFormDto>().ReverseMap();
    }
}
