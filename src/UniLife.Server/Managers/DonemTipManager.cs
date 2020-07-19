using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class DonemTipManager : BaseManager<DonemTip,DonemTipDto>, IDonemTipManager
    {
        public DonemTipManager(IDonemTipStore donemTipStore) : base(donemTipStore)
        {

        }

    }
}
