using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class MufredatsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public MufredatsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery(MaxExpansionDepth = 4)]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<Mufredat> Get()
        {
            return _applicationDbContext.Mufredats;
        }
    }
}
