using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class DersAcilansController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DersAcilansController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<DersAcilan> Get()
        {
            return _applicationDbContext.DersAcilans;
        }

    }
}
