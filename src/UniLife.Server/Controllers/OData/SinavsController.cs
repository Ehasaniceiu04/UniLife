using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class SinavsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public SinavsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<Sinav> Get()
        {
            return _applicationDbContext.Sinavs;
        }

    }
}
