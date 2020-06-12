using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Sample;

namespace UniLife.Shared.DataInterfaces
{
    public interface IMessageStore
    {
        Task<Message> AddMessage(MessageDto messageDto);

        Task DeleteById(int id);

        List<MessageDto> GetMessages();
    }
}