using System.Collections.Generic;
using System.Threading.Tasks;
using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto;
using Semerkand.Shared.Dto.Sample;

namespace Semerkand.Server.Managers
{
    public interface IMessageManager
    {
        Task<ApiResponse> Create(MessageDto messageDto);
        List<MessageDto> GetList();
        Task<ApiResponse> Delete(int id);
    }
}