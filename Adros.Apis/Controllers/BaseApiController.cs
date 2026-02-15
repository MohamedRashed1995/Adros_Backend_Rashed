using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
