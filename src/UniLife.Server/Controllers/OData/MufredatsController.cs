using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class MufredatsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public MufredatsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery(MaxExpansionDepth = 4)]
        [HttpGet]
        [Authorize(Permissions.Mufredat.Read)]
        public IEnumerable<Mufredat> Get()
        {
            return _applicationDbContext.Mufredats;
        }
    }
}
