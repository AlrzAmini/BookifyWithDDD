using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookify.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
