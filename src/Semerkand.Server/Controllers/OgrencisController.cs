using Semerkand.Server.Managers;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.AuthorizationDefinitions;
using Semerkand.Shared.Dto.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Semerkand.Shared.Dto.Definitions;
using Microsoft.AspNetCore.Builder;
using Semerkand.Storage;
using System.Collections.Generic;
using Semerkand.Shared.DataModels;

namespace Semerkand.Server.Controllers
{
    public class OgrencisController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OgrencisController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        public IEnumerable<Ogrenci> Get()
        {
            return _applicationDbContext.Ogrencis;
        }

    }
}
