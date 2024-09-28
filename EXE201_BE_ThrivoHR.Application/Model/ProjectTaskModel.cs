namespace EXE201_BE_ThrivoHR.Application.Model;

public class ProjectTaskModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ProjectId { get; set; }
    public string? AssigneeCode { get; set; }
    public DateTime? DueDate { get; set; }
}
