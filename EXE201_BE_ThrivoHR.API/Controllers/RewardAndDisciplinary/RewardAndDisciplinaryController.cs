using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Queries;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Commands;
namespace EXE201_BE_ThrivoHR.API.Controllers.RewardAndDisciplinary;


public class RewardAndDisciplinaryController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FilterRewardAndDisciplinary filter)
    {

        return Ok(await _sender.Send(filter));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Create command)
    {
        return Ok(await _sender.Send(command));
    }
}
