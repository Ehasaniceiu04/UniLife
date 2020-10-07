using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Sample;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface IFakulteStore
    {
        Task<List<FakulteDto>> GetAll();

        Task<FakulteDto> GetById(int id);

        Task<Fakulte> Create(FakulteDto fakulteDto);

        Task<Fakulte> Update(FakulteDto fakulteDto);

        Task DeleteById(int id);
        Task<List<ChartData>> GetOgrCountOfFakultesGYear();
    }
}