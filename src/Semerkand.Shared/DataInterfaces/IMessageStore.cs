using System.Collections.Generic;
using System.Threading.Tasks;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Sample;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IMessageStore
    {
        Task<Message> AddMessage(MessageDto messageDto);

        Task DeleteById(int id);

        List<MessageDto> GetMessages();
    }
}