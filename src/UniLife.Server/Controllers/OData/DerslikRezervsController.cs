using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class DerslikRezervsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DerslikRezervsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.DerslikRezerv.Read)]
        public IEnumerable<DerslikRezerv> Get()
        {
            return _applicationDbContext.DerslikRezervs;
        }

    }
}
