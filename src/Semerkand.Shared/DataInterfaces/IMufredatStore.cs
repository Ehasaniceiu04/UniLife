using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IMufredatStore
    {
        Task<List<MufredatDto>> GetAll();

        Task<MufredatDto> GetById(int id);

        Task<Mufredat> Create(MufredatDto mufredatDto);

        Task<Mufredat> Update(MufredatDto mufredatDto);

        Task DeleteById(int id);
    }
}
