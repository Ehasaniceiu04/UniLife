using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataModels;
using UniLife.Shared.Extensions;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class DersAcilansController : BaseController
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DersAcilansController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        public IEnumerable<DersAcilan> Get()
        {
            var userYetki = _applicationDbContext.UserProgramYetkis.Where(x => x.UserId == GuidUserId);

            //var result = from da in _applicationDbContext.DersAcilans.Where(x => userYetki.Select(y => y.ProgramId).Contains(x.ProgramId))
            var result = from da in _applicationDbContext.DersAcilans.WhereIf(userYetki.Count() > 0, x => userYetki.Select(y => y.ProgramId).Contains(x.ProgramId))
                         select da;

            return result;

        }

    }
}
