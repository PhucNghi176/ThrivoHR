using EXE201_BE_ThrivoHR.Application.UseCase.V1.Departments.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.Department;


public class DepartmentController : BaseController
{
    public DepartmentController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _sender.Send(new Get());
        return Ok(result);
    }
}

