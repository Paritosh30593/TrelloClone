using Microsoft.AspNetCore.Mvc;

namespace TC.WebAPI.Controllers.Base
{
    /// <summary>
    /// BaseController serves as a base class for all API controllers in the application. It provides common functionality and routing conventions for derived controllers.
    /// The [Route] attribute defines a routing pattern that includes the controller name and action name, allowing for consistent and organized API endpoints.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}