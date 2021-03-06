using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class AkademisyensController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AkademisyensController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.Akademisyen.Read)]
        public IEnumerable<Akademisyen> Get()
        {
            return _applicationDbContext.Akademisyens;
        }

    }
}
