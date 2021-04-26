using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Extensions;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class ProgramsController : BaseOdataController
    {
        public ProgramsController(IApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.Program.Read)]
        public IEnumerable<UniLife.Shared.DataModels.Program> Get()
        {
            return _applicationDbContext.Programs.WhereIf(UserYetki.Count() > 0, x => UserYetki.Select(y => y.ProgramId).Contains(x.Id));
        }

    }
}
