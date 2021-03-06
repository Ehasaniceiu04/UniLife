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
        Task<List<SinavOgrNotlarDto>> GetSinavKayitOgrenciNotlar(int sinavId,int dersAcilanId);
        Task<List<KeyValueDto>> GetOgrenciSinavsByDers(int ogrenciId, int dersAcilanId);
        Task<List<SinavSinavKayitDto>> GetSinavKayitsByOgrenciDers(int ogrenciId, int dersAcilanId);
        Task UpdateSinavKayit(int sinavkayitId, double orgNot);
        Task<bool> PutOgrenciSinavKayitNot(OgrenciNotlarDto ogrenciNotlarDto);
        Task<bool> PutAkaOgrenciSinavKayitNot(SinavOgrNotlarDto sinavOgrNotlarDto);
        Task UpdateOgrNotsBatch(List<SinavKayitNotBatch> sinavKayitNotBatches);
    }
}