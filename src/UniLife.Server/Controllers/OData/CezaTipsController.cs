
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class CezaTipsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CezaTipsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize]
        public IEnumerable<CezaTip> Get()
        {
            return _applicationDbContext.CezaTips;
        }

    }
}
