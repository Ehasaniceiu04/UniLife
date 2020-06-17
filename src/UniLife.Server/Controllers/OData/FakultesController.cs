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
