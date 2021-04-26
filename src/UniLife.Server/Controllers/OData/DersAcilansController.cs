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
    public class DersAcilansController : BaseOdataController
    {
        public DersAcilansController(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        public IEnumerable<DersAcilan> Get()
        {
            var result = from da in _applicationDbContext.DersAcilans.WhereIf(UserYetki.Count() > 0, x => UserYetki.Select(y => y.ProgramId).Contains(x.ProgramId))
                         select da;

            return result;

        }

    }
}
