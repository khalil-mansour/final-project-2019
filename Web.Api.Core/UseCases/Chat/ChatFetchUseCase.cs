using System;
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
    public sealed class ChatFetchUseCase : IChatFetchUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IChatRepository _chatRepository;

        public ChatFetchUseCase(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<bool> HandleAsync(ChatFetchRequest message, IOutputPort<ChatFetchResponse> outputPort)
        {
            // convert Timestamp string to DateTime
            var dateTime = Convert.ToDateTime(message.Timestamp);

            var response = await _chatRepository.
                FetchMessages(message.QuoteId, dateTime);

            outputPort.Handle(response.Success ? new ChatFetchResponse(response.Chats, true) : new ChatFetchResponse(new[] { new Error("Ation Failed", "Failed to fetch the request chat messages.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
