using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IProgramManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(ProgramDto programDto);
        Task<ApiResponse> Update(ProgramDto programDto);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse> GetProgramByBolumIds(string[] bolumIds);
    }
}
