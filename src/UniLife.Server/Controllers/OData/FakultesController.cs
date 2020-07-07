using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class FakultesController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public FakultesController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<Fakulte> Get()
        {
            return _applicationDbContext.Fakultes;
        }

    }
}
