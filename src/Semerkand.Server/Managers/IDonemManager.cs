using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IDonemManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(DonemDto donemDto);
        Task<ApiResponse> Update(DonemDto donemDto);
        Task<ApiResponse> Delete(int id);
    }
}
