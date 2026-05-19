using Amazon.API_s.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API_s.Controllers
{
    [Route("error/{statusCode}")] // statusCode is known implicitly from the context; where the endpoint is not found so the status code is 404
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)] // to hide api from swagger, replaces http method
    public class ErrorsController : ControllerBase
    {
        public ActionResult error(int statusCode)
        {
            return NotFound(new ApiResponse(statusCode));
        }
    }
}
