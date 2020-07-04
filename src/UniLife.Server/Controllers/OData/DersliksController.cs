using UniLife.Server.Managers;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using UniLife.Shared.Dto.Definitions;
using Microsoft.AspNetCore.Builder;
using UniLife.Storage;
using System.Collections.Generic;
using UniLife.Shared.DataModels;

namespace UniLife.Server.Controllers
{
    public class DersliksController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DersliksController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.DerslikRezerv.Create)]
        public IEnumerable<Derslik> Get()
        {
            return _applicationDbContext.Dersliks;
        }

    }
}
