using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IOgrenimTurStore
    {
        Task<List<OgrenimTurDto>> GetAll();

        Task<OgrenimTurDto> GetById(int id);

        Task<OgrenimTur> Create(OgrenimTurDto ogrenimTurDto);

        Task<OgrenimTur> Update(OgrenimTurDto ogrenimTurDto);

        Task DeleteById(int id);
    }
}
