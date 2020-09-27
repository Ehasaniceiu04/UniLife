using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class ApiLogItemsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ApiLogItemsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize]
        public IEnumerable<ApiLogItem> Get()
        {
            return _applicationDbContext.ApiLogs;
        }

    }
}
