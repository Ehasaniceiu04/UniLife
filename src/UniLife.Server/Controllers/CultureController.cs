using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;

namespace UniLife.Server.Controllers
{
    public class CultureController : Controller
    {
        private readonly ILogger<CultureController> _logger;

        public CultureController(ILogger<CultureController> logger)
        {
            _logger = logger;
        }
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            try
            {
                if (culture != null)
                {
                    HttpContext.Response.Cookies.Append(
                        CookieRequestCultureProvider.DefaultCookieName,
                        CookieRequestCultureProvider.MakeCookieValue(
                            new RequestCulture(culture)));
                }

                _logger.LogError("redirect ahmet");

                return LocalRedirect(redirectUri.Replace("//", "/"));
            }
            catch (System.Exception ex)
            {
                _logger.LogError("culture Error: " + ex.Message + "inner : " + ex.InnerException.Message + "stack:" + ex.StackTrace);
                throw;
            }
            
        }
    }
}
