using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class KayitNedensController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public KayitNedensController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize]
        public IEnumerable<KayitNeden> Get()
        {
            return _applicationDbContext.KayitNedens;
        }

    }
}
