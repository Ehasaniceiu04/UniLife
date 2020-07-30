using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class NufussController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public NufussController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel")]
        public IEnumerable<Nufus> Get()
        {
            return _applicationDbContext.Nufuss;
        }

    }
}
