using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniLife.Server.Managers;
using System;
using System.Threading.Tasks;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalAuthController : ControllerBase
    {
        private readonly IExternalAuthManager _externalAuthManager;

        public ExternalAuthController(IExternalAuthManager externalAuthManager)
        {
            _externalAuthManager = externalAuthManager;
        }

        [HttpGet("challenge/{provider}")]
        [AllowAnonymous]
        public async Task<IActionResult> Challenge(string provider)
        {
            try
            {
                var (authProps, schemaName) = await _externalAuthManager.Challenge(Url.RouteUrl("ExternalSignIn"), provider);

                return Challenge(authProps, schemaName);
            }
            catch (ArgumentNullException ex)
            {
                return Redirect(ex.Message);
            }
        }


        [HttpGet("ExternalSignIn", Name = "ExternalSignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalSignIn()
        => Redirect(await _externalAuthManager.ExternalSignIn(HttpContext));

    }
}
