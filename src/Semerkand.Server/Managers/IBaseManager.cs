using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IBaseManager<T,TDto> where TDto : EntityDto<int>, new()
                                        where T : Entity<int>, new()
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(TDto tDto);
        Task<ApiResponse> Update(TDto tDto);
        Task<ApiResponse> Delete(int id);
    }
}
