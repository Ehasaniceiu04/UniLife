using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Server.Managers;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrator,Personel")]
    public class YokController : ControllerBase
    {
        private readonly IYokManager _yokManager;

        public YokController(IYokManager yokManager)
        {
            _yokManager = yokManager;
        }

        [HttpPost]
        public async Task<ApiResponse> Post()
           => ModelState.IsValid ?
               await _yokManager.AskerlikErtelemeTalepGonder() :
               new ApiResponse(Status400BadRequest, "Yok Model is Invalid");
    }
}
