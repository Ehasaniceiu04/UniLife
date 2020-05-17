﻿using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IProgramStore
    {
        Task<List<ProgramDto>> GetAll();

        Task<ProgramDto> GetById(int id);

        Task<Program> Create(ProgramDto programDto);

        Task<Program> Update(ProgramDto programDto);

        Task DeleteById(int id);
        Task<List<Program>> GetProgramByBolumIds(string[] bolumIds);
    }
}
