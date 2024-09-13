using EXE201_BE_ThrivoHR.Domain.Entities.Base;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Forms;

public class ResignForm : BaseForm
{
    public DateOnly LastWorkingDate { get; set; }
}
