using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class DersNedensController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DersNedensController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<DersNeden> Get()
        {
            return _applicationDbContext.DersNedens;
        }

    }
}
