using EXE201_BE_ThrivoHR.Application.UseCase.V1.Salaries.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Salaries.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.Salaries;


public class SalaryController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Generate()
    {
        return Ok(await _sender.Send(new GeneratePayRoll()));
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Filter query)
    {
        return Ok(await _sender.Send(query));
    }
}
