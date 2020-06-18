﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Shared.DataInterfaces
{
    public interface ISinavStore : IBaseStore<Sinav, SinavDto>
    {
        Task<List<SinavDto>> GetSinavListByAcilanDersId(int dersId);
    }
}
