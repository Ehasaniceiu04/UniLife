using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class AkademisyenManager : BaseManager<Akademisyen, AkademisyenDto>, IAkademisyenManager
    {
        public AkademisyenManager(IAkademisyenStore akademisyenStore) : base(akademisyenStore)
        {

        }

    }
}
