namespace EXE201_BE_ThrivoHR.API.Controllers
{
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController(ISender sender) : ControllerBase
    {
        protected readonly ISender _sender = sender;
    }
}
