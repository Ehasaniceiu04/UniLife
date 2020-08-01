using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Shared.DataInterfaces
{
    public interface IOgrenciDigerStore : IBaseStore<OgrenciDiger, OgrenciDigerDto>
    {
        Task PostSaveGecis(OgrenciDigerDto ogrenciDigerDto);
        Task PostSaveKayitDond(OgrenciDigerDto ogrenciDigerDto);
        Task PostSaveKayitCeza(OgrenciDigerDto ogrenciDigerDto);
        Task PostSaveKayitStaj(OgrenciDigerDto ogrenciDigerDto);
        Task PostSaveKayitTez(OgrenciDigerDto ogrenciDigerDto);
    }
}
