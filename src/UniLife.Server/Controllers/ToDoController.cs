using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ITodoManager _todoManager;

        public ToDoController(ITodoManager todoManager)
        {
            _todoManager = todoManager;
        }

        // GET: api/Todo
        [HttpGet]
        [Authorize]
        public async Task<ApiResponse> Get()
            => await _todoManager.Get();

        // GET: api/Todo/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ApiResponse> Get(int id)
            => ModelState.IsValid ?
                await _todoManager.Get(id) :
                new ApiResponse(Status400BadRequest, "Todo Model is Invalid");

        // POST: api/Todo
        [HttpPost]
        [Authorize(Permissions.Todo.Create)]
        public async Task<ApiResponse> Post([FromBody] TodoDto todo)
            => ModelState.IsValid ?
                await _todoManager.Create(todo) :
                new ApiResponse(Status400BadRequest, "Todo Model is Invalid");

        // Put: api/Todo
        [HttpPut]
        [Authorize(Permissions.Todo.Update)]
        public async Task<ApiResponse> Put([FromBody] TodoDto todo)
            => ModelState.IsValid ?
                await _todoManager.Update(todo) :
                new ApiResponse(Status400BadRequest, "Todo Model is Invalid");

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        [Authorize(Permissions.Todo.Delete)]
        public async Task<ApiResponse> Delete(long id)
            => await _todoManager.Delete(id);
    }
}
