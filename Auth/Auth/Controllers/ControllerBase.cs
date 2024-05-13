using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("account/[controller]")]
    public class ControllerBase : Microsoft.AspNetCore.Mvc.Controller
    {
        public ControllerBase() { }
    }
}
