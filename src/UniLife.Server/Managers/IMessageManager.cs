using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Sample;

namespace UniLife.Server.Managers
{
    public interface IMessageManager
    {
        Task<ApiResponse> Create(MessageDto messageDto);
        List<MessageDto> GetList();
        Task<ApiResponse> Delete(int id);
    }
}