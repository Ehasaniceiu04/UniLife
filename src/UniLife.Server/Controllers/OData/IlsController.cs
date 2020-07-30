using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class IlsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public IlsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<Il> Get()
        {
            return _applicationDbContext.Ils;
        }

    }
}
