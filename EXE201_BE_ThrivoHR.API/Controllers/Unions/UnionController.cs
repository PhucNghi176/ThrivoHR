
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Unions.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Unions.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.Unions;


public class UnionController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateUnion createUnion)
    {
        return Ok(await _sender.Send(createUnion));
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterUnion filterUnion)
    {
        return Ok(await _sender.Send(filterUnion));
    }
    [HttpPut]
    public async Task<IActionResult> Update(UpdateUnion updateUnion)
    {
        return Ok(await _sender.Send(updateUnion));
    }
}
