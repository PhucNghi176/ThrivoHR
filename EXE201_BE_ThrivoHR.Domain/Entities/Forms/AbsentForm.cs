using EXE201_BE_ThrivoHR.Domain.Entities.Base;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Forms;

public class AbsentForm : BaseForm
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }

}
