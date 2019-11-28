using Web.Api.Core.Dto.UseCaseRequests.Chat;
using Web.Api.Core.Dto.UseCaseResponses.Chat;

namespace Web.Api.Core.Interfaces.UseCases.Chat
{
    public interface IChatSendUseCase : IUseCaseRequestHandler<ChatSendRequest, ChatResponse>
    {
    }
}
