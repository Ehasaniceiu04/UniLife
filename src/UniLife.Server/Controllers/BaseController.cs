using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UniLife.Server.Controllers
{
    public class BaseController : ControllerBase
    {

        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public BaseController(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;

        //    var userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject).Value);
        //}

        public Guid GuidUserId
        {
            get
            {
                if (User != null) /* The user object is found to be null here. */
                {
                    return new Guid(User.GetSubjectId());
                }

                return new Guid();
            }
        }
    }
}
