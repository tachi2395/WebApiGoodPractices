using Microsoft.AspNetCore.Mvc;

namespace WebApiGoodPracticesSample.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiBaseController : ControllerBase
    {
    }
}
