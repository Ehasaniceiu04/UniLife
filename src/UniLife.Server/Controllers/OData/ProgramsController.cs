using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<UniLife.Shared.DataModels.Program> Get()
        {
            return _applicationDbContext.Programs;
        }

    }
}
