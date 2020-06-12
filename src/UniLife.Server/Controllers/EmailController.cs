using UniLife.Shared;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Server.Managers;
using UniLife.Shared.Dto.Email;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc;


namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailManager _emailManager;

        public EmailController(IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        [HttpPost("Send")]
        [ProducesResponseType((int)Status200OK)]
        [ProducesResponseType((int)Status400BadRequest)]
        public async Task<ApiResponse> Send(EmailDto parameters)
            => ModelState.IsValid ?
                await _emailManager.Send(parameters) :
                new ApiResponse(Status400BadRequest, "User Model is Invalid");

        [HttpGet("Receive")]
        [Authorize]
        public async Task<ApiResponse> Receive()
            => await _emailManager.ReceiveMailImapAsync();
    }
}
