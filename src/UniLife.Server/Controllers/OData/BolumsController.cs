using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniLife.Shared.DataModels;
using UniLife.Shared.Extensions;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class BolumsController : BaseOdataController
    {
        public BolumsController(IApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize]
        public IEnumerable<Bolum> Get()
        {
            return _applicationDbContext.Bolums.WhereIf(UserYetki.Count() > 0, x => UserYetki.Select(y => y.BolumId).Contains(x.Id));
        }

    }
}
