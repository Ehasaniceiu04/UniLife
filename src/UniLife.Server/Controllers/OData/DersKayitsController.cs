using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class DersKayitsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DersKayitsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.DersKayit.Read)]
        public IEnumerable<DersKayit> Get()
        {
            return _applicationDbContext.DersKayits;
        }

    }
}
