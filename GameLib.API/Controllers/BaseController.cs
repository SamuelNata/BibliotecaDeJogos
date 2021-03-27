using System.Linq;
using System.Security.Claims;
using GameLib.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API.Controllers
{
    /// <summary>
    /// BaseController require a autenticated user to work properly
    /// </summary>
    [Authorize]
    [TypeFilter(typeof(ExceptionHandlerFilter))]
    public abstract class UserScopedController : ControllerBase
    {
        protected string CurrentUserId => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

    }
}