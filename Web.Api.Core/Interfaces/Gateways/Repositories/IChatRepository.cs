using System;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories.Chat;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IChatRepository
    {
        Task<ChatRepoResponse> SendMessage(Chat chat);
        Task<ChatRepoResponse> FetchMessage(int quoteId, DateTime time);
    }
}
