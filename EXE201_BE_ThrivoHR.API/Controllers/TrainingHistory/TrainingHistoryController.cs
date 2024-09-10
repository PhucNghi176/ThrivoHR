
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.TrainingHistories.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.TrainingHistories.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.TrainingHistory;

public class TrainingHistoryController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<ActionResult<Result>> CreateTrainingHistory([FromBody] CreateTrainingHistory model)
    {
        var result = await _sender.Send(model);
        return result.IsSuccess ? result : BadRequest(result);
    }
    [HttpGet]
    public async Task<ActionResult<Result>> Get([FromQuery] FilterTrainingHistory model)
    {
        var result = await _sender.Send(model);
        return result.IsSuccess ? result : BadRequest(result);
    }
    [HttpDelete]
    public async Task<ActionResult<Result>> Delete([FromQuery] DeleteTrainingHistory model)
    {
        var result = await _sender.Send(model);
        return result.IsSuccess ? result : BadRequest(result);
    }
}

