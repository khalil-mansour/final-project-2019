using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.Chat;
using Web.Api.Core.Dto.UseCaseResponses.Chat;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.Chat;

namespace Web.Api.Core.UseCases.Chat
{
    public sealed class ChatSendUseCase : IChatSendUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IChatRepository _chatRepository;

        public ChatSendUseCase(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<bool> HandleAsync(ChatSendRequest message, IOutputPort<ChatPostResponse> outputPort)
        {
            var response = await _chatRepository.
                SendMessage(new Domain.Entities.Chat(
                    message.UserId,
                    message.QuoteId,
                    message.Message,
                    message.TimeStamp));

            outputPort.Handle(response.Success ? new ChatPostResponse(response.Chat, true) : new ChatPostResponse(new[] { new Error("Send Failed", "Failed to send the chat.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
