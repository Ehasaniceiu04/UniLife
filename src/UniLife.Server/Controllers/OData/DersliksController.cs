using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class DersliksController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DersliksController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.Derslik.Read)]
        public IEnumerable<Derslik> Get()
        {
            return _applicationDbContext.Dersliks;
        }

    }
}
