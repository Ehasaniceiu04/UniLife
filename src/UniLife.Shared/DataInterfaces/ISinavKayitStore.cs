using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Sample;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface ISinavKayitStore
    {
        Task<List<SinavKayitDto>> GetAll();

        Task<SinavKayitDto> GetById(int id);

        Task<SinavKayit> Create(SinavKayitDto sinavKayitDto);

        Task<SinavKayit> Update(SinavKayitDto sinavKayitDto);

        Task DeleteById(int id);
        Task<List<OgrenciNotlarDto>> GetOgrenciNotlar(int ogrenciId);
        Task<List<SinavOgrNotlarDto>> GetSinavKayitOgrenciNotlar(int sinavId);
    }
}