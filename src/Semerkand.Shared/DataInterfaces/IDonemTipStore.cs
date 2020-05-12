using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IDonemTipStore
    {
        Task<List<DonemTipDto>> GetAll();

        Task<DonemTipDto> GetById(int id);

        Task<DonemTip> Create(DonemTipDto donemTipDto);

        Task<DonemTip> Update(DonemTipDto donemTipDto);

        Task DeleteById(int id);
    }
}
