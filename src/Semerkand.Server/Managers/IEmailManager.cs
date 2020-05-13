using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.Dto.Email;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IEmailManager
    {
        Task<ApiResponse> Send(EmailDto parameters);
        Task<ApiResponse> Receive();
        Task<ApiResponse> SendEmailAsync(EmailMessageDto emailMessage);
        List<EmailMessageDto> ReceiveEmail(int maxCount = 10);
        Task<ApiResponse> ReceiveMailImapAsync();
        Task<ApiResponse> ReceiveMailPopAsync(int min = 0, int max = 0);
        void Send(EmailMessageDto emailMessage);
    }
}