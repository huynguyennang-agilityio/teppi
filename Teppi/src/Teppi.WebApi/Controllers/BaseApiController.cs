using Microsoft.AspNetCore.Mvc;

namespace Teppi.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController : ControllerBase
{
}