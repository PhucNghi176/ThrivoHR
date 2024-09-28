using EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Queries;
namespace EXE201_BE_ThrivoHR.API.Controllers.RewardAndDisciplinary;


public class RewardAndDisciplinaryController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterRewardAndDisciplinary filter)
    {

        return Ok(await _sender.Send(filter));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Create command)
    {
        return Ok(await _sender.Send(command));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _sender.Send(new Delete(id)));
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Update command)
    {

        return Ok(await _sender.Send(command));
    }
    [HttpPut("status")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatus command)
    {
        return Ok(await _sender.Send(command));
    }
}
