using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class SinavKayitsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public SinavKayitsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<SinavKayit> Get()
        {
            return _applicationDbContext.SinavKayits;
        }

    }
}
