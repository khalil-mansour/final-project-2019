using System;
using Web.Api.Core.Dto.UseCaseResponses.Chat;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Chat
{
    public class ChatSendRequest : IUseCaseRequest<ChatResponse>
    {
        public string UserId { get; }

        public int QuoteId { get; }

        public string Message { get; }

        public DateTime TimeStamp { get; }

        public ChatSendRequest(string userId, int quoteId, string message, DateTime timeStamp)
        {
            UserId = userId;
            QuoteId = quoteId;
            Message = message;
            TimeStamp = timeStamp;
        }
    }
}
