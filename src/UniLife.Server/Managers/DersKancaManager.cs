using System;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class DersKancaManager : BaseManager<DersKanca, DersKancaDto>, IDersKancaManager
    {
        public DersKancaManager(IDersKancaStore osymStore) : base(osymStore)
        {
         
        }
    }
}
