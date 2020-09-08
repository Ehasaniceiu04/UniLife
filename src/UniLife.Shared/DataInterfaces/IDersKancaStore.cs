using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Shared.DataInterfaces
{
    public interface IDersKancaStore : IBaseStore<DersKanca, DersKancaDto>
    {
        Task YeniKanca(DersKancaDto dersKancaDto);
    }
}
