using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class DersDilsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DersDilsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<DersDil> Get()
        {
            return _applicationDbContext.DersDils;
        }

    }
}
