using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class DerssController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DerssController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.Ders.Read)]
        public IEnumerable<Ders> Get()
        {
            return _applicationDbContext.Derss;
        }

    }
}
