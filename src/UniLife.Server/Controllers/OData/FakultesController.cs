using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniLife.Shared.DataModels;
using UniLife.Shared.Extensions;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class FakultesController : BaseOdataController
    {
        public FakultesController(IApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize]
        public IEnumerable<Fakulte> Get()
        {
            return _applicationDbContext.Fakultes.WhereIf(UserYetki.Count() > 0, x => UserYetki.Select(y => y.FakulteId).Contains(x.Id));
        }

    }
}
