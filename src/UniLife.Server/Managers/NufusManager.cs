using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Storage.Migrations;

namespace UniLife.Server.Managers
{
    public class NufusManager : BaseManager<Nufus, NufusDto>, INufusManager
    {
        public NufusManager(INufusStore nufusStore) : base(nufusStore)
        {

        }

    }
}
