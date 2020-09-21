using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class ProgramsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProgramsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.Program.Read)]
        public IEnumerable<UniLife.Shared.DataModels.Program> Get()
        {
            return _applicationDbContext.Programs;
        }

    }
}
