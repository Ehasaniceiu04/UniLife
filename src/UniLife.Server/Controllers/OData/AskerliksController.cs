using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class AskerliksController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AskerliksController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public IEnumerable<Askerlik> Get()
        {
            return _applicationDbContext.Askerliks;
        }

    }
}
