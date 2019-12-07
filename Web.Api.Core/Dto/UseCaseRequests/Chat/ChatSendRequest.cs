using System;
using Web.Api.Core.Dto.UseCaseResponses.Chat;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Chat
{
    public class ChatSendRequest : IUseCaseRequest<ChatPostResponse>
    {
        public string UserId { get; }

        public int QuoteId { get; }

        public string Message { get; }

        public string TimeStamp { get; }

        public ChatSendRequest(string userId, int quoteId, string message, string timeStamp)
        {
            UserId = userId;
            QuoteId = quoteId;
            Message = message;
            TimeStamp = timeStamp;
        }
    }
}
