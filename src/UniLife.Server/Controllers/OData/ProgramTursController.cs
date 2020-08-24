using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class ProgramTursController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProgramTursController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        //[Microsoft.AspNet.OData.EnableQuery()]
        //[HttpGet]
        ////[Authorize(Permissions.Ogrenci.Create)]
        //public IEnumerable<UniLife.Shared.DataModels.ProgramTur> Get()
        //{
        //    return _applicationDbContext.ProgramTurs;
        //}

    }
}
