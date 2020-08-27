using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface IMufredatStore
    {
        Task<List<MufredatDto>> GetAll();

        Task<MufredatDto> GetById(int id);

        Task<Mufredat> Create(MufredatDto mufredatDto);

        Task<Mufredat> Update(MufredatDto mufredatDto);

        Task DeleteById(int id);
        Task Cokla(int id);
        Task<List<Mufredat>> GetMufredatByProgramIds(string[] programIds);
        Task<MufredatStateDto> GetMufredatState(int mufredatId);
        Task CreateDersAcilansByMufredatIds(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds);
        Task<MufredatDto> GetLastMufredatByProgramId(int programId);
    }
}
