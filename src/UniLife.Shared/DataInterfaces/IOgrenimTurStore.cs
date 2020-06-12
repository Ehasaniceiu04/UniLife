using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
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
